using System;
using System.Linq;
using System.Threading.Tasks;
using FinancesCore.Business.Intefaces;
using FinancesCore.Business.Models;
using FluentValidation;

namespace FinancesCore.Business.Validations
{
    public class TransactionValidation : AbstractValidator<Transaction>
    {
        public TransactionValidation()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
                .Length(min: 2, max: 100).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(t => t.Value)
                .GreaterThan(0).WithMessage("The Transaction {PropertyName} field must be greater than {ComparisonValue}");

            RuleFor(x => x.CategoryId).NotEqual(Guid.Empty).WithMessage("CategoryId cannot be empty");         
        }
    }
}
