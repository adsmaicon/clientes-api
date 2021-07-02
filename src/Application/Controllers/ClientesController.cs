using Clientes.Application.Models;
using Clientes.Domain.Interfaces.Services;
using Clientes.Service.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clientes.Application.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync(ClienteRequest request)
        {            
            try
            {
                var clienteOut = await _clienteService.AddAsync<ClienteRequest, ClienteValidator>(request);

                return Created(string.Empty, clienteOut);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var cliente = await _clienteService.GetAsync();

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpGet("{id}", Name = "GetCliente")]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (id == 0)
                return NotFound();

            return Ok(await _clienteService.GetByIdAsync(id));
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<IActionResult> GetByCPFAsync(string CPF)
        {
            return Ok(await _clienteService.GetByCPFAsync<CPFValidator>(CPF));
        }

    }
}