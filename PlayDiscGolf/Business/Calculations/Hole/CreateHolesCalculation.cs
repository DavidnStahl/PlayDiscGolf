using PlayDiscGolf.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace PlayDiscGolf.Business.Calculations.Hole
{
    public class CreateHolesCalculation : ICreateHolesCalculation
    {
        public CreateHolesViewModel ConfigureHoles(CreateHolesViewModel model)
        {
            if (model.Holes.Count == model.NumberOfHoles) 
                return model;
            if(model.Holes.Count < model.NumberOfHoles) 
                return CreateNewHoles(model);

            return RemoveHoles(model);
        }

        private CreateHolesViewModel CreateNewHoles(CreateHolesViewModel model)
        {
            for (int i = model.Holes.Count; i < model.NumberOfHoles; i++)
                model.Holes.Add(new CourseFormViewModel.CourseHolesViewModel
                {
                    CourseID = model.CourseID,
                    HoleNumber = i + 1,
                    HoleID = Guid.NewGuid(),
                    ParValue = 1,
                    Distance = 1
                });

            return model;
        }

        private CreateHolesViewModel RemoveHoles(CreateHolesViewModel model)
        {
            var newHolesList = model.Holes;

            for (int i = model.NumberOfHoles; i < model.Holes.Count; i++) 
                newHolesList.RemoveAt(i);

            model.Holes = newHolesList;

            return model;
        }
    }
}
