using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Simple.Edit
{
    /// <summary>
    /// Represents the criteria of the editable product object.
    /// </summary>
    [Serializable]
    public class ProductCriteria
    {
        public long? ProductKey { get; set; }

        public ProductCriteria(
            string? productId
            )
        {
            ProductKey = KeyHash.Decode(ID.Product, productId);
        }
    }
}
