using Csla8RestApi.Dal;

namespace Csla8ModelTemplates.Contracts.Simple.Command
{
    /// <summary>
    /// Defines the data access functions of the rename team command.
    /// </summary>
    public interface IRenameTeamDal : ITransactionalDal
    {
        void Execute(RenameTeamDao dao);
    }
}
