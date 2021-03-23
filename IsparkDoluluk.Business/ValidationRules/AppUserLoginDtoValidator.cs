using FluentValidation;
using IsparkDoluluk.Dto.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.Business.ValidationRules
{
    public class AppUserLoginDtoValidator : AbstractValidator<AppUserLoginDto>
    {
        public AppUserLoginDtoValidator()
        {
            RuleFor(I => I.Username).NotEmpty().WithMessage("Kullanıcı adı boş olamaz.");
            RuleFor(I => I.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz.");
        }
    }
}
