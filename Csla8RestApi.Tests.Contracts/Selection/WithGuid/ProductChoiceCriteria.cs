using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Selection.WithGuid
{
    /// <summary>
    /// Represents the criteria of the read-only product choice collection.
    /// </summary>
    [Serializable]
    public class ProductChoiceCriteria : ChoiceCriteria
    {
        public string? ProductName { get; set; }
    }
}
