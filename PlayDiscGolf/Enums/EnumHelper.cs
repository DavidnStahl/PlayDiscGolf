﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PlayDiscGolf.Enums
{
    public class EnumHelper
    {
        public enum SearchType
        {
            Area,
            Course
        }

        public enum ScoreCardViewModelSessionKey
        {
            ScoreCardViewModel
        }

        public enum PlusAndMinus
        {
            Plus,
            Minus
        }

        public enum BestRound
        {
            None
        }

        public enum AverageRound
        {
            None
        }

        public enum UserManager
        {
            User
        }

    }
}
