using RESTfulAPICourse.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPICourse.Models
{
    // abstract because we dont want this class to be used on its own, intended as an interface
    [CourseTitleMustBeDifferentFromDescription( // this is an example of a property level attribute
         ErrorMessage = "Title must be diff from desc.")]
    public abstract class CourseForManipulationDto
    {
        [Required(ErrorMessage = "required failed for title")] // Adding these here allowed for the error to be called a client 400 error
        [MaxLength(100, ErrorMessage = "max length of title failed")]
        public string Title { get; set; }

        [MaxLength(1500, ErrorMessage = "max length of desc failed")]
        public virtual string Description { get; set; } //is virtual because its inherited by two classes and one has an extra validation rule so we want to override it

        // no longer need the below code because our attribute above the class takes care of the validation

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title == Description)
        //    {
        //        yield return new ValidationResult(
        //            "The provided description should be different from the title",
        //            new[] { "CreationForCourseDto" }); // class level validation... can also set to title and description
        //    }
        //}
    }
}
