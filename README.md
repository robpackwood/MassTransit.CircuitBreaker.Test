# MassTransit.CircuitBreaker.Test
 MassTransit Simple Circuit Breaker Test

To run set MassTransit.Consumer.CircuitBreaker and MassTransit.Publisher as startup projects and run
The Publisher will publish 1 message every second
The Consumer will fail 50% of the time, which exceeds the circuit breaker threshold.

Turn on CLR exceptions and you ignore the "Broken Circuit Test" exception. The key exception is the object reference null exception happening in MassTransit 6.2.5 here many times over:

System.NullReferenceException
  HResult=0x80004003
  Message=Object reference not set to an instance of an object.
  Source=MassTransit
  StackTrace:

>	MassTransit.dll!MassTransit.Context.ConsumeSendEndpoint.ConsumeTask(System.Threading.Tasks.Task task) Line 189	C#
 	MassTransit.dll!MassTransit.Context.ConsumeSendEndpoint.Send<MassTransit.ReceiveFault>(MassTransit.ReceiveFault message, System.Threading.CancellationToken cancellationToken) Line 40	C#
 	MassTransit.dll!MassTransit.Pipeline.Filters.GenerateFaultFilter.GenerateFault(MassTransit.ExceptionReceiveContext context)	C#
 	MassTransit.dll!MassTransit.Pipeline.Filters.GenerateFaultFilter.GreenPipes.IFilter<MassTransit.ExceptionReceiveContext>.Send(MassTransit.ExceptionReceiveContext context, GreenPipes.IPipe<MassTransit.ExceptionReceiveContext> next)	C#
 	GreenPipes.dll!GreenPipes.Pipes.FilterPipe<MassTransit.ExceptionReceiveContext>.GreenPipes.IPipe<MassTransit.ExceptionReceiveContext>.Send(MassTransit.ExceptionReceiveContext context)	Unknown
 	GreenPipes.dll!GreenPipes.Filters.RescueFilter<MassTransit.ReceiveContext, MassTransit.ExceptionReceiveContext>.GreenPipes.IFilter<MassTransit.ReceiveContext>.Send(MassTransit.ReceiveContext context, GreenPipes.IPipe<MassTransit.ReceiveContext> next)	Unknown
 	GreenPipes.dll!GreenPipes.Pipes.FilterPipe<MassTransit.ReceiveContext>.GreenPipes.IPipe<MassTransit.ReceiveContext>.Send(MassTransit.ReceiveContext context)	Unknown
 	MassTransit.dll!MassTransit.Pipeline.Filters.DeadLetterFilter.GreenPipes.IFilter<MassTransit.ReceiveContext>.Send(MassTransit.ReceiveContext context, GreenPipes.IPipe<MassTransit.ReceiveContext> next)	C#
 	GreenPipes.dll!GreenPipes.Pipes.FilterPipe<MassTransit.ReceiveContext>.GreenPipes.IPipe<MassTransit.ReceiveContext>.Send(MassTransit.ReceiveContext context)	Unknown
 	MassTransit.dll!MassTransit.Pipeline.Pipes.ReceivePipe.GreenPipes.IPipe<MassTransit.ReceiveContext>.Send(MassTransit.ReceiveContext context) Line 23	C#
 	MassTransit.dll!MassTransit.Transports.ReceivePipeDispatcher.Dispatch(MassTransit.ReceiveContext context, MassTransit.Transports.ReceiveLockContext receiveLock)	C#
 	MassTransit.RabbitMqTransport.dll!MassTransit.RabbitMqTransport.Pipeline.RabbitMqBasicConsumer.RabbitMQ.Client.IBasicConsumer.HandleBasicDeliver.AnonymousMethod__0()	Unknown
 	System.Private.CoreLib.dll!System.Threading.Tasks.Task<System.Threading.Tasks.Task>.InnerInvoke()	Unknown
 	System.Private.CoreLib.dll!System.Threading.Tasks.Task..cctor.AnonymousMethod__274_0(object obj)	Unknown
 	System.Private.CoreLib.dll!System.Threading.ExecutionContext.RunFromThreadPoolDispatchLoop(System.Threading.Thread threadPoolThread, System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state)	Unknown
 	System.Private.CoreLib.dll!System.Threading.Tasks.Task.ExecuteWithThreadLocal(ref System.Threading.Tasks.Task currentTaskSlot, System.Threading.Thread threadPoolThread)	Unknown
 	System.Private.CoreLib.dll!System.Threading.Tasks.Task.ExecuteEntryUnsafe(System.Threading.Thread threadPoolThread)	Unknown
 	System.Private.CoreLib.dll!System.Threading.Tasks.Task.ExecuteFromThreadPool(System.Threading.Thread threadPoolThread)	Unknown
 	System.Private.CoreLib.dll!System.Threading.ThreadPoolWorkQueue.Dispatch()	Unknown


