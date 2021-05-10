using AspNet.Security.OpenIdConnect.Primitives;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Extras;
using WayVid.Infrastructure.Interfaces.Core;
using WayVid.Infrastructure.Interfaces.Service;
using WayVid.Infrastructure.Model;

namespace WayVid.Service
{
    public class OwnerEstablishmentService : CrudGenericService<OwnerEstablishment, OwnerEstablishmentModel, ApiDbContext>, IOwnerEstablishmentService
    {

        private readonly IRepositoryGeneric<OwnerEstablishment, ApiDbContext> repository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IUserService userService;

        public OwnerEstablishmentService(IRepositoryGeneric<OwnerEstablishment, ApiDbContext> repository,
            IMapper mapper,
            IHttpContextAccessor contextAccessor,
            IUserService userService)
            : base(repository, mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.contextAccessor = contextAccessor;
            this.userService = userService;
        }

        public override async Task<ServiceCrudResponse<OwnerEstablishmentModel>> InsertAsync(OwnerEstablishmentModel model)
        {
            Claim subjectClaim = contextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == OpenIdConnectConstants.Claims.Subject);
            if (subjectClaim == null)
                return ErrorResponse("Invlaid user");
            UserModel user = (await userService.GetAsync(Guid.Parse(subjectClaim.Value))).Model;
            if (user == null || !await userService.IsInRoleAsync(user, "Owner"))
                return ErrorResponse("Invlaid user");
            if (model.EstablishmentID != Guid.Empty || model.Establishment.ID != Guid.Empty)
                return ErrorResponse("Invlaid form data");
            model.OwnerID = user.Owner.ID;
            OwnerEstablishment entity = mapper.Map<OwnerEstablishmentModel, OwnerEstablishment>(model);
            entity = await repository.InsertAsync(entity);
            model = mapper.Map<OwnerEstablishment, OwnerEstablishmentModel>(entity);
            return SuccessResponse(model);
        }

        public async Task<ServiceCrudResponse<OwnerEstablishmentModel>> GetTopOwnerEstablishmentForOwner(Guid OwnerID)
        {
            OwnerEstablishment entity = await repository.GetEntitySet().Include(e => e.Owner).Include(e => e.Establishment).Where(e => e.OwnerID == OwnerID).FirstOrDefaultAsync();
            return SuccessResponse(mapper.Map<OwnerEstablishmentModel>(entity));
        }
    }
}
