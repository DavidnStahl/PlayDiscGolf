using PlayDiscGolf.Core.Dtos.AdminCourse;
using PlayDiscGolf.Core.Dtos.Entities;
using System;

namespace PlayDiscGolf.Core.Business.Calculations.Hole
{
    public class CreateHolesCalculation : ICreateHolesCalculation
    {
        public CreateHolesDto ConfigureHoles(CreateHolesDto model)
        {
            if (model.Holes.Count == model.NumberOfHoles) 
                return model;
            if(model.Holes.Count < model.NumberOfHoles) 
                return CreateNewHoles(model);

            return RemoveHoles(model);
        }

        private CreateHolesDto CreateNewHoles(CreateHolesDto model)
        {
            for (int i = model.Holes.Count; i < model.NumberOfHoles; i++)
                model.Holes.Add(new HoleDto
                {
                    HoleNumber = i + 1,
                    HoleID = Guid.NewGuid(),
                    ParValue = 1,
                    Distance = 1
                });

            return model;
        }

        private CreateHolesDto RemoveHoles(CreateHolesDto model)
        {
            var newHolesList = model.Holes;

            for (int i = model.NumberOfHoles; i < model.Holes.Count; i++) 
                newHolesList.RemoveAt(i);

            model.Holes = newHolesList;

            return model;
        }
    }
}
