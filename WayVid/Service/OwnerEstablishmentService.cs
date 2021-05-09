using AspNet.Security.OpenIdConnect.Primitives;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Interfaces.Core;
using WayVid.Infrastructure.Interfaces.Service;
using WayVid.Infrastructure.Model;

namespace WayVid.Service
{
    public class OwnerEstablishmentService : IOwnerEstablishmentService
    {

        private readonly IRepositoryGeneric<OwnerEstablishment, ApiDbContext> repository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IUserService userService;
        private readonly IEstablishmentService establishmentService;


        public OwnerEstablishmentService(IRepositoryGeneric<OwnerEstablishment,
            ApiDbContext> repository,
            IMapper mapper,
            IHttpContextAccessor contextAccessor,
            IUserService userService,
            IEstablishmentService establishmentService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.contextAccessor = contextAccessor;
            this.userService = userService;
            this.establishmentService = establishmentService;
        }

        public async Task DeleteAsync(OwnerEstablishmentModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<OwnerEstablishmentModel> GetAsync(Guid ID, bool includeDependecies = false)
        {
            throw new NotImplementedException();
        }

        public async Task<OwnerEstablishmentModel> InsertAsync(OwnerEstablishmentModel model)
        {
            Claim subjectClaim = contextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == OpenIdConnectConstants.Claims.Subject);
            if (subjectClaim == null)
                return null;
            UserModel user = await userService.GetAsync(Guid.Parse(subjectClaim.Value));
            if (user == null || user.Owner == null)
                return null;
            if (model.EstablishmentID != Guid.Empty || model.Establishment.ID != Guid.Empty)
                return null;
            model.Owner = user.Owner;
            OwnerEstablishment entity = mapper.Map<OwnerEstablishmentModel, OwnerEstablishment>(model);
            entity = await repository.InsertAsync(entity);
            return mapper.Map<OwnerEstablishment, OwnerEstablishmentModel>(entity);
        }

        public async Task<OwnerEstablishmentModel> UpdateAsync(OwnerEstablishmentModel model)
        {
            throw new NotImplementedException();
        }
    }
}
