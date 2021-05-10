using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Interfaces.Core;

namespace WayVid.Database.Repository
{
    public class RepositoryGeneric<TEntity, TContext> : IRepositoryGeneric<TEntity, TContext> where TEntity : class where TContext : DbContext
    {

        private DbSet<TEntity> entitySet { get; set; }
        private TContext context { get; set; }

        public RepositoryGeneric(TContext context)
        {
            this.context = context;
            entitySet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetAsync(Guid ID)
        {
            return await entitySet.FindAsync(ID);
        }


        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            SetCreateInfo(entity);
            context.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            SetUpdateInfo(entity);
            var q = context.Entry(entity);
            q.State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            } catch(Exception ex)
            {
                return null;
            }
            return entity;
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entitySet.Remove(entity);
            await context.SaveChangesAsync();
        }

        private void SetCreateInfo(TEntity entity)
        {
            if (entity is IUpdateable)
            {
                ICreatable createInfo = entity as ICreatable;
                createInfo.CreatedOn = DateTimeOffset.Now;
            }
        }

        private void SetUpdateInfo(TEntity entity)
        {
            if (entity is IUpdateable)
            {
                IUpdateable updateInfo = entity as IUpdateable;
                updateInfo.UpdatedOn = DateTimeOffset.Now;
            }
        }

        public DbSet<TEntity> GetEntitySet()
        {
            return entitySet;
        }

    }
}
