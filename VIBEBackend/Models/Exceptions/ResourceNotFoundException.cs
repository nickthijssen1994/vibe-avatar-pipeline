namespace Models.Exceptions
{
	public class ResourceNotFoundException : Exception
	{
		public ResourceNotFoundException(string resourceName) : base($"Resource {resourceName} not found") { }

		public ResourceNotFoundException(string resourceName, Exception innerException) : base(
			$"Resource {resourceName} not found", innerException) { }
	}
}