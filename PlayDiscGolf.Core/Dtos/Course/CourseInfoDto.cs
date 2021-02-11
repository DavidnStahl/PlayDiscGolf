using PlayDiscGolf.Core.Dtos.Cards;
using PlayDiscGolf.Core.Dtos.Entities;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;


namespace PlayDiscGolf.Core.Dtos.Course
{
    public class CourseInfoDto
    {
        public Guid CourseID { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public int TotalHoles { get; set; }

        public int TotalParValue { get; set; }

        public int TotalDistance { get; set; }

        public int NumberOfRounds { get; set; }

        public string AverageRound { get; set; }

        public string BestRound { get; set; }

        public List<HoleDto> Holes { get; set; }

        public List<ScoreCardDto> ScoreCards { get; set; }
    }
}
