namespace Csla8ModelTemplates.Contracts.Complex.Set
{
    /// <summary>
    /// Defines the data access functions of the editable team set item object.
    /// </summary>
    public interface ITeamSetItemDal
    {
        Task InsertAsync(TeamSetItemDao dao);
        Task UpdateAsync(TeamSetItemDao dao);
        Task DeleteAsync(TeamSetItemCriteria criteria);
    }
}
