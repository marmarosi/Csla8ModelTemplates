using Csla;
using Csla.Data;
using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Contracts.Complex.Set;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Models.Validations;

namespace Csla8ModelTemplates.Models.Complex.Set
{
    /// <summary>
    /// Represents an editable team object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ComplexText), ObjectName = "TeamSetItem")]
    public class TeamSetItem : EditableModel<TeamSetItem, TeamSetItemDto>
    {
        #region Properties

        public static readonly PropertyInfo<long?> TeamKeyProperty = RegisterProperty<long?>(nameof(TeamKey));
        public long? TeamKey
        {
            get => GetProperty(TeamKeyProperty);
            private set => SetProperty(TeamKeyProperty, value);
        }

        public static readonly PropertyInfo<string?> TeamIdProperty = RegisterProperty<string?>(nameof(TeamId), RelationshipTypes.PrivateField);
        public string? TeamId
        {
            get => KeyHash.Encode(ID.Team, TeamKey);
            set => TeamKey = KeyHash.Decode(ID.Team, value);
        }

        public static readonly PropertyInfo<string?> TeamCodeProperty = RegisterProperty<string?>(nameof(TeamCode));
        [Required]
        [MaxLength(10)]
        public string? TeamCode
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

        public static readonly PropertyInfo<TeamSetPlayers> PlayersProperty = RegisterProperty<TeamSetPlayers>(nameof(Players));
        public TeamSetPlayers Players
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
        //    BusinessRules.AddRule(new IsInRole(
        //        AuthorizationActions.ReadProperty,
        //        TeamNameProperty,
        //        "Manager"
        //        ));
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamSetItem),
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
        public override async Task SetValuesOnBuild(
            TeamSetItemDto dto,
            IChildDataPortalFactory childFactory
            )
        {
            DataMapper.Map(dto, this, "Players");
            await BusinessRules.CheckRulesAsync();
            await Players.SetValuesById(dto.Players, "PlayerId", childFactory);
        }

        #endregion

        #region Data Access

        [CreateChild]
        private async Task CreateAsync(
            [Inject] IChildDataPortal<TeamSetPlayers> itemsPortal
            )
        {
            // Set values from data transfer object.
            Players = await itemsPortal.CreateChildAsync();
            //LoadProperty(TeamCodeProperty, "");
            await BusinessRules.CheckRulesAsync();
        }

        [FetchChild]
        private async Task FetchAsync(
            TeamSetItemDao dao,
            [Inject] IChildDataPortal<TeamSetPlayers> itemsPortal
            )
        {
            // Set values from data access object.
            using (BypassPropertyChecks)
            {
                DataMapper.Map(dao, this, "Players");
                Players = await itemsPortal.FetchChildAsync(dao.Players);
            }
        }

        [InsertChild]
        private async Task InsertAsync(
            [Inject] ITeamSetItemDal dal
            )
        {
            // Insert values into persistent storage.
            using (BypassPropertyChecks)
            {
                var dao = Copy.PropertiesFrom(this).Omit("Players").ToNew<TeamSetItemDao>();
                await dal.InsertAsync(dao);

                // Set new data.
                TeamKey = dao.TeamKey;
                Timestamp = dao.Timestamp;
            }
            await FieldManager.UpdateChildrenAsync(this);
        }

        [UpdateChild]
        private async Task UpdateAsync(
            [Inject] ITeamSetItemDal dal
            )
        {
            // Update values in persistent storage.
            if (IsSelfDirty)
            {
                using (BypassPropertyChecks)
                {
                    var dao = Copy.PropertiesFrom(this).Omit("Players").ToNew<TeamSetItemDao>();
                    await dal.UpdateAsync(dao);

                    // Set new data.
                    Timestamp = dao.Timestamp;
                }
            }
            await FieldManager.UpdateChildrenAsync(this);
        }

        [DeleteSelfChild]
        private async Task DeleteSelfAsync(
            [Inject] ITeamSetItemDal dal
            )
        {
            // Delete values from persistent storage.
            if (TeamKey.HasValue)
            {
                Players.Clear();
                await FieldManager.UpdateChildrenAsync(this);

                var criteria = new TeamSetItemCriteria(TeamKey);
                await dal.DeleteAsync(criteria);
            }
        }

        #endregion
    }
}
