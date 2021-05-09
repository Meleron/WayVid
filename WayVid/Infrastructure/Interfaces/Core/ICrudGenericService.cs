using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayVid.Infrastructure.Interfaces.Core
{
    public interface ICrudGenericService<TEntity, TModel, TContext> : ICrudService<TModel> where TModel: class where TEntity: class where TContext : DbContext
    {
    }
}
