using System.Net;

namespace Csla8RestApi.Dal
{
    /// <summary>
    /// This class is the base exception that is thrown when a
    /// non-fatal application error occurs in the application.
    /// </summary>
    public class BackendException : ApplicationException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public BackendException()
            : base()
        {
            Init(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public BackendException(
            string message
            )
            : base(message)
        {
            Init(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.
        /// If the innerException parameter is not a null reference, the current exception
        /// is raised in a catch block that handles the inner exception.</param>
        public BackendException(
            string message,
            Exception inner
            )
            : base(message, inner)
        {
            Init(HttpStatusCode.BadRequest);
        }

        private void Init(
            HttpStatusCode statusCode
            )
        {
            StatusCode = (int)statusCode;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or set the HTTP status code.
        /// </summary>
        public int StatusCode { get; set; }

        #endregion Properties
    }
}
