using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayVid.Infrastructure.Interfaces.Core
{
    public interface IRepositoryGeneric <TEntity, TContext> where TEntity: class where TContext: DbContext
    {
        public DbSet<TEntity> GetEntitySet();
        public Task<TEntity> GetAsync(Guid ID);
        public Task<TEntity> InsertAsync(TEntity entity);
        public Task<TEntity> UpdateAsync(TEntity entity);
        public Task DeleteAsync(TEntity entity);
    }
}
