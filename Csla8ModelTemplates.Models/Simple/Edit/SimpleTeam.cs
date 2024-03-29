using Csla;
using Csla.Data;
using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Contracts.Simple.Edit;
using Csla8RestApi.Models;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Models.Validations;
using Csla8RestApi.Dal.Contracts;
using Csla8ModelTemplates.Resources;

namespace Csla8ModelTemplates.Models.Simple.Edit
{
    /// <summary>
    /// Represents an editable team object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ValidationText), ObjectName = "SimpleTeam")]
    public class SimpleTeam : EditableModel<SimpleTeam, SimpleTeamDto>
    {
        #region Properties

        public static readonly PropertyInfo<long?> TeamKeyProperty = RegisterProperty<long?>(nameof(TeamKey));
        public long? TeamKey
        {
            get => GetProperty(TeamKeyProperty);
            private set => SetProperty(TeamKeyProperty, value);
        }

        public static readonly PropertyInfo<long?> TeamIdProperty = RegisterProperty<long?>(nameof(TeamId), RelationshipTypes.PrivateField);
        public string TeamId
        {
            get => KeyHash.Encode(ID.Team, TeamKey);
            set => TeamKey = KeyHash.Decode(ID.Team, value);
        }

        public static readonly PropertyInfo<string> TeamCodeProperty = RegisterProperty<string>(nameof(TeamCode));
        [Required]
        [MaxLength(10)]
        public string TeamCode
        {
            get => GetProperty(TeamCodeProperty);
            set => SetProperty(TeamCodeProperty, value);
        }

        public static readonly PropertyInfo<string?> TeamNameProperty = RegisterProperty<string?>(nameof(TeamName));
        [Required]
        [MaxLength(100)]
        public string? TeamName
        {
            get => GetProperty(TeamNameProperty);
            set => SetProperty(TeamNameProperty, value);
        }

        public static readonly PropertyInfo<DateTimeOffset?> TimestampProperty = RegisterProperty<DateTimeOffset?>(nameof(Timestamp));
        public DateTimeOffset? Timestamp
        {
            get =>  GetProperty(TimestampProperty);
            private set => LoadProperty(TimestampProperty, value);
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Call base class implementation to add data annotation rules to BusinessRules.
        //    // NOTE: DataAnnotation rules is always added with Priority = 0.
        //    base.AddBusinessRules();

        //    // Add validation rules.
        //    BusinessRules.AddRule(new Required(TeamNameProperty));

        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        new IsInRole(
        //            AuthorizationActions.ReadProperty,
        //            TeamNameProperty,
        //            "Manager"
        //            )
        //        );
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(SimpleTeam),
        //        new IsInRole(
        //            AuthorizationActions.EditObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Business Methods

        /// <summary>
        /// Updates an editable model and its children from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <param name="childFactory">The child data portal factory.</param>
        public override void SetValuesOnBuild(
            SimpleTeamDto dto,
            IChildDataPortalFactory childFactory
            )
        {
            DataMapper.Map(dto, this);
            BusinessRules.CheckRules();
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets a new team to edit.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <returns>The new team.</returns>
        public static async Task<SimpleTeam> NewAsync(
            IDataPortalFactory factory
            )
        {
            return await factory.GetPortal<SimpleTeam>().CreateAsync();
        }

        /// <summary>
        /// Gets the specified team to edit.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="id">The identifier of the team.</param>
        /// <returns>The requested team.</returns>
        public static async Task<SimpleTeam> GetAsync(
            IDataPortalFactory factory,
            string id
            )
        {
            var criteria = new SimpleTeamParams(id);
            return await factory.GetPortal<SimpleTeam>().FetchAsync(criteria.Decode());
        }

        /// <summary>
        /// Builds a new or existing team.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="childFactory">The child data portal factory.</param>
        /// <param name="dto"></param>
        /// <returns>The team built.</returns>
        public static async Task<SimpleTeam> BuildAsync(
            IDataPortalFactory factory,
            IChildDataPortalFactory childFactory,
            SimpleTeamDto dto
            )
        {
            long? teamKey = KeyHash.Decode(ID.Team, dto.TeamId);
            SimpleTeam team = teamKey.HasValue ?
                await GetAsync(factory, dto.TeamId!) :
                await NewAsync(factory);
            team.SetValuesOnBuild(dto, childFactory);
            return team;
        }

        /// <summary>
        /// Deletes the specified team.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="id">The identifier of the team.</param>
        public static async Task DeleteAsync(
            IDataPortalFactory factory,
            string id
            )
        {
            var criteria = new SimpleTeamParams(id);
            await factory.GetPortal<SimpleTeam>().DeleteAsync(criteria.Decode());
        }

        #endregion

        #region Data Access

        [Create]
        [RunLocal]
        private async Task CreateAsync()
        {
            // Load default values.
            await Task.Run(async () =>
            {
                //LoadProperty(TeamCodeProperty, "");
                await BusinessRules.CheckRulesAsync();
            });
        }

        [Fetch]
        private async Task FetchAsync(
            SimpleTeamCriteria criteria,
            [Inject] ISimpleTeamDal dal
            )
        {
            // Load values from persistent storage.
            SimpleTeamDao dao = await dal.FetchAsync(criteria);
            using (BypassPropertyChecks)
                DataMapper.Map(dao, this);
        }

        [Insert]
        protected async Task InsertAsync(
            [Inject] ISimpleTeamDal dal
            )
        {
            // Insert values into persistent storage.
            using (var transaction = dal.BeginTransaction())
            {
                using (BypassPropertyChecks)
                {
                    var dao = Copy.PropertiesFrom(this).ToNew<SimpleTeamDao>();
                    await dal.InsertAsync(dao);

                    // Set new data.
                    TeamKey = dao.TeamKey;
                    Timestamp = dao.Timestamp;
                }
                dal.Commit(transaction);
            }
        }

        [Update]
        protected async Task UpdateAsync(
            [Inject] ISimpleTeamDal dal
            )
        {
            // Update values in persistent storage.
            using (var transaction = dal.BeginTransaction())
            {
                using (BypassPropertyChecks)
                {
                    var dao = Copy.PropertiesFrom(this).ToNew<SimpleTeamDao>();
                    await dal.UpdateAsync(dao);

                    // Set new data.
                    Timestamp = dao.Timestamp;
                }
                dal.Commit(transaction);
            }
        }

        [DeleteSelf]
        protected async Task DeleteSelfAsync(
            [Inject] ISimpleTeamDal dal
            )
        {
            using (BypassPropertyChecks)
                await DeleteAsync(new SimpleTeamCriteria(TeamKey), dal);
        }

        [Delete]
        protected async Task DeleteAsync(
            SimpleTeamCriteria criteria,
            [Inject] ISimpleTeamDal dal
            )
        {
            // Delete values from persistent storage.
            using (var transaction = dal.BeginTransaction())
            {
                await dal.DeleteAsync(criteria);
                dal.Commit(transaction);
            }
        }

        #endregion
    }
}
