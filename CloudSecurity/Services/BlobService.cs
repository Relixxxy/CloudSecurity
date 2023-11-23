using CloudSecurity.Data.Entities;
using CloudSecurity.Data.Repositories;
using CloudSecurity.Services.Interfaces;
using CloudSecurity.Validators;
using Microsoft.AspNetCore.Components.Forms;

namespace CloudSecurity.Services;

public class BlobService : IBlobService
{
    private readonly IBlobRepository _blobRepository;
    private readonly BlobFileValidator _validator;

    public BlobService(IBlobRepository blobRepository, BlobFileValidator validator)
    {
        _blobRepository = blobRepository;
        _validator = validator;
    }

    public IAsyncEnumerable<FileInfoEntity> GetAllFilesAsync()
    {
        return _blobRepository.GetAllFilesAsync();
    }

    public async Task<bool> UploadBlobFileAsync(IBrowserFile file)
    {
        var validationResult = _validator.Validate(file);

        if (validationResult.IsValid)
        {
            using var stream = file.OpenReadStream();
            var result = await _blobRepository.UploadBlobFileAsync(stream, file.Name);

            return true;
        }

        return false;
    }
}
