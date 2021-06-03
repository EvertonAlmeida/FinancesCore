using System;
using System.Linq;
using System.Threading.Tasks;
using FinancesCore.Business.Models;
using FluentValidation;

namespace FinancesCore.Business.Validations
{
    public class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
                .Length(min: 2, max: 100).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");
        }
    }
}
