using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Infrastructure.Repository.Generic;
using PlayDiscGolf.Infrastructure.Repository.Specific.Interface;
using PlayDiscGolf.Models.Models.DataModels;

namespace PlayDiscGolf.Infrastructure.Repository.Specific
{
    public class PlayerCardRepository : EntityRepository<PlayerCard>, IPlayerCardRepository
    {
        private readonly DataBaseContext _context;

        public PlayerCardRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
