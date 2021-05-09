using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Interfaces.Core;
using WayVid.Infrastructure.Interfaces.Service;
using WayVid.Infrastructure.Model;

namespace WayVid.Service
{
    public class EstablishmentService : IEstablishmentService
    {

        IRepositoryGeneric<Establishment, ApiDbContext> establishmentRepository;
        IMapper mapper;

        public EstablishmentService(IRepositoryGeneric<Establishment, ApiDbContext> establishmentRepository, IMapper mapper)
        {
            this.establishmentRepository = establishmentRepository;
            this.mapper = mapper;
        }

        public async Task<EstablishmentModel> GetAsync(Guid ID, bool includeDependecies = false)
        {
            Establishment entity = await establishmentRepository.GetAsync(ID);
            return mapper.Map<Establishment, EstablishmentModel>(entity);
        }

        public async Task<EstablishmentModel> InsertAsync(EstablishmentModel model)
        {
            Establishment entity = mapper.Map<EstablishmentModel, Establishment>(model);
            entity = await establishmentRepository.InsertAsync(entity);
            return mapper.Map<Establishment, EstablishmentModel>(entity);
        }

        public async Task<bool> CheckIfEsistsAsync(Guid ID)
        {
            return (await establishmentRepository.GetAsync(ID)) != null;
        }

        public async Task<EstablishmentModel> UpdateAsync(EstablishmentModel model)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(EstablishmentModel model)
        {
            throw new NotImplementedException();
        }
    }
}
