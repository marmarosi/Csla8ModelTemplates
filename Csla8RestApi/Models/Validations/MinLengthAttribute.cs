using System.ComponentModel.DataAnnotations;

namespace Csla8RestApi.Models.Validations
{
    /// <summary>
    /// Specifies the minimum length of array or string data allowed in a property.
    /// </summary>
    public class MinLengthAttribute : System.ComponentModel.DataAnnotations.MinLengthAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinLengthAttribute"/> class.
        /// </summary>
        /// <param name="length">The minimum allowable length of array or string data.</param>
        public MinLengthAttribute(int length)
            : base(length)
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
            this.SetErrorMessage(validationContext, "MinLength");

            // Validate the value.
            return base.IsValid(value, validationContext);
        }
    }
}
