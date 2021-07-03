using System.Net;
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
    public class ClienteControllerInsertAsyncTest : ClienteControllerBaseTest
    {
        [Fact]
        public void ClienteControllerTest_InsertAsync_Success()
        {
            //Given            
            var clienteResponse = new Cliente
            {
                Id = 1,
                Nome = "Maicon",
                Estado = "PR",
                CPF = "489.754.200-60"
            };

            _clienteService.Setup(e => e.AddAsync<ClienteRequest, ClienteValidator>
                (It.IsAny<ClienteRequest>()))
                .ReturnsAsync(clienteResponse);

            var clienteRequest = new ClienteRequest
            {
                Nome = "Maicon",
                Estado = "PR",
                CPF = "489.754.200-60"
            };

            //When
            var response = (CreatedResult)_clienteController.InsertAsync(clienteRequest).Result;

            //Then
            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)response.StatusCode);
        }

        [Fact]
        public void ClienteControllerTest_InsertAsync_Error()
        {
            ///Given            
            _clienteService.Setup(e => e.AddAsync<ClienteRequest, ClienteValidator>
                (It.IsAny<ClienteRequest>()))
                .Throws(new FluentValidation.ValidationException(
                    "Validation failed: \n " +
                    "    -- CPF: 'CPF' inválido. Severity: Error"));

            var clienteRequest = new ClienteRequest
            {
                Nome = "Maicon",
                Estado = "PR",
                CPF = "878.222.211-00"
            };

            //When
            var response = (BadRequestObjectResult)_clienteController.InsertAsync(clienteRequest).Result;

            //Then
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)response.StatusCode);
        }
    }
}