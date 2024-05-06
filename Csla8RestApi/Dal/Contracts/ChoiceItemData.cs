namespace Csla8RestApi.Dal.Contracts
{
    /// <summary>
    /// Defines the data access object of the read-only choice item object.
    /// </summary>
    public class ChoiceItemDao<T>
    {
        public T Value { get; set; }
        public string? Name { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only choice item object.
    /// </summary>
    public class ChoiceItemDto<T>
    {
        public T Value { get; set; }
        public string? Name { get; set; }
    }
}
