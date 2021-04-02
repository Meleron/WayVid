using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Interfaces.Entity;
using WayVid.Infrastructure.Model;

namespace WayVid.Infrastructure.Extras
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<VisitorModel, Visitor>().ForMember(d => d.User, option => option.Ignore());
            CreateMap<Visitor, VisitorModel>().ForMember(d => d.UserName, option => option.MapFrom(s => s.User.UserName));
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
        }
    }
}
