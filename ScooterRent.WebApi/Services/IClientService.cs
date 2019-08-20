using ScooterWebApiModels.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScooterRent.WebApi.Services
{
    public interface IClientService
    {
        void RentScooter(RequestRentModel rentModel);
        void ReturnScooter(RequestRentModel rentModel);
        void ReportDefect(RequestBrokenModel rentModel);
    }
}