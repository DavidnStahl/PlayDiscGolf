using PlayDiscGolf.Infrastructure.Repository.Specific;
using PlayDiscGolf.Infrastructure.Repository.Specific.Interface;
using PlayDiscGolf.Models.Models.DataModels;

namespace PlayDiscGolf.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfwork
    {
        private readonly DataBaseContext _context;
        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
            HoleCards = new HoleCardRepository(_context);
            Holes = new HoleRepository(_context);
            PlayerCards = new PlayerCardRepository(_context);
            Courses = new CourseRepository(_context);
            ScoreCards = new ScoreCardRepository(_context);
            Friends = new FriendRepository(_context);
        }

        public IHoleCardRepository HoleCards { get; private set; }
        public IHoleRepository Holes { get; private set; }
        public IPlayerCardRepository PlayerCards { get; private set; }
        public IScoreCardRepository ScoreCards { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public IFriendRepository Friends { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
