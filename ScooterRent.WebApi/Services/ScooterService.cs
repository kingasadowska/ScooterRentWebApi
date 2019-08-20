using Microsoft.EntityFrameworkCore;
using ScooterRent.WebApi.Database;
using ScooterWebApiModels.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ScooterRent.WebApi.Services.ClientService;

namespace ScooterRent.WebApi.Services
{
    public partial class ScooterService : IScooterService
    {
        private readonly Context _context;

        public ScooterService(Context context)
        {
            _context = context;
        }

        public void AddScooter(RequestAddScooterModel scooterModel)
        {
            _context.Scooters.Add(new Scooter()
            {
                SerialNumber = scooterModel.SerialNumber
            });

            _context.SaveChanges();
        }

        public void FixScooter(FixScooterModel fixScooterModel)
        {
            var scooter = _context.Scooters
                  .Include(p => p.Defect)
                  .FirstOrDefault(x => x.SerialNumber == fixScooterModel.SerialNumber);

            if (scooter?.Defect != null)
            {
                scooter.Defect.Description = "";
                scooter.Defect.DefectType = Defects.None;
            }

            _context.SaveChanges();
        }

        public IEnumerable<BrokenScooter> ShowBrokenScooters()
        {
            return _context.Scooters
                  .Include(p => p.Defect)
                  .Where(x => x.Defect.DefectType == Defects.Broke)
                  .Select(el=> new BrokenScooter
                  {
                      SerialNummbers = el.SerialNumber
                  });;
        }

        public async Task<int> CountOfFreeScooterAsync()
        {
            return await _context.Scooters
                  .Include(p => p.Defect)
                  .Where(el => el.Defect == null || el.Defect.DefectType == Defects.None)
                  .CountAsync();  
        }

        public List<HoursRentCount> CountOfBorrowOfLoyalClients()
        {
            var xxx = _context.Clients
                     .Include(el => el.Rentals).ToList();

            return _context.Clients
                      .Include(el => el.Rentals)
                      .Select(el => new HoursRentCount
                      {
                          ClientName = el.FirstName + "  " + el.LastName,
                          TotalRentsTimes = el.Rentals.Count(),
                      })
                      .OrderByDescending(el => el.TotalRentsTimes)
                      .Take(10)
                      .ToList();
        }

        public List<ScooterRentCount> TotalTimeOfRentForScooter()
        {
            return _context.Scooters
                 .Include(el => el.Rentals)
                 .Select(el => new ScooterRentCount
                 {
                     SerialNumber = el.SerialNumber,
                     TotalSeconds = el.Rentals.Select(d => new
                     {
                         SpendTime = (d.EndRentTime == DateTime.MaxValue ? DateTime.UtcNow - d.BeginRentTime : d.EndRentTime - d.BeginRentTime).TotalSeconds,
                     }).Sum(s => s.SpendTime)
                 })
                 .OrderByDescending(el => el.TotalSeconds)
                 .ToList();
        }
    }
}

