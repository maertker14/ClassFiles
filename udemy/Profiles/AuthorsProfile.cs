using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper; // for create map function and Profile interface extension
using CourseLibrary.API.Entities; // for Author
using RESTfulAPICourse.Helper; 

namespace RESTfulAPICourse.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Author, Models.AuthorDto>()
                .ForMember(
                    dest => dest.Name, // for each member name, we are mapping the first and last
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
                ).ForMember(
                    dest => dest.Age, // for each member name, we are mapping the first and last
                    opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge())
                );

            CreateMap<Models.AuthorForCreationDto, Author>();
        }
    }
}
