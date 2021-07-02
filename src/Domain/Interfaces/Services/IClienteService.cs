using FluentValidation;
using Clientes.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clientes.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        Task<Cliente> AddAsync<TRequest, TValidator>(TRequest request)
            where TValidator : AbstractValidator<Cliente>
            where TRequest : class;

        Task<Cliente> UpdateAsync<TRequest, TValidator>(TRequest request)
            where TValidator : AbstractValidator<Cliente>
            where TRequest : class;

        Task DeleteAsync(int id);

        Task<IList<Cliente>> GetAsync();

        Task<Cliente> GetByIdAsync(int id);
        
        Task<IEnumerable<Cliente>> GetByCPFAsync<TValidator>(string CPF) where TValidator : AbstractValidator<string>;

    }

}