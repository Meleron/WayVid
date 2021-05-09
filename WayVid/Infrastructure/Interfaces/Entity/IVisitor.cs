using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Interfaces.Core;

namespace WayVid.Infrastructure.Interfaces.Entity
{
    public interface IVisitor: IPerson, IEntity
    {
    }
}
