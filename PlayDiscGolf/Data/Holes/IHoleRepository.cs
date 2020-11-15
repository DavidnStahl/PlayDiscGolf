using PlayDiscGolf.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayDiscGolf.Data.Holes
{
    public interface IHoleRepository
    {
        public Task SaveChangesAsync();
        public Task<Hole> CreateHoleAsync(Hole hole);

        public Task CreateHolesAsync(List<Hole> holes);

        public Hole EditHoleAsync(Hole hole);

        public void DeleteHoleAsync(Hole hole);

        public Task<Hole> GetHoleByIDAsync(Guid holeID);

        public Task<List<Hole>> GetHolesByCourseID(Guid courseID);

        public Task UpdateHoles(List<Hole> holes);
    }
}
