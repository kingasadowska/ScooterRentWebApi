using System.Collections.Generic;
using System.Threading.Tasks;
using ScooterWebApiModels.ApiModels;
using static ScooterRent.WebApi.Services.ClientService;
using static ScooterRent.WebApi.Services.ScooterService;

namespace ScooterRent.WebApi.Services
{
    public interface IScooterService
    {
        void AddScooter(RequestAddScooterModel scooterModel);
        void FixScooter(FixScooterModel fixScooterModel);
        Task<int> CountOfFreeScooterAsync();
        List<HoursRentCount> CountOfBorrowOfLoyalClients();
        List<ScooterRentCount> TotalTimeOfRentForScooter();
        IEnumerable<BrokenScooter> ShowBrokenScooters();
    }
}