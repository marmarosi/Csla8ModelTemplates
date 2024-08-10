namespace Csla8ModelTemplates.Contracts.Complex.Set
{
    /// <summary>
    /// Defines the data access functions of the editable player object.
    /// </summary>
    public interface ITeamSetPlayerDal
    {
        Task InsertAsync(TeamSetPlayerDao dao);
        Task UpdateAsync(TeamSetPlayerDao dao);
        Task DeleteAsync(TeamSetPlayerCriteria criteria);
    }
}
