using PlayDiscGolf.Infrastructure.Repository.Generic;
using PlayDiscGolf.Infrastructure.Repository.Specific.Interface;
using PlayDiscGolf.Models.Models.DataModels;

namespace PlayDiscGolf.Infrastructure.Repository.Specific
{
    public class HoleRepository : EntityRepository<Hole>, IHoleRepository
    {
        private readonly DataBaseContext _context;

        public HoleRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
