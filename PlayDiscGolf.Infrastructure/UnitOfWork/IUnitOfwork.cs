using PlayDiscGolf.Infrastructure.Repository.Specific.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.Infrastructure.UnitOfWork
{
    public interface IUnitOfwork : IDisposable
    {
        ICourseRepository Courses { get; }
        IHoleCardRepository HoleCards { get; }
        IHoleRepository Holes { get; }
        IPlayerCardRepository PlayerCards { get; }
        IScoreCardRepository ScoreCards { get; }
        int Complete();
    }
}
