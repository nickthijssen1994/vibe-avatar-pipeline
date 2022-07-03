# FileStorageService

This service provides an endpoint for uploading files to an Azure Blob Storage. It handles the directory structure
within the Blob Storage and is able to retrieve files from a specific avatar or analysis.

After uploading a file or files, it sends a message via RabbitMQ to let the AvatarService know files where added for a
specific avatar.

### Links to resources and techniques used for this service

https://github.com/dotnet/AspNetCore.Docs/tree/main/aspnetcore/mvc/models/file-uploads/samples

https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-6.0#file-upload-scenarios

https://docs.microsoft.com/nl-nl/azure/storage/blobs/storage-quickstart-blobs-dotnet?tabs=environment-variable-windows
