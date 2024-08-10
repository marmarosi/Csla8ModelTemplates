using Csla;
using Csla8RestApi.Models;
using Csla8RestApi.Dal.Contracts;
using Csla8ModelTemplates.Contracts.Selection.ByGuid;

namespace Csla8ModelTemplates.Models.Selection.ByGuid
{
    /// <summary>
    /// Represents a read-only team choice collection.
    /// </summary>
    [Serializable]
    public class TeamByGuidChoice : ReadOnlyList<TeamByGuidChoice, ChoiceItem<Guid?>>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamByGuidChoice),
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
        public static async Task<TeamByGuidChoice> GetAsync(
            IDataPortalFactory factory,
            TeamByGuidChoiceCriteria criteria
            )
        {
            return await factory.GetPortal<TeamByGuidChoice>().FetchAsync(criteria);
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            TeamByGuidChoiceCriteria criteria,
            [Inject] ITeamByGuidChoiceDal dal,
            [Inject] IChildDataPortal<ChoiceItem<Guid?>> itemPortal
            )
        {
            // Load values from persistent storage.
            using (LoadListMode)
            {
                List<ChoiceItemDao<Guid?>> list = await dal.FetchAsync(criteria);
                foreach (var item in list)
                    Add(await itemPortal.FetchChildAsync(item));
            }
        }

        #endregion
    }
}
