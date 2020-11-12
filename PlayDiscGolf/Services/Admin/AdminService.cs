
using PlayDiscGolf.Data.Locations;
using PlayDiscGolf.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly ILocationRepository _locationRepository;

        public AdminService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        public async Task<List<Location>> GetLocationsByQuery(string query)
        {
            return await _locationRepository.GetLocationsByQueryAsync(query);
        }
    }
}
