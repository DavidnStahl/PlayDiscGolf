using System.Collections.Generic;


namespace PlayDiscGolf.Services.Admin
{
    public interface IAdminNewCountryCourseService
    {
        List<string> GetAddedCountryCodesInCourses();
    }
}
