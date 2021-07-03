using Clientes.Application.Controllers;
using Clientes.Domain.Interfaces.Services;
using Moq;

namespace Clientes.UnitTests.Application.Controllers
{
    public class ClienteControllerBaseTest
    {
        protected readonly ClientesController _clienteController;
        protected readonly Mock<IClienteService> _clienteService;

        public ClienteControllerBaseTest()
        {
            _clienteService = new Mock<IClienteService>();
            _clienteController = new ClientesController(_clienteService.Object);
        }
    }
}
