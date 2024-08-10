namespace Csla8RestApi.Tests.Contracts.Complex.Edit
{
    /// <summary>
    /// Represents the criteria of the editable part object.
    /// </summary>
    [Serializable]
    public class ProductPartCriteria
    {
        public long? PartKey { get; set; }

        public ProductPartCriteria()
        { }

        public ProductPartCriteria(
            long? partKey
            )
        {
            PartKey = partKey;
        }
    }
}
