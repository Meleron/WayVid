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
using WayVid.Infrastructure.Extras;
using WayVid.Infrastructure.Interfaces.Core;
using WayVid.Infrastructure.Interfaces.Service;
using WayVid.Infrastructure.Model;

namespace WayVid.Service
{
    public class EstablishmentService : CrudGenericService<Establishment, EstablishmentModel, ApiDbContext>, IEstablishmentService
    {

        private readonly IRepositoryGeneric<Establishment, ApiDbContext> repository;
        private readonly IOwnerEstablishmentService ownerEstablishmentService;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public EstablishmentService(IRepositoryGeneric<Establishment, ApiDbContext> repository,
            IMapper mapper,
            IHttpContextAccessor contextAccessor,
            IUserService userService,
            IOwnerEstablishmentService ownerEstablishmentService) : base(repository, mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.contextAccessor = contextAccessor;
            this.userService = userService;
            this.ownerEstablishmentService = ownerEstablishmentService;
        }

        public async Task<bool> CheckIfEsistsAsync(Guid ID)
        {
            return (await repository.GetAsync(ID)) != null;
        }

        public async Task<ServiceCrudResponse<EstablishmentModel>> GetTopEstablishment()
        {
            //Claim subjectClaim = contextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == OpenIdConnectConstants.Claims.Subject);
            ServiceCrudResponse<UserModel> resp = await userService.GetUserModelByPrincipalAsync(contextAccessor.HttpContext.User);
            if (!resp.Success || resp.Model == null)
                return ErrorResponse("User not found");
            if (resp.Model.OwnerID == null)
                return ErrorResponse("User has no owner");
            ServiceCrudResponse<OwnerEstablishmentModel> ownerEstModel= await ownerEstablishmentService.GetTopOwnerEstablishmentForOwner(resp.Model.OwnerID.Value);
            if (!ownerEstModel.Success || ownerEstModel.Model == null)
                return ErrorResponse("Error receiving owner");
            return SuccessResponse(ownerEstModel.Model.Establishment);
        }
    }
}
