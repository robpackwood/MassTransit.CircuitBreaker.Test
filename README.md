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
   at MassTransit.Context.ConsumeSendEndpoint.ConsumeTask(Task task)
