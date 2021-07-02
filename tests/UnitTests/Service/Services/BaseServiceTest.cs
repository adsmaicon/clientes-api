using AutoMapper;
using Clientes.Application.Models;
using Clientes.Domain.Entities;
using Clientes.Domain.Interfaces.Repositories;
using Clientes.Domain.Interfaces.Services;
using Clientes.Service.Services;
using Moq;
using Xunit;

namespace Clientes.UnitTests.Service.Services
{
    public class BaseServiceTest
    {
        private readonly IClienteService _service;
        private readonly Mock<IClienteRepository> _repository;

        public BaseServiceTest()
        {
            _repository =  new Mock<IClienteRepository>();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClienteRequest, Cliente>()
                    .ForMember(d => d.CPF, map =>
                    map.MapFrom(s => s.CPF.Replace(".", "").Replace("-", ""))
                );

                cfg.CreateMap<Cliente, ClienteResponse>()
                    .ForMember(d => d.CPF, map =>
                        map.MapFrom(s => s.CPF.Replace(".", "").Replace("-", ""))
                );
            });
            var mapper = configuration.CreateMapper();
            _service = new ClienteService(_repository.Object, mapper);
        }

        [Fact]
        public void TestName()
        {
            //Given

            //When

            //Then
        }
    }
}
