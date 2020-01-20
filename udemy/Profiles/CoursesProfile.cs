using AutoMapper;
using CourseLibrary.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPICourse.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            // SRC below                        Dest below
            CreateMap<Course, Models.CourseDto>();
            CreateMap<Models.CourseForCreationDto, Course>();
            CreateMap<Models.CourseForUpdateDto, Course>(); // for updating a course for a specific author
            CreateMap<Course, Models.CourseForUpdateDto>();
        }
    }
}
