using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScooterRent.WebApi.Database;
using ScooterRent.WebApi.Services;
using ScooterWebApiModels.ApiModels;

namespace ScooterRent.WebApi.Controllers
{
    [ApiController]
    public class ScooterController : ControllerBase
    {
        private readonly IScooterService _scooterService;

        public ScooterController(IScooterService scooterService)
        {
            _scooterService = scooterService;
        }


        [HttpPost]
        [Route("api/AddScooter")]
        public ActionResult AddScooter([FromBody] RequestAddScooterModel model)
        {
            _scooterService.AddScooter(model);
            return Ok();
        }

        [HttpPost]
        [Route("api/FixScooter")]
        public ActionResult FixScooter([FromBody] FixScooterModel fixScooterModel)
        {
            _scooterService.FixScooter(fixScooterModel);
            return Ok();
        }

        [HttpGet]
        [Route("api/CountOfFreeScooterAsync")]
        public ActionResult CountOfFreeScooterAsync()
        {
            return Ok(_scooterService.CountOfFreeScooterAsync());
        }

        [HttpGet]
        [Route("api/CountOfBorrowOfLoyalClients")]
        public ActionResult CountOfBorrowOfLoyalClients()
        {
            return Ok(_scooterService.CountOfBorrowOfLoyalClients());
        }

        [HttpGet]
        [Route("api/TotalTimeOfRentForScooter")]
        public ActionResult TotalTimeOfRentForScooter()
        {
            return Ok(_scooterService.TotalTimeOfRentForScooter());
        }

        [HttpGet]
        [Route("api/ShowBrokenScooters")]
        public ActionResult ShowBrokenScooters()
        {
            return Ok(_scooterService.ShowBrokenScooters());
        }
    }
}
