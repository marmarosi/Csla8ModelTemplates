using Csla8RestApi.Dal;

namespace Csla8RestApi.Tests.Contracts.Simple.Command
{
    /// <summary>
    /// Defines the data access functions of the clear product command.
    /// </summary>
    public interface IClearProductDal : ITransactionalDal
    {
        Task ExecuteAsync(ClearProductDao dao);
    }
}
