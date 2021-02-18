using PlayDiscGolf.Infrastructure.Repository.Specific.Interface;
using System;

namespace PlayDiscGolf.Infrastructure.UnitOfWork
{
    public interface IUnitOfwork : IDisposable
    {
        ICourseRepository Courses { get; }
        IHoleCardRepository HoleCards { get; }
        IHoleRepository Holes { get; }
        IPlayerCardRepository PlayerCards { get; }
        IScoreCardRepository ScoreCards { get; }
        IFriendRepository Friends { get; }
        int Complete();
    }
}
