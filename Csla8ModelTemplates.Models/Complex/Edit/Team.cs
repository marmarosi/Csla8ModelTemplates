using Csla;
using Csla.Data;
using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Contracts.Complex.Edit;
using Csla8RestApi.Models;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Models.Validations;
using Csla8RestApi.Dal.Contracts;
using Csla8ModelTemplates.Resources;

namespace Csla8ModelTemplates.Models.Complex.Edit
{
    /// <summary>
    /// Represents an editable team object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ValidationText), ObjectName = "Team")]
    public class Team : EditableModel<Team, TeamDto>
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

        public static readonly PropertyInfo<TeamPlayers> PlayersProperty = RegisterProperty<TeamPlayers>(nameof(Players));
        public TeamPlayers Players
        {
            get => GetProperty(PlayersProperty);
            private set => LoadProperty(PlayersProperty, value);
        }

        public static readonly PropertyInfo<DateTimeOffset?> TimestampProperty = RegisterProperty<DateTimeOffset?>(nameof(Timestamp));
        public DateTimeOffset? Timestamp
        {
            get => GetProperty(TimestampProperty);
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
        //        typeof(Team),
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
            TeamDto dto,
            IChildDataPortalFactory childFactory
            )
        {
            DataMapper.Map(dto, this, "Players");
            BusinessRules.CheckRules();
            Players.SetValuesById(dto.Players, "PlayerId", childFactory);
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets a new team to edit.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <returns>The new team.</returns>
        public static async Task<Team> NewAsync(
            IDataPortalFactory factory
            )
        {
            return await factory.GetPortal<Team>().CreateAsync();
        }

        /// <summary>
        /// Gets the specified team to edit.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="id">The identifier of the team.</param>
        /// <returns>The requested team.</returns>
        public static async Task<Team> GetAsync(
            IDataPortalFactory factory,
            string id
            )
        {
            var criteria = new TeamParams(id);
            return await factory.GetPortal<Team>().FetchAsync(criteria.Decode());
        }

        /// <summary>
        /// Builds a new or existing team.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="childFactory">The child data portal factory.</param>
        /// <param name="dto"></param>
        /// <returns>The team built.</returns>
        public static async Task<Team> BuildAsync(
            IDataPortalFactory factory,
            IChildDataPortalFactory childFactory,
            TeamDto dto
            )
        {
            long? teamKey = KeyHash.Decode(ID.Team, dto!.TeamId);
            Team team = teamKey.HasValue ?
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
            var criteria = new TeamParams(id);
            await factory.GetPortal<Team>().DeleteAsync(criteria.Decode());
        }

        #endregion

        #region Data Access

        [Create]
        [RunLocal]
        private async Task CreateAsync(
            [Inject] IChildDataPortal<TeamPlayers> itemsPortal
            )
        {
            // Load default values.
            Players = await itemsPortal.CreateChildAsync();
            //LoadProperty(TeamCodeProperty, "");
            await BusinessRules.CheckRulesAsync();
        }

        [Fetch]
        private async Task FetchAsync(
            TeamCriteria criteria,
            [Inject] ITeamDal dal,
            [Inject] IChildDataPortal<TeamPlayers> itemsPortal
            )
        {
            // Load values from persistent storage.
            TeamDao dao = await dal.FetchAsync(criteria);
            using (BypassPropertyChecks)
            {
                DataMapper.Map(dao, this, "Players");
                Players = await itemsPortal.FetchChildAsync(dao.Players);
            }
        }

        [Insert]
        protected async Task InsertAsync(
            [Inject] ITeamDal dal
            )
        {
            // Insert values into persistent storage.
            using (var transaction = dal.BeginTransaction())
            {
                using (BypassPropertyChecks)
                {
                    var dao = Copy.PropertiesFrom(this).Omit("Players").ToNew<TeamDao>();
                    await dal.InsertAsync(dao);

                    // Set new data.
                    TeamKey = dao.TeamKey;
                    Timestamp = dao.Timestamp;
                }
                await FieldManager.UpdateChildrenAsync(this);
                dal.Commit(transaction);
            }
        }

        [Update]
        protected async Task UpdateAsync(
            [Inject] ITeamDal dal
            )
        {
            // Update values in persistent storage.
            using (var transaction = dal.BeginTransaction())
            {
                if (IsSelfDirty)
                {
                    using (BypassPropertyChecks)
                    {
                        var dao = Copy.PropertiesFrom(this).Omit("Players").ToNew<TeamDao>();
                        await dal.UpdateAsync(dao);

                        // Set new data.
                        Timestamp = dao.Timestamp;
                    }
                }
                await FieldManager.UpdateChildrenAsync(this);
                dal.Commit(transaction);
            }
        }

        [DeleteSelf]
        protected async Task DeleteSelfAsync(
            [Inject] ITeamDal dal,
            [Inject] IChildDataPortal<TeamPlayers> itemPortal
            )
        {
            using (BypassPropertyChecks)
                await DeleteAsync(new TeamCriteria(TeamKey), dal, itemPortal);
        }

        [Delete]
        protected async Task DeleteAsync(
            TeamCriteria criteria,
            [Inject] ITeamDal dal,
            [Inject] IChildDataPortal<TeamPlayers> itemPortal
            )
        {
            // Delete values from persistent storage.
            using (var transaction = dal.BeginTransaction())
            {
                if (!TeamKey.HasValue)
                    await FetchAsync(criteria, dal, itemPortal);

                Players.Clear();
                await FieldManager.UpdateChildrenAsync(this);

                await dal.DeleteAsync(criteria);
                dal.Commit(transaction);
            }
        }

        #endregion
    }
}
