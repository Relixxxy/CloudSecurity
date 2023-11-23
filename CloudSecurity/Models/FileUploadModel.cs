using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace CloudSecurity.Models;

public class FileUploadModel
{
    [Required(ErrorMessage = "Please choose a file.")]
    public IBrowserFile File { get; set; } = null!;
}
