﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Data.Holes;
using PlayDiscGolf.Models.DataModels;

namespace PlayDiscGolf.Data
{
    public class HoleRepository : IHoleRepository
    {
        private readonly DataBaseContext _context;
        public HoleRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<Hole> CreateHoleAsync(Hole hole)
        {
            await _context.Holes.AddAsync(hole);
            return hole;
        }

        public async Task CreateHolesAsync(List<Hole> holes)
        {
            await _context.Holes.AddRangeAsync(holes);
        }

        public void DeleteHoleAsync(Hole hole)
        {
            _context.Holes.Remove(hole);
        }

        public Hole EditHoleAsync(Hole hole)
        {
            _context.Holes.Update(hole);
            return hole;
        }

        public async Task<Hole> GetHoleByIDAsync(Guid holeID)
        {
            return  await _context.Holes.FirstOrDefaultAsync(hole => hole.HoleID == holeID);
        }

        public async Task<List<Hole>> GetHolesByCourseID(Guid courseID)
        {
            return await _context.Holes.Where(hole => hole.CourseID == courseID).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHoles(List<Hole> holes)
        {
            var getholes = _context.Holes.Where(h => h.CourseID == holes[0].CourseID);
            if(getholes != null)
            {
                _context.Holes.UpdateRange(holes);
            }
            else
            {
                await CreateHolesAsync(holes);
            }
            
        }
    }
}
