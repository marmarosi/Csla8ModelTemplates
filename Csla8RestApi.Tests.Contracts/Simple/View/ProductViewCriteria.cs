using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Simple.View
{
    /// <summary>
    /// Represents the criteria of the read-only product model.
    /// </summary>
    [Serializable]
    public class ProductViewParams
    {
        public string? ProductId { get; set; }

        public ProductViewParams(
            string productId
            )
        {
            ProductId = productId;
        }

        public ProductViewCriteria Decode()
        {
            return new ProductViewCriteria
            {
                ProductKey = KeyHash.Decode(ID.Product, ProductId)
            };
        }
    }

    /// <summary>
    /// Represents the criteria of the read-only product object.
    /// </summary>
    [Serializable]
    public class ProductViewCriteria
    {
        public long? ProductKey { get; set; }
    }
}
