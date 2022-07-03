using System.Net.Sockets;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace MessagingNetwork
{
	public class RabbitBusFactory
	{
		public static IBus CreateBus(string hostName)
		{
			var _factory = new ConnectionFactory()
			{
				HostName = hostName,
				DispatchConsumersAsync = true
			};
			IConnection _connection = _factory.CreateConnection();
			IModel _channel = _connection.CreateModel();
			return new RabbitBus(_channel);
		}

		public static IBus CreateBus(string hostName, ushort hostPort, string virtualHost, string username,
			string password)
		{
			var _factory = new ConnectionFactory
			{
				HostName = hostName,
				Port = hostPort,
				VirtualHost = virtualHost,
				UserName = username,
				Password = password,
				DispatchConsumersAsync = true
			};

			var policy = Policy.Handle<SocketException>()
				.Or<BrokerUnreachableException>()
				.WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
				{
					Console.Out.Write(
						$"RabbitMQ Client could not connect after {time.TotalSeconds:n1}s ({ex.Message})");
					Console.Out.WriteLine();
				});

			IConnection _connection = null;
			policy.Execute(() => { _connection = _factory.CreateConnection(); });

			IModel _channel = _connection.CreateModel();
			return new RabbitBus(_channel);
		}
	}
}