# FileStorage Library

You can use this library to connect to an Azure Blob Storage and upload or retreive files.

Add this line to the startup of the application:

```csharp
builder.Services.AddAzureBlobStorageServices(builder.Configuration);
```

When running the blob storage locally, use the following connectionstring:

`UseDevelopmentStorage=true;`

Put the connectionstring in the appsettings.json file of the application:

```json
{
  "AzureStorageConnectionString": "UseDevelopmentStorage=true;",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "FileSizeLimit": 268435456,
  "AllowedHosts": "*"
}
```
