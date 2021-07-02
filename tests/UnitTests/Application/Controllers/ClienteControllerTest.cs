using System.Net;
using System.Threading.Tasks;
using Clientes.Application.Controllers;
using Clientes.Application.Models;
using Clientes.Domain.Entities;
using Clientes.Domain.Interfaces.Services;
using Clientes.Service.Validators;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Clientes.UnitTests.Application.Controllers
{
    public class ClienteControllerTest
    {
        private readonly ClientesController _clienteController;
        private readonly Mock<IClienteService> _clienteService;

        public ClienteControllerTest()
        {
            _clienteService = new Mock<IClienteService>();
            _clienteController = new ClientesController(_clienteService.Object);
        }

        [Fact]
        public void ClienteControllerTest_InsertAsync_Success()
        {
            //Given
            var clienteRequest = new ClienteRequest
            {
                Nome = "Maicon",
                Estado = "PR",
                CPF = "489.754.200-60"
            };

            var clienteResponse = new Cliente{
                Id = 1,
                Nome = "Maicon",
                Estado = "PR",
                CPF = "489.754.200-60"
            };

            _clienteService.Setup(e => e.AddAsync<ClienteRequest, ClienteValidator>
                (It.IsAny<ClienteRequest>()))
                .ReturnsAsync(clienteResponse);

            //When
            var response = (CreatedResult) _clienteController.InsertAsync(clienteRequest).Result;

            //Then
            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)response.StatusCode);
        }
    }
}
