namespace Csla8RestApi.Dal
{
    /// <summary>
    /// Represents an exception thrown by the data access layer.
    /// </summary>
    public class DalException : BackendException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">The message.</param>
        public DalException(
            string message
            )
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DalException(
            string message,
            Exception innerException
            )
            : base(message, innerException)
        { }

        #endregion Constructors
    }
}
