using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayVid.Infrastructure.Interfaces.Core
{
    public interface ICreatable
    {
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
