using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Extras;
using WayVid.Infrastructure.Interfaces.Core;

namespace WayVid.Service
{
    public class CrudGenericService<TEntity, TModel, TContext> : ICrudGenericService<TEntity, TModel, TContext> where TEntity : class where TModel : class, IModel where TContext : DbContext
    {

        IRepositoryGeneric<TEntity, TContext> repository;
        IMapper mapper;

        public CrudGenericService(IRepositoryGeneric<TEntity, TContext> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public virtual async Task<ServiceCrudResponse<TModel>> GetAsync(Guid ID, bool includeDependecies = false)
        {
            TModel model;
            try
            {
                TEntity entity = await repository.GetAsync(ID);
                model = mapper.Map<TModel>(entity);
            }
            catch (Exception ex)
            {
                return new ServiceCrudResponse<TModel>(null, false, ex.Message);
            }
            return new ServiceCrudResponse<TModel>(model, true, "Get success");
        }

        public virtual async Task<ServiceCrudResponse<TModel>> InsertAsync(TModel model)
        {
            try
            {
                TEntity entity = mapper.Map<TEntity>(model);
                entity = await repository.InsertAsync(entity);
                model = mapper.Map<TModel>(entity);
            }
            catch (Exception ex)
            {
                return new ServiceCrudResponse<TModel>(null, false, ex.Message);
            }
            return new ServiceCrudResponse<TModel>(model, true, "Insert success");
        }

        public virtual async Task<ServiceCrudResponse<TModel>> UpdateAsync(TModel model)
        {
            try
            {
                TEntity entity = mapper.Map<TEntity>(model);
                entity = await repository.UpdateAsync(entity);
                model = mapper.Map<TModel>(entity);
            }
            catch (Exception ex)
            {
                return new ServiceCrudResponse<TModel>(null, false, ex.Message);
            }
            return new ServiceCrudResponse<TModel>(model, true, "Update success");
        }

        public virtual async Task<ServiceCrudResponse<TModel>> DeleteAsync(TModel model)
        {
            try
            {
                TEntity entity = mapper.Map<TEntity>(model);
                await repository.DeleteAsync(entity);
            }
            catch (Exception ex)
            {
                return new ServiceCrudResponse<TModel>(null, false, ex.Message);
            }
            return new ServiceCrudResponse<TModel>(model, true, "Delete success");
        }

        public virtual ServiceCrudResponse<TModel> ErrorResponse()
        {
            return new ServiceCrudResponse<TModel>(null, false, "");
        }

        public virtual ServiceCrudResponse<TModel> ErrorResponse(string message)
        {
            return new ServiceCrudResponse<TModel>(null, false, message);
        }

        public virtual ServiceCrudResponse<TModel> ErrorResponse(TModel model, string message)
        {
            return new ServiceCrudResponse<TModel>(model, false, message);
        }

        public virtual ServiceCrudResponse<TModel> SuccessResponse(TModel model)
        {
            return new ServiceCrudResponse<TModel>(model, true, "");
        }
        public virtual ServiceCrudResponse<TModel> SuccessResponse(TModel model, string message)
        {
            return new ServiceCrudResponse<TModel>(model, true, message);
        }
    }
}
