using APBD_5_S20414.Data;
using APBD_5_S20414.Models;
using APBD_5_S20414.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace APBD_5_S20414.Services
{
    public interface ITripsService
    {
        Task<IEnumerable<Trip>> GetTripsWithAdditionalData();
        Task<IEnumerable<TripWithAdditionalData>> GetTripWithAdditionalData2();
        Task<bool> AssignClientToTrip(ClientToTripRequest request);
        Task<bool> RemoveClient(int clientId);
    }
    public class TripsService : ITripsService
    {
        private readonly ApbdContext _context;
        public TripsService(ApbdContext context)
        {

            _context = context;

        }
        public async Task<IEnumerable<Trip>> GetTripsWithAdditionalData()
        {
            return await _context.Trips
                .Include(e => e.IdCountries)
                .Include(e => e.ClientTrips)
                .ThenInclude(e => e.IdClientNavigation)
                .ToListAsync();
        }

        public async Task<IEnumerable<TripWithAdditionalData>> GetTripWithAdditionalData2()
        {
            var trips = await _context.Trips.Select(e => new TripWithAdditionalData
            {
                Description = e.Description,
                Countries = e.IdCountries.Select(c => new CountryName { Name = c.Name })
            }).ToListAsync();
            return trips;
        }

        public async Task<bool> AssignClientToTrip(ClientToTripRequest request)
        {
            var trip = await _context.Trips.FindAsync(request.TripID);
            if (trip == null)
            {
                return false; // zwróć false, jeśli wycieczka nie istnieje
            }

            var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == request.Pesel);
            if (existingClient == null)
            {
                var newClient = new Client
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Telephone = request.Telephone,
                    Pesel = request.Pesel
                };

                _context.Clients.Add(newClient);
                await _context.SaveChangesAsync();

                var clientTrip = new ClientTrip
                {
                    IdClient = newClient.IdClient,
                    IdTrip = request.TripID,
                    RegisteredAt = DateTime.Now
                };

                _context.ClientTrips.Add(clientTrip);
                await _context.SaveChangesAsync();
            }
            else
            {
                var isClientAssignedToTrip = await _context.ClientTrips
                    .AnyAsync(ct => ct.IdClient == existingClient.IdClient && ct.IdTrip == request.TripID);

                if (isClientAssignedToTrip)
                {
                    return false; // zwróć false, jeśli klient jest już przypisany do wycieczki
                }

                var clientTrip = new ClientTrip
                {
                    IdClient = existingClient.IdClient,
                    IdTrip = request.TripID,
                    RegisteredAt = DateTime.Now
                };

                _context.ClientTrips.Add(clientTrip);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> RemoveClient(int clientId)
        {
            Console.WriteLine("Weszło w remove");
            Console.WriteLine(clientId);
            var client = await _context.Clients.FindAsync(clientId);
            if (client == null)
            {
                return false; // zwróć false, jeśli klient o podanym ID nie istnieje
            }

            var isClientAssignedToTrip = await _context.ClientTrips.AnyAsync(ct => ct.IdClient == client.IdClient);
            if (isClientAssignedToTrip)
            {
                Console.WriteLine("Klient o podanym ID {" + clientId +  "} ma już przypisaną wycieczkę");
                return false; // zwróć false, jeśli klient ma przypisaną wycieczkę
            }

            Console.WriteLine(client);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
