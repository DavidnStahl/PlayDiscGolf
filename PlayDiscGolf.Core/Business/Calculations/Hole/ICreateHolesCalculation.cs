using PlayDiscGolf.Core.Dtos.AdminCourse;

namespace PlayDiscGolf.Core.Business.Calculations.Hole
{
    public interface ICreateHolesCalculation
    {
        public CreateHolesDto ConfigureHoles(CreateHolesDto model);

    }
}
