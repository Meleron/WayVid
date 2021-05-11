using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Extras;
using WayVid.Infrastructure.Interfaces.Core;
using WayVid.Infrastructure.Interfaces.Repository;
using WayVid.Infrastructure.Interfaces.Service;
using WayVid.Infrastructure.Model;

namespace WayVid.Service
{
    public class VisitorService : CrudGenericService<Visitor, VisitorModel, ApiDbContext>, IVisitorService
    {

        private readonly IVisitorRepository repository;
        private readonly IMapper mapper;

        public VisitorService(IVisitorRepository repository,
            IMapper mapper)
            : base(repository, mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
    }
}
