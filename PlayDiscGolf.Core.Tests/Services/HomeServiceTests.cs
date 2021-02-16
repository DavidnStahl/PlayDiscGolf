using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using PlayDiscGolf.Infrastructure.UnitOfWork;
using PlayDiscGolf.Core.Services.Home;
using PlayDiscGolf.Core.AutoMapper;
using AutoMapper;
using PlayDiscGolf.AutoMapper.Profiles.Search;
using PlayDiscGolf.Core.Dtos.Home;
using System.Linq;
using PlayDiscGolf.Models.Models.DataModels;
using PlayDiscGolf.Core.AutoMapper.Profiles.SearchResult;

namespace PlayDiscGolf.Core.Tests.Services
{
    public class HomeServiceTests
    {
        private readonly IHomeService _sut;
        private readonly Mock<IUnitOfwork> _unitOfWorkMock = new Mock<IUnitOfwork>();
        private static IMapper _mapper;
        public HomeServiceTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new SearchResultProfiles());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _sut = new HomeService(_unitOfWorkMock.Object, _mapper);
        }

        [Fact]

        public void TypeIsArea_Should_Call_UnitOfWorkCoursesFindBy_Once()
        {
            //Arange
            var model = new SearchFormHomeDto
            {
                Country = "Sweden",
                Query = "Haninge",
                Type = "Area"
            };

            var courses = new List<Course>
            {
                new Course { Country = "Sweden", Area = "Haninge" , HolesTotal = 5}
            };

            _unitOfWorkMock.Setup(x => x.Courses.FindAllBy(x => x.Country == model.Country && x.Area.StartsWith(model.Query) && x.HolesTotal > 0)).Returns(courses);

            //Act
            var result = _sut.TypeIsArea(model);
            //Assert
            _unitOfWorkMock.Verify(x => x.Courses.FindAllBy(x => x.Country == model.Country && x.Area.StartsWith(model.Query) && x.HolesTotal > 0), Times.Once());
        }

        [Fact]

        public void TypeIsArea_Should_return_List_With_Property_Area_As_Haninge()
        {
            //Arange
            var model = new SearchFormHomeDto
            {
                Country = "Sweden",
                Query = "Haninge",
                Type = "Area"
            };

            var courses = new List<Course>
            {
                new Course { Country = "Sweden", Area = "Haninge" , HolesTotal = 5},
                new Course { Country = "Sweden", Area = "Haninge" , HolesTotal = 7}
            };

            _unitOfWorkMock.Setup(x => x.Courses.FindAllBy(x => x.Country == model.Country && x.Area.StartsWith(model.Query) && x.HolesTotal > 0)).Returns(courses);

            //Act
            var result = _sut.TypeIsArea(model);
            //Assert
            Assert.Equal(result.Select(x => x.Area).Distinct(), courses.Select(x => x.Area).Distinct());
        }

        [Fact]

        public void TypeIsCourse_Should_Call_UnitOfWorkCoursesFindBy_Once()
        {
            //Arange
            var model = new SearchFormHomeDto
            {
                Country = "Sweden",
                Query = "Rudan",
                Type = "Course"
            };

            var courses = new List<Course>
            {
                new Course { Country = "Sweden", FullName = "Rudan" , HolesTotal = 5},
                new Course { Country = "Sweden", FullName = "Rudan" , HolesTotal = 7}
            };

            _unitOfWorkMock.Setup(x => x.Courses.FindAllBy(x => x.Country == model.Country && x.FullName.StartsWith(model.Query) && x.HolesTotal > 0)).Returns(courses);

            //Act
            var result = _sut.TypeIsCourse(model);
            //Assert
            _unitOfWorkMock.Verify(x => x.Courses.FindAllBy(x => x.Country == model.Country && x.FullName.StartsWith(model.Query) && x.HolesTotal > 0), Times.Once());
        }

        [Fact]

        public void TypeIsCourse_Should_return_List_With_Property_Course_As_Rudan()
        {
            //Arange
            var model = new SearchFormHomeDto
            {
                Country = "Sweden",
                Query = "Rudan",
                Type = "Course"
            };

            var courses = new List<Course>
            {
                new Course { Country = "Sweden", FullName = "Rudan" , HolesTotal = 5},
                new Course { Country = "Sweden", FullName = "Rudan" , HolesTotal = 7}
            };

            _unitOfWorkMock.Setup(x => x.Courses.FindAllBy(x => x.Country == model.Country && x.FullName.StartsWith(model.Query) && x.HolesTotal > 0)).Returns(courses);

            //Act
            var result = _sut.TypeIsCourse(model);
            //Assert
            Assert.Equal(result.Select(x => x.Area).Distinct(), courses.Select(x => x.Area).Distinct());
        }

    }
}
