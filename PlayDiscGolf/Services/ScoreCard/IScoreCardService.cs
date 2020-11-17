﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.ScoreCard
{
    public interface IScoreCardService
    {
        public Task CreateScoreCardAsync(string courseID);

        public Task EditScoreCardAsync(string scoreCardID);

        public Task DeleteScoreCardAsync(string scoreCardID);

        public Task ClaimScoreCardAsync(string userID);

    }
}
