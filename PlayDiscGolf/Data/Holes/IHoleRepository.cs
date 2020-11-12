using PlayDiscGolf.Models.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayDiscGolf.Data.Holes
{
    public interface IHoleRepository
    {
        public Task SaveChangesAsync();
        public Task<Hole> CreateHoleAsync(Hole hole);

        public Hole EditHoleAsync(Hole hole);

        public void DeleteHoleAsync(Hole hole);

        public Task<Hole> GetHoleByIDAsync(int holeID);

        public Task<List<Hole>> GetHolesByCourseID(int courseID);
    }
}
