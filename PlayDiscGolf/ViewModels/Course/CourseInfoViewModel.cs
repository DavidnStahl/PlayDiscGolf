using PlayDiscGolf.Core.Dtos.Entities;
using PlayDiscGolf.Dtos;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.ViewModels.Course
{
    public class CourseInfoViewModel
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
        public List<HoleViewModel> Holes { get; set; }
        public List<ScoreCardViewModel> ScoreCards { get; set; }
    }
}
