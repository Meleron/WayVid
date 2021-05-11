using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Extras;
using WayVid.Infrastructure.Interfaces.Service;
using WayVid.Infrastructure.Model;

namespace WayVid.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EstablishmentController : ControllerBase
    {

        private readonly IEstablishmentService establishmentService;

        public EstablishmentController(IEstablishmentService establishmentService)
        {
            this.establishmentService = establishmentService;
        }

        [HttpGet]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetModel(Guid ID)
        {
            ServiceCrudResponse<EstablishmentModel> resp = await establishmentService.GetAsync(ID, true);
            if (resp.Success && resp.Model != null)
                return Ok(resp.Model);
            return BadRequest(resp.Message);
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> InsertModel(EstablishmentModel model)
        {
            throw new NotImplementedException();
            //Claim subjectClaim = User.Claims.FirstOrDefault(claim => claim.Type == OpenIdConnectConstants.Claims.Subject);
            //model = await establishmentService.InsertAsync(model);
            //if(model != null)
            //    return CreatedAtAction(nameof(EstablishmentModel), model);
            //return BadRequest("Error inserting model");
        }

        
        [HttpGet("GetTopEstablishment")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetTopEstablishment()
        {
            ServiceCrudResponse<EstablishmentModel> resp = await establishmentService.GetTopEstablishment();
            if (!resp.Success)
                return BadRequest(resp.Message);
            return Ok(resp.Model);
        }

        [HttpPost("checkIn")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> CheckIn([FromQuery] Guid establishmentID)
        {
            ServiceCrudResponse resp = await establishmentService.CheckInAsync(establishmentID);
            if (resp.Success)
                return Ok(resp.Message);
            return BadRequest(resp.Message);
        }

        [HttpPost("checkOut")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> CheckOut([FromQuery] Guid establishmentID)
        {
            ServiceCrudResponse resp = await establishmentService.CheckOutAsync(establishmentID);
            if (resp.Success)
                return Ok(resp.Message);
            return BadRequest(resp.Message);
        }

    }
}