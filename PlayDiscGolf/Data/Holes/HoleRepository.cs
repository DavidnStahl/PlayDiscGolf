using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Data.Holes;
using PlayDiscGolf.Models.Models.DataModels;

namespace PlayDiscGolf.Data
{
    public class HoleRepository : IHoleRepository
    {
        private readonly DataBaseContext _context;
        public HoleRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task CreateHoleAsync(Hole hole) => 
            await _context.Holes.AddAsync(hole);

        public async Task CreateHolesAsync(List<Hole> holes) => 
            await _context.Holes.AddRangeAsync(holes);

        public void DeleteHoleAsync(Hole hole) => 
            _context.Holes.Remove(hole);

        public void DeleteHoles(List<Hole> holes) => 
            _context.Holes.RemoveRange(holes);

        public void EditHoleAsync(Hole hole) => 
            _context.Holes.Update(hole);

        public async Task<Hole> GetHoleByIDAsync(Guid holeID) => 
            await _context.Holes.FirstOrDefaultAsync(hole => hole.HoleID == holeID);

        public async Task<List<Hole>> GetHolesByCourseID(Guid courseID) => 
            await _context.Holes.Where(hole => hole.CourseID == courseID).OrderBy(o => o.HoleNumber).ToListAsync();

        public async Task SaveChangesAsync() => 
            await _context.SaveChangesAsync();

        public void UpdateHoles(List<Hole> holes) => 
            _context.Holes.UpdateRange(holes);
    }
}
