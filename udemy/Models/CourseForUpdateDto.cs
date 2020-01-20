using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPICourse.Models
{
    public class CourseForUpdateDto : CourseForManipulationDto
    {
        [Required(ErrorMessage = "required failed for desc")] // additional validation is added onto the implemented class
        public override string Description { get => base.Description; set => base.Description = value; }
    }
}
