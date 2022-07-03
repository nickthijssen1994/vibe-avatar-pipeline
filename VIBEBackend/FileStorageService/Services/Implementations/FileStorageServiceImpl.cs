using MessagingNetwork;

namespace FileStorageService.Services.Implementations;

public class FileStorageServiceImpl : IFileStorageService
{
	private readonly ILogger<FileStorageServiceImpl> _logger;
	private readonly IBus _bus;

	public FileStorageServiceImpl(ILogger<FileStorageServiceImpl> logger, IBus bus)
	{
		_logger = logger;
		_bus = bus;
	}
}