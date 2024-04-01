using Csla8RestApi.Dal;
using System.Text;

namespace Csla8RestApi.Models
{
    /// <summary>
    /// Represents an error occurred on the backend.
    /// </summary>
#pragma warning disable S3376 // Attribute, EventArgs, and Exception type names should end with the type being extended
    public class BackendError : ApplicationException
#pragma warning restore S3376 // Attribute, EventArgs, and Exception type names should end with the type being extended
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the error type.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public new string? Message { get; set; }

        /// <summary>
        /// Gets or sets the summary of error messages.
        /// </summary>
        public string? Summary { get; set; }

        /// <summary>
        /// Gets or sets the source of error messages.
        /// </summary>
        public new string? Source { get; set; }

        /// <summary>
        /// Gets or sets the summary of error messages.
        /// </summary>
        public new string? StackTrace { get; set; }

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
            if (exception is not null)
            {
                while (exception.InnerException is not null)
                    exception = exception.InnerException;

                Message = exception.Message;
                Name = exception.GetType().Name;
                Summary = summary;
                Source = exception.TargetSite?.DeclaringType?.FullName;
                StackTrace = exception.StackTrace;
            }
        }

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

            while (ex is not null)
            {
                string line = String.Format("{0} {1} * {2}", prefix, ex.GetType().Name, ex.Message);
                if (ex.Source is not null)
                    line += String.Format(" [ {0} ]", ex.Source);
                summary.AppendLine(line);

                if (ex is BackendException)
                    statusCode = (ex as BackendException)!.StatusCode;

                ex = ex.InnerException;
                prefix = "        ";
            }
            return new BackendError(exception, summary.ToString());
        }

        #endregion
    }
}
