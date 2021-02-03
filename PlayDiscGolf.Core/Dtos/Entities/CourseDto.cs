using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;

namespace PlayDiscGolf.Core.Dtos.Course
{
    public class CourseDto
    {
        public Guid CourseID { get; set; }

        public string Area { get; set; }

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

        public string ApiID { get; set; }

        public string ApiParentID { get; set; }

        public bool Main { get; set; }

    }
}
