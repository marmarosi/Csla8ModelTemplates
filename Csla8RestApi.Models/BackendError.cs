using Csla8RestApi.Dal;
using System.Runtime.Serialization;
using System.Text;

namespace Csla8RestApi.Models
{
    /// <summary>
    /// Represents an error occurred on the backend.
    /// </summary>
    [Serializable]
    public class BackendError : ApplicationException
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the error type.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public new string Message { get; set; }

        /// <summary>
        /// Gets or sets the summary of error messages.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the source of error messages.
        /// </summary>
        public new string Source { get; set; }

        /// <summary>
        /// Gets or sets the summary of error messages.
        /// </summary>
        public new string StackTrace { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public BackendError() { }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="message">The message to send to the client.</param>
        public BackendError(
            string message
            )
        {
            Message = message;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="message">The message to send to the client.</param>
        /// <param name="name">The name of the error type.</param>
        public BackendError(
            string message,
            string name
            )
        {
            Message = message;
            Name = name;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="exception">The exception thrown by the backend.</param>
        /// <param name="summary">The summary of the messages.</param>
        public BackendError(
            Exception exception,
            string summary
            )
        {
            if (exception != null)
            {
                while (exception.InnerException != null)
                    exception = exception.InnerException;

                Message = exception.Message;
                Name = exception.GetType().Name;
                Summary = summary;
                Source = exception.TargetSite.DeclaringType?.FullName;
                StackTrace = exception.StackTrace;
            }
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="info">The serialization info that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The streaming context that contains contextual information about the source or destination.</param>
        protected BackendError(
            SerializationInfo info,
            StreamingContext context
            )
            : base(info, context)
        { }

        #endregion Constructors

        #region Factory Methods

        public static BackendError Evaluate(
            Exception exception,
            out int statusCode
            )
        {
            var ex = exception;
            var prefix = ">>> Web API";
            var summary = new StringBuilder();
            statusCode = 500; // StatusCodes.Status500InternalServerError

            while (ex != null)
            {
                string line = String.Format("{0} {1} * {2}", prefix, ex.GetType().Name, ex.Message);
                if (ex.Source != null)
                    line += String.Format(" [ {0} ]", ex.Source);
                summary.AppendLine(line);

                if (ex is BackendException)
                    statusCode = (ex as BackendException).StatusCode;

                ex = ex.InnerException;
                prefix = "        ";
            }
            return new BackendError(exception, summary.ToString());
        }

        #endregion
    }
}
