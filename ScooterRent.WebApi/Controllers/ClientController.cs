using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScooterRent.WebApi.Services;
using ScooterWebApiModels.ApiModels;

namespace ScooterRent.WebApi.Controllers
{
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPatch]
        [Route("api/RentScooter")]
        public ActionResult RentScooter([FromBody] RequestRentModel model)
        {
            _clientService.RentScooter(model);
            return Ok();
        }

        [HttpPatch]
        [Route("api/ReturnScooter")]
        public ActionResult ReturnScooter([FromBody]RequestRentModel rentModel)
        {
            _clientService.ReturnScooter(rentModel);
            return Ok();
        }

        [HttpPost]
        [Route("api/ReportDefect")]
        public ActionResult ReportDefect([FromBody]RequestBrokenModel rentModel)
        {
            _clientService.ReportDefect(rentModel);
            return Ok();
        }
    }
}
