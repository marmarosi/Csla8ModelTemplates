using System.Collections.Generic;

namespace Csla8RestApi.Models
{
    /// <summary>
    /// Defines the helper functions of read-only lists.
    /// </summary>
    public interface IReadOnlyList
    {
        IList<T> ToDto<T>() where T : class;
    }
}
