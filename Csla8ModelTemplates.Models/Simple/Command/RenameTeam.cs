using Csla;
using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Contracts.Simple.Command;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;

namespace Csla8ModelTemplates.Models.Simple.Command
{
    /// <summary>
    /// Renames the specified team.
    /// </summary>
    [Serializable]
    public class RenameTeam : CommandBase<RenameTeam>
    {
        #region Properties

        public static readonly PropertyInfo<long?> TeamKeyProperty = RegisterProperty<long?>(nameof(TeamKey));
        public long? TeamKey
        {
            get => ReadProperty(TeamKeyProperty);
            private set => LoadProperty(TeamKeyProperty, value);
        }

        public static readonly PropertyInfo<long?> TeamIdProperty = RegisterProperty<long?>(nameof(TeamId), RelationshipTypes.PrivateField);
        public string TeamId
        {
            get => KeyHash.Encode(ID.Team, TeamKey);
            set => TeamKey = KeyHash.Decode(ID.Team, value);
        }

        public static readonly PropertyInfo<string?> TeamNameProperty = RegisterProperty<string?>(c => c.TeamName);
        public string? TeamName
        {
            get => ReadProperty(TeamNameProperty);
            private set => LoadProperty(TeamNameProperty, value);
        }

        public static readonly PropertyInfo<bool> ResultProperty = RegisterProperty<bool>(c => c.Result);
        public bool Result
        {
            get => ReadProperty(ResultProperty);
            private set => LoadProperty(ResultProperty, value);
        }

        #endregion

        #region Business Rules

        private void Validate()
        {
            if (string.IsNullOrEmpty(TeamName))
                throw new BrokenRulesException(
                    nameof(RenameTeam),
                    nameof(TeamName),
                    SimpleText.RenameTeam_TeamName_Required
                    );
        }

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(RenameTeam),
        //        new IsInRole(
        //            AuthorizationActions.ExecuteMethod,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Renames the specified team.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="dto">The data transer object of the rename team command.</param>
        /// <returns>The command instance.</returns>
        public static async Task<RenameTeam> ExecuteAsync(
            IDataPortalFactory factory,
            RenameTeamDto dto
            )
        {
            return await factory.GetPortal<RenameTeam>().ExecuteAsync(dto);
        }

        #endregion

        #region Data Access

        [Execute]
        private async Task ExecuteAsync(
            RenameTeamDto dto,
            [Inject] IRenameTeamDal dal
            )
        {
            // Execute the command.
            TeamId = dto.TeamId!;
            TeamName = dto.TeamName;
            Validate();

            using (var transaction = dal.BeginTransaction())
            {
                RenameTeamDao dao = new RenameTeamDao(TeamKey ?? 0, TeamName);
                await dal.ExecuteAsync(dao);
            }

            // Set new data.
            Result = true;
        }

        #endregion
    }
}
