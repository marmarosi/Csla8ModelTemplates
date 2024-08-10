using Csla;
using Csla8RestApi.Models;
using Csla8RestApi.Dal.Contracts;
using Csla8ModelTemplates.Contracts.Selection.ByKey;

namespace Csla8ModelTemplates.Models.Selection.ByKey
{
    /// <summary>
    /// Represents a read-only team choice collection.
    /// </summary>
    [Serializable]
    public class TeamByKeyChoice : ReadOnlyList<TeamByKeyChoice, ChoiceItem<long?>>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamByKeyChoice),
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
        public static async Task<TeamByKeyChoice> GetAsync(
            IDataPortalFactory factory,
            TeamByKeyChoiceCriteria criteria
            )
        {
            return await factory.GetPortal<TeamByKeyChoice>().FetchAsync(criteria);
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            TeamByKeyChoiceCriteria criteria,
            [Inject] ITeamByKeyChoiceDal dal,
            [Inject] IChildDataPortal<ChoiceItem<long?>> itemPortal
            )
        {
            // Load values from persistent storage.
            using (LoadListMode)
            {
                List<ChoiceItemDao<long?>> list = await dal.FetchAsync(criteria);
                foreach (var item in list)
                    Add(await itemPortal.FetchChildAsync(item));
            }
        }

        #endregion
    }
}
