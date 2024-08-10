using Csla;
using Csla8ModelTemplates.Contracts.Simple.Set;
using Csla8RestApi.Models;

namespace Csla8ModelTemplates.Models.Simple.Set
{
    /// <summary>
    /// Represents an editable team collection.
    /// </summary>
    [Serializable]
    public class SimpleTeamSet : EditableList<SimpleTeamSet, SimpleTeamSetItem, SimpleTeamSetItemDto>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(SimpleTeamSet),
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
        public static async Task<SimpleTeamSet> GetAsync(
            IDataPortalFactory factory,
            SimpleTeamSetCriteria criteria
            )
        {
            return await factory.GetPortal<SimpleTeamSet>().FetchAsync(criteria);
        }

        /// <summary>
        /// Rebuilds an editable team instance from the data transfer object.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="childFactory">The child data portal factory.</param>
        /// <param name="criteria">The criteria of the team set.</param>
        /// <param name="list">The data transer objects of the team set.</param>
        /// <returns>The team set built.</returns>
        public static async Task<SimpleTeamSet> BuildAsync(
            IDataPortalFactory factory,
            IChildDataPortalFactory childFactory,
            SimpleTeamSetCriteria criteria,
            List<SimpleTeamSetItemDto> list
            )
        {
            var set = await factory.GetPortal<SimpleTeamSet>().FetchAsync(criteria);
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
        //    Child_Create();
        //}

        [Fetch]
        private async Task FetchAsync(
            SimpleTeamSetCriteria criteria,
            [Inject] ISimpleTeamSetDal dal,
            [Inject] IChildDataPortal<SimpleTeamSetItem> itemPortal
            )
        {
            // Load values from persistent storage.
            using (LoadListMode)
            {
                List<SimpleTeamSetItemDao> list = await dal.FetchAsync(criteria);
                foreach (SimpleTeamSetItemDao item in list)
                    Add(await itemPortal.FetchChildAsync(item));
            }
        }

        [Update]
        protected async Task UpdateAsync(
            [Inject] ISimpleTeamSetDal dal
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
