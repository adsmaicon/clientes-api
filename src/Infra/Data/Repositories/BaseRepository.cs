using Clientes.Domain.Entities;
using Clientes.Domain.Interfaces.Repositories;
using Clientes.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clientes.Infra.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly MySqlContext _mySqlContext;

        public BaseRepository(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }

        public async Task InsertAsync(TEntity obj)
        {
            await _mySqlContext.Set<TEntity>().AddAsync(obj);
            await _mySqlContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity obj)
        {
            _mySqlContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _mySqlContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _mySqlContext.Set<TEntity>().Remove(await SelectAsync(id));
            await _mySqlContext.SaveChangesAsync();
        }

        public async Task<IList<TEntity>> SelectAsync()
        {
            return await Task.FromResult(_mySqlContext.Set<TEntity>().ToList());
        }

        public async Task<TEntity> SelectAsync(int id)
        {
            return await _mySqlContext.Set<TEntity>().FindAsync(id);
        }
    }
}