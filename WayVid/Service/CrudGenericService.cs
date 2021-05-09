using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Interfaces.Core;

namespace WayVid.Service
{
    public class CrudGenericService<TEntity, TModel, TContext> : ICrudGenericService<TEntity, TModel, TContext> where TEntity : class where TModel : class where TContext : DbContext
    {

        IRepositoryGeneric<TEntity, TContext> repository;
        IMapper mapper;

        public CrudGenericService(IRepositoryGeneric<TEntity, TContext> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public virtual async Task DeleteAsync(TModel model)
        {
            TEntity entity = mapper.Map<TEntity>(model);
            await repository.DeleteAsync(entity);
        }

        public virtual async Task<TModel> GetAsync(Guid ID, bool includeDependecies = false)
        {
            TEntity entity = await repository.GetAsync(ID);
            return mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> InsertAsync(TModel model)
        {
            TEntity entity = mapper.Map<TEntity>(model);
            entity = await repository.InsertAsync(entity);
            return mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> UpdateAsync(TModel model)
        {
            TEntity entity = mapper.Map<TEntity>(model);
            entity = await repository.UpdateAsync(entity);
            return mapper.Map<TModel>(entity);
        }
    }
}
