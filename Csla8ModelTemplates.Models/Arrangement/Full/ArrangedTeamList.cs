using Csla;
using Csla8ModelTemplates.Contracts.Arrangement.Full;
using Csla8RestApi.Models;
using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Models.Arrangement.Full
{
    /// <summary>
    /// Represents a read-only paginated sorted team collection.
    /// </summary>
    [Serializable]
    public class ArrangedTeamList : ReadOnlyModel<ArrangedTeamList>
    {
        #region Properties

        public static readonly PropertyInfo<ArrangedTeamListItems?> DataProperty = RegisterProperty<ArrangedTeamListItems?>(nameof(Data));
        public ArrangedTeamListItems? Data
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
        //            DataProperty,
        //            "Manager"
        //            )
        //        );
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(ArrangedTeamList),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets the specified read-only paginated sorted team collection.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="criteria">The criteria of the read-only team.</param>
        /// <returns>The requested read-only team list.</returns>
        public static async Task<ArrangedTeamList> GetAsync(
            IDataPortalFactory factory,
            ArrangedTeamListCriteria criteria
            )
        {
            return await factory.GetPortal<ArrangedTeamList>().FetchAsync(criteria);
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            ArrangedTeamListCriteria criteria,
            [Inject] IArrangedTeamListDal dal,
            [Inject] IChildDataPortal<ArrangedTeamListItems> itemsPortal
            )
        {
            // Load values from persistent storage.
            IPaginatedList<ArrangedTeamListItemDao> dao = await dal.FetchAsync(criteria);
            Data = await itemsPortal.FetchChildAsync(dao.Data);
            TotalCount = dao.TotalCount;
        }

        #endregion
    }
}
