namespace Csla8ModelTemplates.Contracts.Simple.View
{
    /// <summary>
    /// Defines the data access functions of the read-only team model.
    /// </summary>
    public interface ISimpleTeamViewDal
    {
        SimpleTeamViewDao Fetch(SimpleTeamViewCriteria criteria);
    }
}
