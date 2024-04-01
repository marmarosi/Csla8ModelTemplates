using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Complex.Edit
{
    /// <summary>
    /// Represents the criteria of the editable product object.
    /// </summary>
    [Serializable]
    public class ProductParams
    {
        public string ProductId { get; set; }

        public ProductParams(
            string productId
            )
        {
            ProductId = productId;
        }

        public ProductCriteria Decode()
        {
            return new ProductCriteria
            {
                ProductKey = KeyHash.Decode(ID.Product, ProductId) ?? 0
            };
        }
    }

    /// <summary>
    /// Represents the criteria of the editable product object.
    /// </summary>
    [Serializable]
    public class ProductCriteria
    {
        public long? ProductKey { get; set; }

        public ProductCriteria() { }

        public ProductCriteria(
            long? productKey
            )
        {
            ProductKey = productKey;
        }
    }
}
