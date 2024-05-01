using Csla;
using Csla.Core;
using Csla.Data;
using Csla.Rules;
using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Contracts.Complex.Edit;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Models.Validations;

namespace Csla8ModelTemplates.Models.Complex.Edit
{
    /// <summary>
    /// Represents an editable player object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ComplexText), ObjectName = "Player")]
    public class TeamPlayer : EditableModel<TeamPlayer, TeamPlayerDto>
    {
        #region Properties

        public static readonly PropertyInfo<long?> PlayerKeyProperty = RegisterProperty<long?>(nameof(PlayerKey));
        public long? PlayerKey
        {
            get => GetProperty(PlayerKeyProperty);
            private set => SetProperty(PlayerKeyProperty, value);
        }

        public static readonly PropertyInfo<long?> PlayerIdProperty = RegisterProperty<long?>(nameof(PlayerId), RelationshipTypes.PrivateField);
        public string PlayerId
        {
            get => KeyHash.Encode(ID.Player, PlayerKey);
            set => PlayerKey = KeyHash.Decode(ID.Player, value);
        }

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

        public static readonly PropertyInfo<string> PlayerCodeProperty = RegisterProperty<string>(nameof(PlayerCode));
        [Required]
        [MaxLength(10)]
        public string PlayerCode
        {
            get => GetProperty(PlayerCodeProperty);
            set => SetProperty(PlayerCodeProperty, value);
        }

        public static readonly PropertyInfo<string> PlayerNameProperty = RegisterProperty<string>(nameof(PlayerName));
        [Required]
        [MaxLength(100)]
        public string PlayerName
        {
            get => GetProperty(PlayerNameProperty);
            set => SetProperty(PlayerNameProperty, value);
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // Call base class implementation to add data annotation rules to BusinessRules.
            // NOTE: DataAnnotation rules is always added with Priority = 0.
            base.AddBusinessRules();

            // Add validation rules.
            BusinessRules.AddRule(new UniquePlayerCodes(PlayerCodeProperty));

            //// Add authorization rules.
            //BusinessRules.AddRule(
            //    new IsInRole(
            //        AuthorizationActions.WriteProperty,
            //        PlayerCodeProperty,
            //        "Manager"
            //        )
            //    );
        }

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(Player),
        //        new IsInRole(
        //            AuthorizationActions.EditObject,
        //            "Manager"
        //            )
        //        );
        //}

        private sealed class UniquePlayerCodes : BusinessRule
        {
            // Add additional parameters to your rule to the constructor.
            public UniquePlayerCodes(
                IPropertyInfo primaryProperty
                )
              : base(primaryProperty)
            {
                // If you are  going to add InputProperties make sure to
                // uncomment line below as InputProperties is NULL by default.
                //if (InputProperties == null) InputProperties = new List<IPropertyInfo>();

                // Add additional constructor code here.

                // Marke rule for IsAsync if Execute contains asyncronous code IsAsync = true; 
            }

            protected override void Execute(
                IRuleContext context
                )
            {
                TeamPlayer target = (TeamPlayer)context.Target;
                if (target.Parent == null)
                    return;

                Team team = (Team)target.Parent.Parent;
                var count = team.Players.Count(player => player.PlayerCode == target.PlayerCode);
                if (count > 1)
                    context.AddErrorResult(ComplexText.Player_PlayerCode_NotUnique);
            }
        }

        #endregion

        #region Business Methods

        /// <summary>
        /// Updates an editable model and its children from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <param name="childFactory">The child data portal factory.</param>
        public override async Task SetValuesOnBuild(
            TeamPlayerDto dto,
            IChildDataPortalFactory childFactory
            )
        {
            DataMapper.Map(dto, this);
            await BusinessRules.CheckRulesAsync();
        }

        #endregion

        #region Data Access

        [CreateChild]
        private async Task CreateAsync()
        {
            // Set values from data transfer object.
            //LoadProperty(PlayerCodeProperty, "");
            await BusinessRules.CheckRulesAsync();
        }

        [FetchChild]
        private async Task FetchAsync(
            TeamPlayerDao dao
            )
        {
            // Load values from persistent storage.
            await Task.Run(() =>
            {
                using (BypassPropertyChecks)
                    DataMapper.Map(dao, this);
            });
        }

        [InsertChild]
        private async Task InsertAsync(
            Team parent,
            [Inject] ITeamPlayerDal dal
            )
        {
            // Insert values into persistent storage.
            using (BypassPropertyChecks)
            {
                TeamKey = parent.TeamKey;
                var dao = Copy.PropertiesFrom(this).ToNew<TeamPlayerDao>();
                await dal.InsertAsync(dao);

                // Set new data.
                PlayerKey = dao.PlayerKey;
            }
            //await FieldManager.UpdateChildrenAsync(this);
        }

        [UpdateChild]
        private async Task UpdateAsync(
            Team parent,
            [Inject] ITeamPlayerDal dal
            )
        {
            // Update values in persistent storage.
            using (BypassPropertyChecks)
            {
                var dao = Copy.PropertiesFrom(this).ToNew<TeamPlayerDao>();
                await dal.UpdateAsync(dao);

                // Set new data.
            }
            //await FieldManager.UpdateChildrenAsync(this);
        }

        [DeleteSelfChild]
        private async Task Child_DeleteSelfAsync(
            Team parent,
            [Inject] ITeamPlayerDal dal
            )
        {
            // Delete values from persistent storage.

            //Items.Clear();
            //await FieldManager.UpdateChildrenAsync(this);

            TeamPlayerCriteria criteria = new TeamPlayerCriteria(PlayerKey);
            await dal.DeleteAsync(criteria);
        }

        #endregion
    }
}
