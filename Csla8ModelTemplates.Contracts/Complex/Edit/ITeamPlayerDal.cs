namespace Csla8ModelTemplates.Contracts.Complex.Edit
{
    /// <summary>
    /// Defines the data access functions of the editable player object.
    /// </summary>
    public interface ITeamPlayerDal
    {
        Task InsertAsync(TeamPlayerDao dao);
        Task UpdateAsync(TeamPlayerDao dao);
        Task DeleteAsync(TeamPlayerCriteria criteria);
    }
}
