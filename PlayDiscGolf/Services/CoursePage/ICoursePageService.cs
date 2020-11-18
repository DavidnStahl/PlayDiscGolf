using PlayDiscGolf.Dtos;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Services.CoursePage
{
    public interface ICoursePageService
    {
        public Task<CourseInfoDto> GetCoursePageInformationAsync(Guid courseID);
    }
}
