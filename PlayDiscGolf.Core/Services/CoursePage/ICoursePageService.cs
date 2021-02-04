using PlayDiscGolf.Core.Dtos.Course;
using PlayDiscGolf.Dtos;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Services.CoursePage
{
    public interface ICoursePageService
    {
        public Task<CourseInfoDto> GetCoursePageInformation(Guid courseID);
    }
}
