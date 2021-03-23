using FluentValidation;
using IsparkDoluluk.Dto.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.Business.ValidationRules
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(I => I.Username).NotEmpty().WithMessage("Kullanıcı adı boş olamaz.");
            RuleFor(I => I.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz.");
            RuleFor(I => I.Role).NotEmpty().WithMessage("Rol alanı boş olamaz.");
        }
    }
}
