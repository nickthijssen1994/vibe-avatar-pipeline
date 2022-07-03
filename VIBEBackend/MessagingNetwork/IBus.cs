namespace MessagingNetwork
{
	public interface IBus
	{
		Task SendAsync<T>(string queue, T message);
		Task ReceiveAsync<T>(string queue, Action<T?> onMessage);
	}
}