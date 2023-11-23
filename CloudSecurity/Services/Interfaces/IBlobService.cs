using Microsoft.AspNetCore.Components.Forms;

namespace CloudSecurity.Services.Interfaces;

public interface IBlobService
{
    Task<bool> UploadBlobFileAsync(IBrowserFile file);
}
