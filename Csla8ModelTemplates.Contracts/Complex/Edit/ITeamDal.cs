using Csla8RestApi.Dal;

namespace Csla8ModelTemplates.Contracts.Complex.Edit
{
    /// <summary>
    /// Defines the data access functions of the editable team object.
    /// </summary>
    public interface ITeamDal : ITransactionalDal
    {
        TeamDao Fetch(TeamCriteria criteria);
        void Insert(TeamDao dao);
        void Update(TeamDao dao);
        void Delete(TeamCriteria criteria);
    }
}
