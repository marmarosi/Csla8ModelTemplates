using System.ComponentModel.DataAnnotations;

namespace Csla8RestApi.Models.Validations
{
    /// <summary>
    /// Specifies that a data field value is required.
    /// </summary>
    public class RequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredAttribute"/> class.
        /// </summary>
        public RequiredAttribute()
            : base()
        { }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns><c>true</c> if value is valid; otherwise, <c>false</c>.</returns>
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext
            )
        {
            // Set resource type and resource name for error message.
            this.SetErrorMessage(validationContext, "Required");

            // Validate the value.
            return base.IsValid(value, validationContext);
        }
    }
}
