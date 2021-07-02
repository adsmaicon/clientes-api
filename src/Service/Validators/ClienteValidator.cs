using FluentValidation;
using Clientes.Infra.CrossCutting.Validators;
using Clientes.Domain.Entities;
using Clientes.Domain.Interfaces.Repositories;

namespace Clientes.Service.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteValidator(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;

            RuleFor(c => c.Nome).NotEmpty().NotNull();

            RuleFor(c => c.Estado).NotEmpty().NotNull();

            RuleFor(c => c.CPF).NotEmpty().NotNull()
                .Must(ValidateCPF.CPFIsValid).WithMessage("'CPF' inválido.")
                .Must(CPFIsDuplicated).WithMessage("'CPF' duplicado.");
        }

        private bool CPFIsDuplicated(string CPF)
        {
            return _clienteRepository.SelectByCPFAsync(CPF).Result.Count == 0;
        }

    }
}