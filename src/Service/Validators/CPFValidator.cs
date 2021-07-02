using FluentValidation;
using Clientes.Infra.CrossCutting.Validators;

namespace Clientes.Service.Validators
{
    public class CPFValidator : AbstractValidator<string>
    {
        public CPFValidator()
        {
            RuleFor(c => c).NotEmpty().NotNull()
                .Must(ValidateCPF.CPFIsValid).WithMessage("'CPF' inválido.");
        }        
    }
}