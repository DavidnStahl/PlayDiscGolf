using PlayDiscGolf.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayDiscGolf.Data.Locations
{
    public interface ILocationRepository
    {
        public Task SaveChangesAsync();
        public Task<Location> CreateLocationAsync(Location location);

        public Location EditLocationAsync(Location location);

        public void DeleteLocationAsync(Location location);

        public Task<Location> GetLocationByIDAsync(Guid locationID);

        public Task<List<Location>> GetLocationsByQueryAsync(string query);
    }
}
