using Csla;
using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Contracts.Selection.ById;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;

namespace Csla8ModelTemplates.Models.Selection.ById
{
    /// <summary>
    /// Represents a read-only team choice collection.
    /// </summary>
    [Serializable]
    public class TeamByIdChoice : ReadOnlyList<TeamByIdChoice, ChoiceItem<string?>>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamByIdChoice),
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
        public static async Task<TeamByIdChoice> GetAsync(
            IDataPortalFactory factory,
            TeamByIdChoiceCriteria criteria
            )
        {
            return await factory.GetPortal<TeamByIdChoice>().FetchAsync(criteria);
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            TeamByIdChoiceCriteria criteria,
            [Inject] ITeamByIdChoiceDal dal,
            [Inject] IChildDataPortal<ChoiceItem<string?>> itemPortal
            )
        {
            // Load values from persistent storage.
            using (LoadListMode)
            {
                List<ChoiceItemDao<long?>> list = await dal.FetchAsync(criteria);
                foreach (var item in list)
                    Add(await itemPortal.FetchChildAsync(item.ToId(ID.Team)));
            }
        }

        #endregion
    }
}
