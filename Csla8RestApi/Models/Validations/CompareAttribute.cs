using System.ComponentModel.DataAnnotations;

namespace Csla8RestApi.Models.Validations
{
    /// <summary>
    /// Provides an attribute that compares two properties of a model.
    /// </summary>
    public class CompareAttribute : System.ComponentModel.DataAnnotations.CompareAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareAttribute"/> class.
        /// </summary>
        /// <param name="otherProperty">The property to compare with the current property.</param>
        public CompareAttribute(string otherProperty)
            : base(otherProperty)
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
            this.SetErrorMessage(validationContext, "Compare");

            // Validate the value.
            return base.IsValid(value, validationContext);
        }
    }
}