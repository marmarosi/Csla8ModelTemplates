namespace Csla8ModelTemplates.Contracts.Simple.List
{
    /// <summary>
    /// Defines the data access functions of the read-only order collection.
    /// </summary>
    public interface IOrderListDal
    {
        Task<List<OrderListItemDao>> FetchAsync(OrderListCriteria criteria);
    }
}
