using BackendCourse.DTOs;
using FluentValidation;

namespace BackendCourse.Validators
{
    public class BeerUpdateValidator : AbstractValidator<BeerUpdateDTO>
    {

        public BeerUpdateValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("El Id es obligatorio.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("El nombre debe medir entre 2 y 20 caracteres");
            RuleFor(x => x.BrandId).NotNull().WithMessage("La marca es obligatoria.");
            RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage("El {propertyName} debe ser mayor a cero ( 0 ).");
        }

    }
}
