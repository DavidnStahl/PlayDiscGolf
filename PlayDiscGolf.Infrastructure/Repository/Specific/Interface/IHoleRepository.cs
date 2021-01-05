﻿using PlayDiscGolf.Infrastructure.Repository.Generic;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.Infrastructure.Repository.Specific.Interface
{
    public interface IHoleRepository : IEntityRepository<Hole>
    {
        Hole GetCourseHole(Guid courseID, int holeNumber);
    }
}
