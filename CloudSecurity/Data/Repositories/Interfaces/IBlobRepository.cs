using CloudSecurity.Data.Entities;

namespace CloudSecurity.Data.Repositories;

public interface IBlobRepository
{
    IAsyncEnumerable<FileInfoEntity> GetAllFilesAsync();

    Task<string> UploadBlobFileAsync(Stream file, string filename);

    Task RemoveBlobFileAsync(string filename);
}
