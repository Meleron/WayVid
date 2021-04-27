using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayVid.Infrastructure.Interfaces.Core
{
    public interface ICrudService<TModel> where TModel: class
    {
        public Task<TModel> GetAsync(Guid ID, bool includeDependecies = false);
        public Task<TModel> InsertAsync(TModel model);
        public Task<TModel> UpdateAsync(TModel model);
        public Task DeleteAsync(TModel model);
    }
}
