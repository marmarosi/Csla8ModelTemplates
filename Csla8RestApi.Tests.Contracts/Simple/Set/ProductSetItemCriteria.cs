namespace Csla8RestApi.Tests.Contracts.Simple.Set
{
    /// <summary>
    /// Represents the criteria of the editable product set item object.
    /// </summary>
    [Serializable]
    public class ProductSetItemCriteria
    {
        public long? ProductKey { get; set; }

        public ProductSetItemCriteria(
            long? productKey
            )
        {
            ProductKey = productKey;
        }
    }
}
