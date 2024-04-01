namespace Csla8RestApi.Dal.Exceptions
{
    /// <summary>
    /// Represents an exception when the modification of the persistent data failed.
    /// </summary>
    public class UpdateFailedException : DalException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public UpdateFailedException(
            string message
            )
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public UpdateFailedException(
            string message,
            Exception innerException
            )
            : base(message, innerException)
        { }

        #endregion Constructors
    }
}
