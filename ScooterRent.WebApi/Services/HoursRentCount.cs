using System;

namespace ScooterRent.WebApi.Services
{
    public partial class ClientService
    {
        public class HoursRentCount
        {
            public string ClientName { get; set; }
            public double TotalRentsTimes { get; set; }
        }
    }
}


