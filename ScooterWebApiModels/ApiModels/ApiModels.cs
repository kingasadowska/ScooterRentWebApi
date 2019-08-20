using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterWebApiModels.ApiModels
{
    public class RequestAddScooterModel
    {
        public int SerialNumber { get; set; }
    }

    public class RequestRentModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class RequestBrokenModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AboutBroke { get; set; }
    }

    public class FixScooterModel
    {
        public int SerialNumber { get; set; }
    }
}

