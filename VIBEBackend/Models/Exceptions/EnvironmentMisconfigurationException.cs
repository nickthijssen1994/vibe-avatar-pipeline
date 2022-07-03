namespace Models.Exceptions
{
	public class EnvironmentMisconfigurationException : Exception
	{
		public EnvironmentMisconfigurationException() { }
		public EnvironmentMisconfigurationException(string message) : base(message) { }
	}
}