namespace Csla8RestApi.Dal.Exceptions
{
    /// <summary>
    /// Represents an exception when the operation cannot be executed for some wrong data.
    /// </summary>
    public class InvalidDataException : DalException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDataException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InvalidDataException(
            string message
            )
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDataException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public InvalidDataException(
            string message,
            Exception innerException
            )
            : base(message, innerException)
        { }

        #endregion Constructors
    }
}
