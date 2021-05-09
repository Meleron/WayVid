using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Interfaces.Core;
using WayVid.Infrastructure.Model;

namespace WayVid.Infrastructure.Interfaces.Service
{
    public interface IEstablishmentService : ICrudService<EstablishmentModel>
    {
        public Task<bool> CheckIfEsistsAsync(Guid ID);
    }
}
