namespace Csla8RestApi.Dal.Exceptions
{
    /// <summary>
    /// Represents an exception when references are found on deletetion check.
    /// </summary>
    public class ReferenceFoundException : DalException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ReferenceFoundException(
            string message
            )
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ReferenceFoundException(
            string message,
            Exception innerException
            )
            : base(message, innerException)
        { }

        #endregion Constructors
    }
}
