using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Complex.View
{
    /// <summary>
    /// Represents the criteria of the read-only product object.
    /// </summary>
    [Serializable]
    public class ProductViewCriteria
    {
        public long? ProductKey { get; set; }

        public ProductViewCriteria(
            string? productId
            )
        {
            ProductKey = KeyHash.Decode(ID.Product, productId);
        }
    }
}
