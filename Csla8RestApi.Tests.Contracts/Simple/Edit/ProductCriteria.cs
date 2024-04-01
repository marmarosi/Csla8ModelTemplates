using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Simple.Edit
{
    /// <summary>
    /// Represents the criteria of the read-only product object.
    /// </summary>
    [Serializable]
    public class ProductParams
    {
        public string? ProductId { get; set; }

        public ProductParams(
            string productId
            )
        {
            ProductId = productId;
        }

        public ProductCriteria Decode()
        {
            return new ProductCriteria(KeyHash.Decode(ID.Product, ProductId));
        }
    }

    /// <summary>
    /// Represents the criteria of the editable product object.
    /// </summary>
    [Serializable]
    public class ProductCriteria
    {
        public long? ProductKey { get; set; }

        public ProductCriteria(
            long? productKey
            )
        {
            ProductKey = productKey;
        }
    }
}
