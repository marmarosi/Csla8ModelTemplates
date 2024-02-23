namespace Csla8ModelTemplates.Contracts.Simple.Set
{
    /// <summary>
    /// Defines the data access functions of the editable team object.
    /// </summary>
    public interface ISimpleTeamSetItemDal
    {
        Task InsertAsync(SimpleTeamSetItemDao dao);
        Task UpdateAsync(SimpleTeamSetItemDao dao);
        Task DeleteAsync(SimpleTeamSetItemCriteria criteria);
    }
}
