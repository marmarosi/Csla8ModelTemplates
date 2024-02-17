using Csla.Rules;
using Csla8RestApi.Dal;
using Csla8RestApi.Models.Validations;
using System.Runtime.Serialization;

namespace Csla8RestApi.Models
{
    /// <summary>
    /// Represents an exception thrown by a business object for failed validations.
    /// </summary>
    [Serializable]
    public class BrokenRulesException : BackendException
    {
        #region Properties

        /// <summary>
        /// Gets information about the broken validation rules.
        /// </summary>
        public List<ValidationMessage> Messages { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="messages">Information v the failed validations.</param>
        public BrokenRulesException(
            List<ValidationMessage> messages
            )
            : base()
        {
            Messages = messages;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="model">The name of the business object model.</param>
        /// <param name="property">The name of the proerty with optional prefix.</param>
        /// <param name="description">The message text.</param>
        /// <param name="severity">The rule severity, defaults to error.</param>
        public BrokenRulesException(
            string model,
            string property,
            string description,
            RuleSeverity severity = RuleSeverity.Error
            )
            : base()
        {
            var message = new ValidationMessage(model, property, description, severity);
            Messages = new List<ValidationMessage> { message };
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="info">The serialization info that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The streaming context that contains contextual information about the source or destination.</param>
        protected BrokenRulesException(
            SerializationInfo info,
            StreamingContext context
            )
            : base(info, context)
        { }

        #endregion Constructors
    }
}
