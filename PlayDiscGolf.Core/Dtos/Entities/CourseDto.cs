﻿using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.Core.Dtos.ScoreCard;
using PlayDiscGolf.Dtos;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Dtos.Course
{
    public class CourseDto
    {
        public Guid CourseID { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public int TotalHoles { get; set; }

        public int TotalParValue { get; set; }

        public int TotalDistance { get; set; }

        public int NumberOfRounds { get; set; }

        public string AverageRound { get; set; }

        public string BestRound { get; set; }

        public List<Hole> Holes = null;

        public List<ScoreCardDto> ScoreCards = null;
    }
}
