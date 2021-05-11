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
using WayVid.Infrastructure.Enum;
using WayVid.Infrastructure.Extras;
using WayVid.Infrastructure.Interfaces.Core;
using WayVid.Infrastructure.Interfaces.Repository;
using WayVid.Infrastructure.Interfaces.Service;
using WayVid.Infrastructure.Model;

namespace WayVid.Service
{
    public class EstablishmentService : CrudGenericService<Establishment, EstablishmentModel, ApiDbContext>, IEstablishmentService
    {

        private readonly IRepositoryGeneric<Establishment, ApiDbContext> repository;
        private readonly IVisitLogItemRepository visitLogItemRepository;
        private readonly IVisitorRepository visitorRepository;
        private readonly IOwnerEstablishmentService ownerEstablishmentService;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public EstablishmentService(IRepositoryGeneric<Establishment, ApiDbContext> repository,
            IVisitLogItemRepository visitLogItemRepository,
            IMapper mapper,
            IHttpContextAccessor contextAccessor,
            IUserService userService,
            IVisitorRepository visitorRepository,
            IOwnerEstablishmentService ownerEstablishmentService) : base(repository, mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.contextAccessor = contextAccessor;
            this.userService = userService;
            this.ownerEstablishmentService = ownerEstablishmentService;
            this.visitLogItemRepository = visitLogItemRepository;
            this.visitorRepository = visitorRepository;
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
            ServiceCrudResponse<OwnerEstablishmentModel> ownerEstModel = await ownerEstablishmentService.GetTopOwnerEstablishmentForOwner(resp.Model.OwnerID.Value);
            if (!ownerEstModel.Success)
                return ErrorResponse("Error receiving owner");
            return SuccessResponse(ownerEstModel.Model.Establishment);
        }

        public async Task<ServiceCrudResponse> CheckInAsync(Guid establishmentID)
        {
            Claim subjectClaim = contextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == OpenIdConnectConstants.Claims.Subject);
            if (subjectClaim == null || subjectClaim.Value == "")
                return new ServiceCrudResponse(false, "User not found");
            Visitor visitor = await visitorRepository.GetVisitorByUserID(Guid.Parse(subjectClaim.Value));
            if(visitor == null)
                return new ServiceCrudResponse(false, "Visitor for user not found");
            VisitLogItem entity = await visitLogItemRepository.GetLastLogByVisitorID(visitor.ID);
            if(entity != null && entity.Status == VisitLogItemStatus.Active)
            {
                entity.ExitedOn = DateTimeOffset.Now;
                entity.Status = VisitLogItemStatus.ForciblyClosed;
                entity = await visitLogItemRepository.UpdateAsync(entity);
            }
            entity = await visitLogItemRepository.InsertAsync(new VisitLogItem
            {
                VisitorID = visitor.ID,
                EstablishmentID = establishmentID,
                Status = VisitLogItemStatus.Active
            });
            if (entity == null)
                return new ServiceCrudResponse(false, "Error inserting visit log");
            return new ServiceCrudResponse(true, "");
        }

        public async Task<ServiceCrudResponse> CheckOutAsync(Guid establishmentID)
        {
            Claim subjectClaim = contextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == OpenIdConnectConstants.Claims.Subject);
            // берём из репы последний лог для юзера
            // и апдейтим его
            // 
            if (subjectClaim == null || subjectClaim.Value == "")
                return new ServiceCrudResponse(false, "User not found");
            Visitor visitor = await visitorRepository.GetVisitorByUserID(Guid.Parse(subjectClaim.Value));
            if (visitor == null)
                return new ServiceCrudResponse(false, "Visitor for user not found");
            VisitLogItem entity = await visitLogItemRepository.GetLastLogByVisitorID(visitor.ID);
            if (entity == null || entity.Status == VisitLogItemStatus.Closed || entity.Status == VisitLogItemStatus.ForciblyClosed)
            {
                entity = await visitLogItemRepository.InsertAsync(new VisitLogItem
                {
                    VisitorID = visitor.ID,
                    EstablishmentID = establishmentID,
                    Status = VisitLogItemStatus.Closed
                });
            } else
            {
                entity.ExitedOn = DateTimeOffset.Now;
                entity.Status = VisitLogItemStatus.Closed;
                entity = await visitLogItemRepository.UpdateAsync(entity);
            }
            if (entity == null)
                return new ServiceCrudResponse(false, "Error updating visit log");
            return new ServiceCrudResponse(true, "");
        }
    }
}
