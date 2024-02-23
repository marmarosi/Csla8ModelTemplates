namespace Csla8RestApi.Dal.Contracts
{
    /// <summary>
    /// Defines the read-only Guid-name option data.
    /// </summary>
    public class GuidNameOptionData
    {
        public Guid? Guid { get; set; }
        public string? Name { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only Guid-name option object.
    /// </summary>
    public class GuidNameOptionDao : GuidNameOptionData
    { }

    /// <summary>
    /// Defines the data transfer object of the read-only Guid-name option object.
    /// </summary>
    public class GuidNameOptionDto : GuidNameOptionData
    { }
}
