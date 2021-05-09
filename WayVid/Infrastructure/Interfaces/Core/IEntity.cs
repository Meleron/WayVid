﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayVid.Infrastructure.Interfaces.Core
{
    public interface IEntity : IEntityBase, ICreatable, IUpdateable
    { }
}
