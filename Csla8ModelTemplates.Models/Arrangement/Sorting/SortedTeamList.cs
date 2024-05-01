using Csla;
using Csla8ModelTemplates.Contracts.Arrangement.Sorting;
using Csla8RestApi.Models;

namespace Csla8ModelTemplates.Models.Arrangement.Sorting
{
    /// <summary>
    /// Represents a read-only sorted team collection.
    /// </summary>
    [Serializable]
    public class SortedTeamList : ReadOnlyList<SortedTeamList, SortedTeamListItem>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(SortedTeamList),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets a read-only sorted team collection that matches the criteria.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="criteria">The criteria of the read-only team collection.</param>
        /// <returns>The requested read-only sorted team collection.</returns>
        public static async Task<SortedTeamList> GetAsync(
            IDataPortalFactory factory,
            SortedTeamListCriteria criteria
            )
        {
            return await factory.GetPortal<SortedTeamList>().FetchAsync(criteria);
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            SortedTeamListCriteria criteria,
            [Inject] ISortedTeamListDal dal,
            [Inject] IChildDataPortal<SortedTeamListItem> itemPortal
            )
        {
            // Load values from persistent storage.
            using (LoadListMode)
            {
                List<SortedTeamListItemDao> list = await dal.FetchAsync(criteria);
                foreach (var item in list)
                    Add(await itemPortal.FetchChildAsync(item));
            }
        }

        #endregion
    }
}
