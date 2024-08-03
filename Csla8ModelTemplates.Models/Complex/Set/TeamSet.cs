using Csla;
using Csla8ModelTemplates.Contracts.Complex.Set;
using Csla8RestApi.Models;

namespace Csla8ModelTemplates.Models.Complex.Set
{
    /// <summary>
    /// Represents an editable team collection.
    /// </summary>
    [Serializable]
    public class TeamSet : EditableList<TeamSet, TeamSetItem, TeamSetItemDto>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamSet),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets the specified team set to edit.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="criteria">The criteria of the team set.</param>
        /// <returns>The requested team set.</returns>
        public static async Task<TeamSet> GetAsync(
            IDataPortalFactory factory,
            TeamSetCriteria criteria
            )
        {
            return await factory.GetPortal<TeamSet>().FetchAsync(criteria);
        }

        /// <summary>
        /// Rebuilds an editable team instance from the data transfer object.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="childFactory">The child data portal factory.</param>
        /// <param name="criteria">The criteria of the team set.</param>
        /// <param name="list">The data transer objects of the team set.</param>
        /// <returns>The team set built.</returns>
        public static async Task<TeamSet> BuildAsync(
            IDataPortalFactory factory,
            IChildDataPortalFactory childFactory,
            TeamSetCriteria criteria,
            List<TeamSetItemDto> list
            )
        {
            var set = await factory.GetPortal<TeamSet>().FetchAsync(criteria);
            await set.SetValuesById(list, "TeamId", childFactory);
            return set;
        }

        #endregion

        #region Data Access

        //[Create]
        //[RunLocal]
        //private async Task CreateAsync()
        //{
        //    // Load default values.
        //}

        [Fetch]
        private async Task FetchAsync(
            TeamSetCriteria criteria,
            [Inject] ITeamSetDal dal,
            [Inject] IChildDataPortal<TeamSetItem> itemPortal
            )
        {
            // Load values from persistent storage.
            using (LoadListMode)
            {
                List<TeamSetItemDao> list = await dal.FetchAsync(criteria);
                foreach (TeamSetItemDao item in list)
                    Add(await itemPortal.FetchChildAsync(item));
            }
        }

        [Update]
        protected async Task UpdateAsync(
            [Inject] ITeamSetDal dal
            )
        {
            // Update values in persistent storage.
            using (var transaction = await dal.BeginTransaction())
            {
                await Child_UpdateAsync();
                await dal.Commit(transaction);
            }
        }

        #endregion
    }
}
