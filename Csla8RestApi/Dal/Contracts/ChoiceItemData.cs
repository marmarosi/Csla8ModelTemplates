#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Csla8RestApi.Dal.Contracts
{
    /// <summary>
    /// Defines the data access object of the read-only choice item object.
    /// </summary>
    public class ChoiceItemDao<T>
    {
        public T Value { get; set; }
        public string? Name { get; set; }

        public ChoiceItemDao<string?> ToId(
            string hashModel
            )
        {
            return new ChoiceItemDao<string?>
            {
                Value = KeyHash.Encode(hashModel, Value == null ? null : Convert.ToInt64(Value)),
                Name = Name
            };
        }
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
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
