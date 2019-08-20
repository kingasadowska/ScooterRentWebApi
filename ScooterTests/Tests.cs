using Microsoft.EntityFrameworkCore;
using ScooterRent.WebApi.Database;
using ScooterRent.WebApi.Services;
using ScooterWebApiModels.ApiModels;
using System;
using Xunit;

namespace ScooterTests
{
    public class Tests
    {
        private Context Context()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: new Random().Next(1000000).ToString())
                .Options;
            var context = new Context(options);
            return context;
        }

        [Fact]
        public async System.Threading.Tasks.Task Test1Async()
        {
            var context = Context();

            var clientService = new ClientService(context);
            var scooterService = new ScooterService(context);

            var test = " test";
            var SerialNumber = 1;

            scooterService.AddScooter(new RequestAddScooterModel()
            {
                SerialNumber = SerialNumber,
            });

            clientService.RentScooter(new RequestRentModel()
            {
                FirstName = test,
                LastName = test,
            });

            clientService.ReturnScooter(new RequestRentModel()
            {
                FirstName = test,
                LastName = test,
            });

            var getAllRentals = await context.Rentals.ToListAsync();

            Assert.Single(getAllRentals);

            Assert.Equal(1, await scooterService.CountOfFreeScooterAsync());

            clientService.ReportDefect(new RequestBrokenModel()
            {
                FirstName = test,
                LastName = test,
                AboutBroke = "Broke",
            });

            var isBroken = await context.Scooters.Include(el=>el.Defect).FirstOrDefaultAsync();

            Assert.Equal(Defects.Broke, isBroken.Defect.DefectType);

            clientService.ReportDefect(new RequestBrokenModel()
            {
                FirstName = test,
                LastName = test,
                AboutBroke = "Broken",
            });

            var isBroken2 = await context.Scooters.Include(el => el.Defect).FirstOrDefaultAsync();

            Assert.Equal(Defects.Broke, isBroken2.Defect.DefectType);


            scooterService.FixScooter(new FixScooterModel()
            {
                SerialNumber = SerialNumber
            });

            Assert.Equal(1, await scooterService.CountOfFreeScooterAsync());

            var countOfBorrowLoyalClients = scooterService.CountOfBorrowOfLoyalClients();

            Assert.Single(countOfBorrowLoyalClients);

            var TotalTimeOfRentForScooter = scooterService.TotalTimeOfRentForScooter();

            Assert.Single(TotalTimeOfRentForScooter);
        }
    }
}
