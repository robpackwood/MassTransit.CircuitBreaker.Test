using System;
using System.Threading.Tasks;
using MassTransit.Messages;

namespace MassTransit.Consumer.CircuitBreaker
{
	public class BrokenConsumer : IConsumer<BrokenConsumerMessage>
	{
		public Task Consume(ConsumeContext<BrokenConsumerMessage> context)
		{
			Console.Out.WriteLine($"Processing message: {context.Message.MessageId}");

			if (context.Message.MessageId % 2 == 0)
				throw new Exception("Broken Circuit Test");

			return Task.CompletedTask;
		}
	}
}