using System;

namespace ScooterRent.WebApi.Services
{
    public partial class ClientService
    {
        public class ScooterRentCount
        {
            public int SerialNumber { get; set; }
            public double TotalSeconds { get; set; }
        }
    }
}

