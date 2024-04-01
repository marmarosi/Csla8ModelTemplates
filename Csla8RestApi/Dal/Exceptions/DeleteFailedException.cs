namespace Csla8RestApi.Dal.Exceptions
{
    /// <summary>
    /// Represents an exception when the deletion of the persistent data failed.
    /// </summary>
    public class DeleteFailedException : DalException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DeleteFailedException(
            string message
            )
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DeleteFailedException(
            string message,
            Exception innerException
            )
            : base(message, innerException)
        { }

        #endregion Constructors
    }
}
