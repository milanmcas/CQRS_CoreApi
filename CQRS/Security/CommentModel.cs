using System.ComponentModel.DataAnnotations;

namespace CQRS.Security
{
    /// <summary>
    /// 1. Input Validation
    /// Validate all inputs to ensure they conform to expected patterns, lengths, and types.
    /// Use data annotations and custom validation attributes to enforce rules.
    /// Sanitizing consists of removing any unsafe characters from user inputs, 
    /// while validating will check to see if the data is in the expected format
    /// </summary>
    public class CommentModel
    {
        [Required] // triggered when ModelState.IsValid
        [StringLength(1000, MinimumLength = 1)] // triggered when ModelState.IsValid
        public string Content { get; set; }
    }
    public class CommentDetail
    {
        [SanitizeInput("Author")] // triggered when ModelState.IsValid
        public string Author { get; set; }
    }
}
