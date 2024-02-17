namespace Csla8ModelTemplates.Contracts.Complex.Set
{
    /// <summary>
    /// Represents the criteria of the editable team set item object.
    /// </summary>
    [Serializable]
    public class TeamSetItemCriteria
    {
        public long? TeamKey { get; set; }

        public TeamSetItemCriteria()
        { }

        public TeamSetItemCriteria(
            long? teamKey
            )
        {
            TeamKey = teamKey;
        }
    }
}
