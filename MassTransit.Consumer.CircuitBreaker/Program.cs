using System;
using GreenPipes;
using Topshelf;

namespace MassTransit.Consumer.CircuitBreaker
{
	public class Program
	{
		public static int Main()
		{
			return (int) HostFactory.Run(cfg => cfg.Service(x => new EventConsumerService()));
		}
	}

	internal class EventConsumerService : ServiceControl
	{
		private IBusControl _bus;

		public bool Start(HostControl hostControl)
		{
			_bus = ConfigureBus();
			_bus.Start();
			return true;
		}

		public bool Stop(HostControl hostControl)
		{
			_bus?.Stop(TimeSpan.FromSeconds(30));
			return true;
		}

		private IBusControl ConfigureBus()
		{
			return Bus.Factory.CreateUsingRabbitMq(cfg =>
			{
				cfg.Host("localhost",
					"masstransit.circuitbreaker",
					host =>
					{
						host.Username("admin");
						host.Password("password");
					});

				cfg.ReceiveEndpoint(
					"masstransit.circuitbreaker.endpoint",
					e =>
					{
						e.UseCircuitBreaker(cb =>
						{
							cb.ActiveThreshold = 5;
							cb.ResetInterval = TimeSpan.FromSeconds(15);
							cb.TrackingPeriod = TimeSpan.FromSeconds(10);
							cb.TripThreshold = 20;
						});

						e.Consumer<BrokenConsumer>();
					});
			});
		}
	}
}