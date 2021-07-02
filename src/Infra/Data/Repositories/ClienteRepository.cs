using Clientes.Domain.Entities;
using Clientes.Domain.Interfaces.Repositories;
using Clientes.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clientes.Infra.Data.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(MySqlContext mySqlContext) : base(mySqlContext)
        {
        }

        public async Task<IList<Cliente>> SelectByCPFAsync(string CPF)
        {
            return await Task.FromResult(_mySqlContext.Clientes.Where(c => c.CPF == CPF).ToList());
        }
    }
}