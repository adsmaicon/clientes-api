using System.Net;
using Clientes.Application.Controllers;
using Clientes.Domain.Entities;
using Clientes.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Clientes.UnitTests.Application.Controllers
{
    public class ClienteControllerGetByIdAsyncTest : ClienteControllerBaseTest
    {

        [Fact]
        public void ClienteControllerTest_GetByIdAsync_Success()
        {
            ///Given           
            var cliente = new Cliente
            {
                Id = 1,
                Nome = "Maicon",
                Estado = "PR",
                CPF = "489.754.200-60"
            };
            _clienteService.Setup(e => e.GetByIdAsync(1)).ReturnsAsync(cliente);

            //When
            var response = (OkObjectResult)_clienteController.GetByIdAsync(1).Result;

            //Then
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)response.StatusCode);
        }

        [Fact]
        public void ClienteControllerTest_GetByIdAsync_NotFound()
        {
            ///Given            
            _clienteService.Setup(e => e.GetByIdAsync(2));

            //When
            var response = (NotFoundResult)_clienteController.GetByIdAsync(2).Result;

            //Then
            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)response.StatusCode);
        }
    }
}
