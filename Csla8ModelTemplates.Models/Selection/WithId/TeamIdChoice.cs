using Csla;
using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Contracts.Selection.WithId;
using Csla8RestApi.Models;
using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Models.Selection.WithId
{
    /// <summary>
    /// Represents a read-only team choice collection.
    /// </summary>
    [Serializable]
    public class TeamIdChoice : ReadOnlyList<TeamIdChoice, IdNameOption>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamIdChoice),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets a choice of team options that match the criteria.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <returns>The requested team choice instance.</returns>
        public static async Task<TeamIdChoice> GetAsync(
            IDataPortalFactory factory,
            TeamIdChoiceCriteria criteria
            )
        {
            return await factory.GetPortal<TeamIdChoice>().FetchAsync(criteria);
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            TeamIdChoiceCriteria criteria,
            [Inject] ITeamIdChoiceDal dal,
            [Inject] IChildDataPortal<IdNameOption> itemPortal
            )
        {
            // Load values from persistent storage.
            using (LoadListMode)
            {
                List<IdNameOptionDao> list = await dal.FetchAsync(criteria);
                foreach (var item in list)
                    Add(itemPortal.FetchChild(item, ID.Team));
            }
        }

        #endregion
    }
}
