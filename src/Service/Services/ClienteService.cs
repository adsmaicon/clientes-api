using AutoMapper;
using Clientes.Domain.Entities;
using Clientes.Domain.Interfaces.Repositories;
using Clientes.Domain.Interfaces.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clientes.Service.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;
        public ClienteService(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<Cliente> AddAsync<TRequest, TValidator>(TRequest request)
            where TValidator : AbstractValidator<Cliente>
            where TRequest : class
        {
            Cliente entity = _mapper.Map<Cliente>(request);

            var validator = (TValidator)Activator.CreateInstance(typeof(TValidator), _repository);
            await validator.ValidateAndThrowAsync(entity);

            await _repository.InsertAsync(entity);

            return _mapper.Map<Cliente>(entity);
        }

        public async Task<Cliente> UpdateAsync<TRequest, TValidator>(TRequest request)
            where TValidator : AbstractValidator<Cliente>
            where TRequest : class
            
        {
            Cliente entity = _mapper.Map<Cliente>(request);

            var validator = (TValidator)Activator.CreateInstance(typeof(TValidator), _repository);
            await validator.ValidateAndThrowAsync(entity);

            await _repository.UpdateAsync(entity);

            return _mapper.Map<Cliente>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IList<Cliente>> GetAsync()
        {
            var entities = await _repository.SelectAsync();

            return _mapper.Map<IList<Cliente>, IList<Cliente>>(entities);
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            var entity = await _repository.SelectAsync(id);

            return _mapper.Map<Cliente>(entity);
        }

        public async Task<IEnumerable<Cliente>> GetByCPFAsync<TValidator>(string CPF)
            where TValidator : AbstractValidator<string>
        {
            await Activator.CreateInstance<TValidator>().ValidateAndThrowAsync(CPF);
            var entities = await _repository.SelectByCPFAsync(CPF.Replace(".", "").Replace("-", ""));

            return entities.Select(s => _mapper.Map<Cliente>(s));
        }
    }
}