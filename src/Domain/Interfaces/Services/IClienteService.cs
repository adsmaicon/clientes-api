using FluentValidation;
using Clientes.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clientes.Domain.Interfaces.Services
{
    public interface IClienteService : IBaseService
    {
        Task<IEnumerable<Cliente>> GetByCPFAsync<TValidator>(string CPF) where TValidator : AbstractValidator<string>;

    }

}