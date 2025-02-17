using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MovieProject.Application.Features.AppUser.Commands;
using MovieProject_Domain.Entities;


namespace MovieProject.Application.Validator.AppUser;

public class ChangePasswordValidator : AbstractValidator<ChangeAppUserPasswordCommand>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.oldPassword)
            .NotEmpty().WithMessage("Eski parola boş olamaz.");

        RuleFor(x => x.newPassword)
            .NotEmpty().WithMessage("Yeni parola boş olamaz.")
            .Matches(@"[A-Z]").WithMessage("Yeni parolanız en az 1 büyük harf içermelidir.")
            .Matches(@"[a-z]").WithMessage("Yeni parolanız en az 1 küçük harf içermelidir.")
            .Matches(@"[0-9]").WithMessage("Yeni parolanız en az 1 rakam içermelidir.")
            .MinimumLength(5).WithMessage("Parolanız en az 5 karakter olmalıdır.")
            .MaximumLength(12).WithMessage("Parolanız en fazla 12 karakter olmalıdır.")
            .NotEqual(x => x.oldPassword).WithMessage("Yeni parola eski parolayla aynı olamaz.");
    }
}

