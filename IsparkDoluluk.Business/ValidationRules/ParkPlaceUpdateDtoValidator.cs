using FluentValidation;
using IsparkDoluluk.Dto.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.Business.ValidationRules
{
    public class ParkPlaceUpdateDtoValidator : AbstractValidator<ParkPlaceUpdateDto>
    {
        public ParkPlaceUpdateDtoValidator()
        {
            RuleFor(I => I.Id).NotEmpty().WithMessage("Id alanı boş olamaz.");
            RuleFor(I => I.Name).NotEmpty().WithMessage("İsim alanı boş olamaz.");
            RuleFor(I => I.ParkType).NotEmpty().WithMessage("Park tipi alanı boş olamaz.");
            RuleFor(I => I.Capacity).NotEmpty().WithMessage("Kapasite alanı boş olamaz");
            RuleFor(I => I.WorkHours).NotEmpty().WithMessage("Çalışma saatleri boş olamaz.");
            RuleFor(I => I.Adress).NotEmpty().WithMessage("Adres boş olamaz.");
            RuleFor(I => I.District).NotEmpty().WithMessage("Bölge boş olamaz.");
        }
    }
}
