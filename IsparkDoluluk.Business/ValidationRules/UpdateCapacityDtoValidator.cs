using FluentValidation;
using IsparkDoluluk.Dto.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.Business.ValidationRules
{
    public class UpdateCapacityDtoValidator : AbstractValidator<UpdateCapacityDto>
    {
        public UpdateCapacityDtoValidator()
        {
            RuleFor(I => I.ParkPlaceId).NotEmpty().WithMessage("Park alanı id alanı boş olamaz.");
            RuleFor(I => I.Capacity).NotEmpty().WithMessage("Kapasite alanı boş olamaz.");
        }
    }
}
