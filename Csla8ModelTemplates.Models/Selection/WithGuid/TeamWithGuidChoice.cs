using Csla;
using Csla8ModelTemplates.Contracts.Selection.WithGuid;
using Csla8RestApi.Models;
using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Models.Selection.WithGuid
{
    /// <summary>
    /// Represents a read-only team choice collection.
    /// </summary>
    [Serializable]
    public class TeamWithGuidChoice : ReadOnlyList<TeamWithGuidChoice, GuidNameOption>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamWithGuidChoice),
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
        /// <param name="criteria">The criteria team choice.</param>
        /// <returns>The requested team choice instance.</returns>
        public static async Task<TeamWithGuidChoice> GetAsync(
            IDataPortalFactory factory,
            TeamWithGuidChoiceCriteria criteria
            )
        {
            return await factory.GetPortal<TeamWithGuidChoice>().FetchAsync(criteria);
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            TeamWithGuidChoiceCriteria criteria,
            [Inject] ITeamWithGuidChoiceDal dal,
            [Inject] IChildDataPortal<GuidNameOption> itemPortal
            )
        {
            // Load values from persistent storage.
            using (LoadListMode)
            {
                List<GuidNameOptionDao> list = await dal.FetchAsync(criteria);
                foreach (var item in list)
                    Add(await itemPortal.FetchChildAsync(item));
            }
        }

        #endregion
    }
}
