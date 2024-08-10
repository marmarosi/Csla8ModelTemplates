using Csla;
using Csla8RestApi.Models;
using Csla8RestApi.Dal.Contracts;
using Csla8ModelTemplates.Contracts.Selection.ByCode;

namespace Csla8ModelTemplates.Models.Selection.ByCode
{
    /// <summary>
    /// Represents a read-only team choice collection.
    /// </summary>
    [Serializable]
    public class TeamByCodeChoice : ReadOnlyList<TeamByCodeChoice, ChoiceItem<string?>>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamByCodeChoice),
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
        public static async Task<TeamByCodeChoice> GetAsync(
            IDataPortalFactory factory,
            TeamByCodeChoiceCriteria criteria
            )
        {
            return await factory.GetPortal<TeamByCodeChoice>().FetchAsync(criteria);
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            TeamByCodeChoiceCriteria criteria,
            [Inject] ITeamByCodeChoiceDal dal,
            [Inject] IChildDataPortal<ChoiceItem<string?>> itemPortal
            )
        {
            // Load values from persistent storage.
            using (LoadListMode)
            {
                List<ChoiceItemDao<string?>> list = await dal.FetchAsync(criteria);
                foreach (var item in list)
                    Add(await itemPortal.FetchChildAsync(item));
            }
        }

        #endregion
    }
}
