using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPICourse.Models;
using RESTfulAPICourse.Helper;
using AutoMapper;
using RESTfulAPICourse.ResourceParameters;
using CourseLibrary.API.Entities;

namespace RESTfulAPICourse.Controllers 
{
    [ApiController]
    [Route("api/authors")] // [Route("api/[controller]")] means the same thing as authors as it takes the prefix to controller and lowercases it
    public class AuthorsController : ControllerBase 
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository,
            IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        // IActionResult defines a contract that represents the result of an action method
        [HttpGet()]
        [HttpHead] // put this here to allow for the HEAD command to work as well
                   // If name isn't the same name as key in query string then must set [FromQuery(Name = "")]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery] AuthorsResourceParameters authorsResourceParameters )
        {//mainCategory isn't a complex type nor a form file or collection, and it doesn't match any parameter name from the route template, 
            // so, thanks to the ApiController attr, mainCategory will be bound from query string... can also say [FromQuery] before string

            var authors = _courseLibraryRepository.GetAuthors(authorsResourceParameters);// the interface takes care of which getAuthors() to call
            //foreach( var author in authors)
            //{
            //    authors2.Add(new AuthorDto()
            //    {
            //        Id = author.Id,
            //        Name = $"{author.FirstName} {author.LastName}",
            //        MainCategory = author.MainCategory,
            //        Age = author.DateOfBirth.GetCurrentAge()
            //    });
            //}
            // we use this to replace this for loop
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }

        // can also say {authorId:guid} to specify that the type of the id changes
        [HttpGet("{authorId}", Name = "GetAuthor")] // the {} means  this parameter changes
        public IActionResult GetAuthor(Guid authorId)
        {
            var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId);
            if (authorFromRepo != null)
            {
                return Ok(_mapper.Map<AuthorDto>(authorFromRepo));
            }
            return NotFound();
            
            
        }


        // Sect 6

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(AuthorForCreationDto author)
        {
            var authorEntity = _mapper.Map<Author>(author);
            _courseLibraryRepository.AddAuthor(authorEntity); //not yet added to db yet, but on dbContext which represents a session with the db
            _courseLibraryRepository.Save(); // for the save to happen we must do this

            var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);
            return CreatedAtRoute("GetAuthor", // route name
                new { authorId = authorToReturn.Id },  // route values
                authorToReturn); // for the response body ( so they can't see the firstName, lastName, DOB )
        }


        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST"); // this inserts an allow key with Get, options, and post being the value
            return Ok();
        }

        [HttpDelete("{authorId}")]
        public ActionResult DeleteAuthor(Guid authorId)
        {
            var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId); // we don't use the author exists method here because we need
                                                                                //that author entity to pass into the repository to delete it
            if (authorFromRepo == null)
            {
                return NotFound();
            }

            _courseLibraryRepository.DeleteAuthor(authorFromRepo);
            _courseLibraryRepository.Save();
            return NoContent();
        }
    }
}
