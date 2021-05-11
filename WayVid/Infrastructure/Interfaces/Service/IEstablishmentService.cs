using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Extras;
using WayVid.Infrastructure.Interfaces.Core;
using WayVid.Infrastructure.Model;

namespace WayVid.Infrastructure.Interfaces.Service
{
    public interface IEstablishmentService : ICrudGenericService<Establishment, EstablishmentModel, ApiDbContext>
    {
        public Task<bool> CheckIfEsistsAsync(Guid ID);
        public Task<ServiceCrudResponse<EstablishmentModel>> GetTopEstablishment();
        public Task<ServiceCrudResponse> CheckInAsync(Guid establishmentID);
        public Task<ServiceCrudResponse> CheckOutAsync(Guid establishmentID);
    }
}
