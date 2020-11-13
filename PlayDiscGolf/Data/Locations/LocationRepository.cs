using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Data.Locations;
using PlayDiscGolf.Models.DataModels;

namespace PlayDiscGolf.Data
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DataBaseContext _context;
        public LocationRepository(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<Location> CreateLocationAsync(Location location)
        {
            await _context.Locations.AddAsync(location);
            return location;
        }

        public void DeleteLocationAsync(Location location)
        {
            _context.Remove(location);
        }

        public  Location EditLocationAsync(Location location)
        {
            _context.Locations.Update(location);
            return location;
        }

        public async Task<Location> GetLocationByIDAsync(Guid locationID)
        {
            return await _context.Locations.FirstOrDefaultAsync(location => location.LocationID == locationID);
        }

        public async Task<List<Location>> GetLocationsByQueryAsync(string query)
        {
            var locations = await _context.Locations.Where(location => location.Name.StartsWith(query)).ToListAsync();
            return locations;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
