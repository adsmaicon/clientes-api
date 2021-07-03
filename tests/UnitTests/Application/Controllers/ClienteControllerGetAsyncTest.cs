using System.Collections.Generic;
using System.Net;
using Clientes.Application.Controllers;
using Clientes.Domain.Entities;
using Clientes.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Clientes.UnitTests.Application.Controllers
{
    public class ClienteControllerGetAsyncTest : ClienteControllerBaseTest
    {

        [Fact]
        public void ClienteControllerTest_GetAsync_Success()
        {
            ///Given            
            var cliente = new Cliente
            {
                Id = 1,
                Nome = "Maicon",
                Estado = "PR",
                CPF = "489.754.200-60"
            };
            IList<Cliente> response_service = new List<Cliente>() { cliente };
            _clienteService.Setup(e => e.GetAsync()).ReturnsAsync(response_service);

            //When
            var response = (OkObjectResult)_clienteController.GetAsync().Result;

            //Then
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)response.StatusCode);
        }

        [Fact]
        public void ClienteControllerTest_GetAsync_NotFound()
        {
            ///Given            
            IList<Cliente> response_service = new List<Cliente>() { };
            _clienteService.Setup(e => e.GetAsync()).ReturnsAsync(response_service);

            //When
            var response = (NotFoundResult)_clienteController.GetAsync().Result;

            //Then
            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)response.StatusCode);
        }
    }
}