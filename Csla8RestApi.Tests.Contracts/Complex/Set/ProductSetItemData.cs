using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Complex.Set
{
    /// <summary>
    /// Defines the editable product set item data.
    /// </summary>
    public class ProductSetItemData
    {
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable product set item object.
    /// </summary>
    public class ProductSetItemDao : ProductSetItemData
    {
        public long? ProductKey { get; set; }
        public List<ProductSetPartDao> Parts { get; set; }

        public ProductSetItemDao()
        {
            Parts = new List<ProductSetPartDao>();
        }
    }

    /// <summary>
    /// Defines the data transfer object of the editable product set item object.
    /// </summary>
    public class ProductSetItemDto : ProductSetItemData
    {
        public string? ProductId { get; set; }
        public List<ProductSetPartDto> Parts { get; set; }

        public ProductSetItemDto()
        {
            Parts = new List<ProductSetPartDto>();
        }

        public ProductSetItemDao ToDao()
        {
            return new ProductSetItemDao
            {
                ProductKey = KeyHash.Decode(ID.Product, ProductId),
                ProductCode = ProductCode,
                ProductName = ProductName,
                Parts = PartsToDao(),
                Timestamp = Timestamp
            };
        }

        protected List<ProductSetPartDao> PartsToDao()
        {
            var list = new List<ProductSetPartDao>();

            foreach (ProductSetPartDto part in Parts)
                list.Add(part.ToDao());

            return list;
        }
    }
}
