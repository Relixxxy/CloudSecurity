using Azure.Storage.Blobs;
using CloudSecurity.Data.Entities;
using Microsoft.Extensions.Options;

namespace CloudSecurity.Data.Repositories;

public class BlobRepository : IBlobRepository
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _client;

    public BlobRepository(BlobServiceClient blobServiceClient, IOptions<CloudSecuritySettings> settings)
    {
        var blobContainerName = settings?.Value.BlobContainerName;

        _blobServiceClient = blobServiceClient;
        _client = _blobServiceClient.GetBlobContainerClient(blobContainerName);
    }

    public async IAsyncEnumerable<FileInfoEntity> GetAllFilesAsync()
    {
        await foreach (var blobItem in _client.GetBlobsAsync())
        {
            var blobClient = _client.GetBlobClient(blobItem.Name);

            yield return new FileInfoEntity
            {
                Name = blobItem.Name,
                Link = blobClient.Uri.ToString()
            };
        }
    }

    public async Task RemoveBlobFileAsync(string filename)
    {
        var blobClient = _client.GetBlobClient(filename);
        await blobClient.DeleteAsync();
    }

    public async Task<string> UploadBlobFileAsync(Stream file, string filename)
    {
        var blobClient = _client.GetBlobClient(filename);
        await blobClient.UploadAsync(file);

        return blobClient.Uri.ToString();
    }
}
