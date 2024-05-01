using Csla;
using Csla8ModelTemplates.Contracts.Arrangement.Pagination;
using Csla8RestApi.Models;
using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Models.Arrangement.Pagination
{
    /// <summary>
    /// Represents a read-only paginated team collection.
    /// </summary>
    [Serializable]
    public class PaginatedTeamList : ReadOnlyModel<PaginatedTeamList>
    {
        #region Properties

        public static readonly PropertyInfo<PaginatedTeamListItems?> DataProperty = RegisterProperty<PaginatedTeamListItems?>(nameof(Data));
        public PaginatedTeamListItems? Data
        {
            get => GetProperty(DataProperty);
            private set => LoadProperty(DataProperty, value);
        }

        public static readonly PropertyInfo<int> TotalCountProperty = RegisterProperty<int>(c => c.TotalCount);
        public int TotalCount
        {
            get => GetProperty(TotalCountProperty);
            private set => LoadProperty(TotalCountProperty, value);
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        new IsInRole(
        //            AuthorizationActions.ReadProperty,
        //            TotalCountProperty,
        //            "Manager"
        //            )
        //        );
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(PaginatedTeamList),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets the specified read-only paginated team collection.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="criteria">The criteria of the read-only team list.</param>
        /// <returns>The requested read-only team list.</returns>
        public static async Task<PaginatedTeamList> GetAsync(
            IDataPortalFactory factory,
            PaginatedTeamListCriteria criteria
            )
        {
            return await factory.GetPortal<PaginatedTeamList>().FetchAsync(criteria);
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            PaginatedTeamListCriteria criteria,
            [Inject] IPaginatedTeamListDal dal,
            [Inject] IChildDataPortal<PaginatedTeamListItems> itemsPortal
            )
        {
            // Load values from persistent storage.
            IPaginatedList<PaginatedTeamListItemDao> dao = await dal.FetchAsync(criteria);
            Data = itemsPortal.FetchChild(dao.Data);
            TotalCount = dao.TotalCount;
        }

        #endregion
    }
}
