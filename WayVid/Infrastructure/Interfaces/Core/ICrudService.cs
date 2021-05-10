using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Extras;

namespace WayVid.Infrastructure.Interfaces.Core
{
    public interface ICrudService<TModel> where TModel : class, IModel
    {
        public Task<ServiceCrudResponse<TModel>> GetAsync(Guid ID, bool includeDependecies = false);
        public Task<ServiceCrudResponse<TModel>> InsertAsync(TModel model);
        public Task<ServiceCrudResponse<TModel>> UpdateAsync(TModel model);
        public Task<ServiceCrudResponse<TModel>> DeleteAsync(TModel model);
        public ServiceCrudResponse<TModel> ErrorResponse();
        public ServiceCrudResponse<TModel> ErrorResponse(string message);
        public ServiceCrudResponse<TModel> ErrorResponse(TModel model, string message);
        public ServiceCrudResponse<TModel> SuccessResponse(TModel model);
        public ServiceCrudResponse<TModel> SuccessResponse(TModel model, string message);
    }
}
