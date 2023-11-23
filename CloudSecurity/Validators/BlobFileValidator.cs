using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;

namespace CloudSecurity.Validators;

public class BlobFileValidator : AbstractValidator<IBrowserFile>
{
    public BlobFileValidator(int maxFileSize)
    {
        RuleFor(file => file.Size)
            .NotNull()
            .WithMessage("File cannot be null")
            .LessThanOrEqualTo(maxFileSize)
            .WithMessage($"File size cannot exceed {maxFileSize} bytes");
    }
}
