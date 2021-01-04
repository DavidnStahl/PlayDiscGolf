using PlayDiscGolf.Infrastructure.Repository.Generic;
using PlayDiscGolf.Infrastructure.Repository.Specific.Interface;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.Infrastructure.Repository.Specific
{
    public class HoleCardRepository : EntityRepository<HoleCard>, IHoleCardRepository
    {
        private readonly DataBaseContext _context;

        public HoleCardRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
