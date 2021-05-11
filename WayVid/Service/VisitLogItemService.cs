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
    public class VisitLogItemService : CrudGenericService<VisitLogItem, VisitLogItemModel, ApiDbContext>, IVisitLogItemService
    {
        public VisitLogItemService(IRepositoryGeneric<VisitLogItem, ApiDbContext> repository,
            IMapper mapper) : base(repository, mapper)
        { }
    }
}
