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
    public class VisitorService: IVisitorService
    {

        IRepositoryGeneric<Visitor, ApiDbContext> repository;
        IMapper mapper;

        public VisitorService(IRepositoryGeneric<Visitor, ApiDbContext> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<VisitorModel> GetAsync(Guid ID, bool includeDependencies = false)
        {
            Visitor entity = await repository.GetAsync(ID);
            return mapper.Map<VisitorModel>(entity);
        }

        public async Task<VisitorModel> InsertAsync(VisitorModel model)
        {
            Visitor entity = mapper.Map<Visitor>(model);
            entity = await repository.InsertAsync(entity);
            return mapper.Map<VisitorModel>(entity);
        }

        public async Task<VisitorModel> UpdateAsync(VisitorModel model)
        {
            Visitor entity = mapper.Map<Visitor>(model);
            entity = await repository.UpdateAsync(entity);
            return mapper.Map<VisitorModel>(entity);
        }

        public async Task DeleteAsync(VisitorModel model)
        {
            Visitor entity = mapper.Map<Visitor>(model);
            await repository.DeleteAsync(entity);
        }

    }
}
