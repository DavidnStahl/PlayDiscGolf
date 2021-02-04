using PlayDiscGolf.Infrastructure.Repository.Generic;
using PlayDiscGolf.Infrastructure.Repository.Specific.Interface;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.Infrastructure.Repository.Specific
{
    public class FriendRepository : EntityRepository<Friend>, IFriendRepository
    {
        private readonly DataBaseContext _context;

        public FriendRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
