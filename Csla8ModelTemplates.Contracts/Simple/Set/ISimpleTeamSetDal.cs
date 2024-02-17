using Csla8RestApi.Dal;

namespace Csla8ModelTemplates.Contracts.Simple.Set
{
    /// <summary>
    /// Defines the data access functions of the editable team collection.
    /// </summary>
    public interface ISimpleTeamSetDal : ITransactionalDal
    {
        List<SimpleTeamSetItemDao> Fetch(SimpleTeamSetCriteria criteria);
    }
}
