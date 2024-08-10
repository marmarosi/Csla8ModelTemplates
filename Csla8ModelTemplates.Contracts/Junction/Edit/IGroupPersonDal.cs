namespace Csla8ModelTemplates.Contracts.Junction.Edit
{
    /// <summary>
    /// Defines the data access functions of the editable group-person object.
    /// </summary>
    public interface IGroupPersonDal
    {
        Task InsertAsync(GroupPersonDao dao);
        Task DeleteAsync(GroupPersonDao dao);
    }
}
