namespace Csla8RestApi.Tests.Contracts.Complex.Command
{
    /// <summary>
    /// Defines the data access functions of the count products by part count command.
    /// </summary>
    public interface ICountProductsDal
    {
        Task<List<CountProductsResultDao>> ExecuteAsync(CountProductsCriteria criteria);
    }
}
