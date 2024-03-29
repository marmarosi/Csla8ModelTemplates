namespace Csla8RestApi.Dal.Exceptions
{
    /// <summary>
    /// Represents an exception when the creation of the persistent data failed.
    /// </summary>
    public class InsertFailedException : DalException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InsertFailedException(
            string message
            )
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public InsertFailedException(
            string message,
            Exception innerException
            )
            : base(message, innerException)
        { }

        #endregion Constructors
    }
}
