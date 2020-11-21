using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PlayDiscGolf.Business.Session;
using PlayDiscGolf.Data.Cards.Holes;
using PlayDiscGolf.Data.Cards.Players;
using PlayDiscGolf.Data.Cards.Scores;
using PlayDiscGolf.Data.Courses;
using PlayDiscGolf.Data.Holes;
using PlayDiscGolf.Enums;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Business.ViewModelBuilder.HoleCard
{
    public class HoleCardViewModelBuilder : IHoleCardViewModelBuilder
    {
        private readonly IHoleRepository _holeRepository;

        public HoleCardViewModelBuilder(IHoleRepository holeRepository)
        {
            _holeRepository = holeRepository;
        }
        public async Task<List<HoleCardViewModel>> BuildHoleCardsForCourseAsync(Guid courseID, Guid playerCardID)
        {
            List<HoleCardViewModel> holeCardViewModelList = new List<HoleCardViewModel>();

            List<Hole> holes = await _holeRepository.GetHolesByCourseIDAsync(courseID);

            for (int i = 0; i < holes.Count; i++)
            {
                holeCardViewModelList.Add(new HoleCardViewModel
                {
                    HoleCardID = Guid.NewGuid(),
                    HoleNumber = i + 1,
                    PlayerCardID = playerCardID,
                    Score = 0
                });
            }

            return holeCardViewModelList;
        }
    }
}
