using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPICourse.Models
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid AuthorId { get; set; }
        
        // the same author dto will be returned with each course if we do the below, so we do the above
        //public AuthorDto Author { get; set; }
    }
}
