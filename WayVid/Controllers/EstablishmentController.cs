using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
            EstablishmentModel model = await establishmentService.GetAsync(ID, true);
            if (model != null)
                return Ok(model);
            return BadRequest("Establishment not found");
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
    }
}