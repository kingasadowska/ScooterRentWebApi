using Microsoft.EntityFrameworkCore;
using ScooterRent.WebApi.Database;
using ScooterWebApiModels.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScooterRent.WebApi.Services
{
    public partial class ClientService : IClientService
    {
        private readonly Context _context;

        public ClientService(Context context)
        {
            _context = context;
        }

        public void RentScooter(RequestRentModel rentModel)
        {
            var findMeAsClient = _context.Clients
                .Where(el => el.FirstName == rentModel.FirstName && el.LastName == rentModel.LastName)
                .Include(el => el.Scooter)
                .FirstOrDefault();

            if (findMeAsClient == null)
            {
                findMeAsClient = _context.Clients.Add(new Client()
                {
                    FirstName = rentModel.FirstName,
                    LastName = rentModel.LastName,
                }).Entity;

                _context.SaveChanges();
            }

            if (findMeAsClient.Scooter != null)
            {
                return;
            }

            var scooterId = _context.Scooters
                                .Include(p => p.Rentals)
                                .Where(x => x.Rentals.Count == 0)
                                .Select(el => new
                                {
                                    ScooterId = (int?) el.Id
                                })
                                .FirstOrDefault()?.ScooterId ?? _context.Scooters
                                .Include(p => p.Defect)
                                .Include(p => p.Rentals)
                                .Where(x => x.Defect.DefectType != Defects.Broke)
                                .SelectMany(el => el.Rentals)
                                .Where(el => el.EndRentTime != DateTime.MaxValue && el.EndRentTime < DateTime.UtcNow.AddMinutes(-15))
                                .Select(el => new
                                {
                                    el.ScooterId
                                })
                                .FirstOrDefault()?.ScooterId;

            if (scooterId == null)
            {
                return;
            }

            findMeAsClient.ScooterId = scooterId;

            _context.Rentals.Add(new Rental()
            {
                EndRentTime = DateTime.MaxValue,
                BeginRentTime = DateTime.UtcNow,
                ScooterId = scooterId,
                ClientId = findMeAsClient.Id
            });

            _context.SaveChanges();
        }

        public void ReturnScooter(RequestRentModel rentModel)
        {
            var findMeAsClient = _context.Clients
                .Where(el => el.FirstName == rentModel.FirstName && el.LastName == rentModel.LastName)
                .Include(el => el.Scooter)
                .FirstOrDefault();

            if (findMeAsClient?.Scooter == null)
            {
                return;
            }

            var findHistoryScrooter = _context.Rentals
                .Where(el => el.ClientId == findMeAsClient.Id && el.ScooterId == findMeAsClient.ScooterId)
                .OrderByDescending(el => el.EndRentTime)
                .FirstOrDefault();

            findMeAsClient.ScooterId = null;
            findHistoryScrooter.EndRentTime = DateTime.UtcNow;

            _context.SaveChanges();
        }


        public void ReportDefect(RequestBrokenModel rentModel)
        {
            var findMeAsClient = _context.Clients
                .Where(el => el.FirstName == rentModel.FirstName && el.LastName == rentModel.LastName)
                .Include(el => el.Rentals)
                .FirstOrDefault();

            if (findMeAsClient == null || findMeAsClient.Rentals == null)
            {
                return;
            }

            var lastRentedScooterForUser = findMeAsClient.Rentals.OrderByDescending(el => el.EndRentTime).FirstOrDefault();

            if (lastRentedScooterForUser != null && (lastRentedScooterForUser.EndRentTime != DateTime.MaxValue && lastRentedScooterForUser.EndRentTime.AddMinutes(15) < DateTime.UtcNow))
            {
                return;
            }

            var scooter = _context.Scooters
                  .Include(p => p.Defect)
                  .FirstOrDefault(x => x.Id == lastRentedScooterForUser.ScooterId);

            if (scooter != null && scooter.Defect == null)
            {
                scooter.Defect = new Defect()
                {
                    DefectType = Defects.Broke,
                    Description = rentModel.AboutBroke
                };
            }
            else
            {
                if (scooter != null) scooter.Defect.Description = rentModel.AboutBroke;
            }

            _context.SaveChanges();
        }
    }
}

