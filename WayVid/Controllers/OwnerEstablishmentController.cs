using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Extras;
using WayVid.Infrastructure.Interfaces.Service;
using WayVid.Infrastructure.Model;

namespace WayVid.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class OwnerEstablishmentController : ControllerBase
    {

        private readonly IOwnerEstablishmentService service;

        public OwnerEstablishmentController(IOwnerEstablishmentService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> AddOwnerEstablishment(OwnerEstablishmentModel model)
        {
            ServiceCrudResponse<OwnerEstablishmentModel> resp = await service.InsertAsync(model);
            if (!resp.Success || resp.Model == null)
                return BadRequest(resp.Message);
            return Ok(resp.Model);
        }
    }
}
