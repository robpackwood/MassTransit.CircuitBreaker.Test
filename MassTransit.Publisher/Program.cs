using System;
using System.Threading;
using MassTransit.Messages;
using Topshelf;

namespace MassTransit.Publisher
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

			for (var i = 1; i <= 30; i++)
			{
				Console.WriteLine($"Publishing message: {i}");
				_bus.Publish(new BrokenConsumerMessage {MessageId = i});
				Thread.Sleep(1000);
			}

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
			});
		}
	}
}