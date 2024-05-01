using Csla;
using Csla.Data;
using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Contracts.Complex.List;
using Csla8RestApi.Models;
using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Models.Complex.List
{
    /// <summary>
    /// Represents an item in a read-only player collection.
    /// </summary>
    [Serializable]
    public class TeamListPlayer : ReadOnlyModel<TeamListPlayer>
    {
        #region Properties

        public static readonly PropertyInfo<long?> PlayerKeyProperty = RegisterProperty<long?>(nameof(PlayerKey));
        public long? PlayerKey
        {
            get => GetProperty(PlayerKeyProperty);
            private set => LoadProperty(PlayerKeyProperty, value);
        }

        public static readonly PropertyInfo<string?> PlayerIdProperty = RegisterProperty<string?>(nameof(PlayerId), RelationshipTypes.PrivateField);
        public string? PlayerId
        {
            get => KeyHash.Encode(ID.Player, PlayerKey);
            private set => PlayerKey = KeyHash.Decode(ID.Player, value);
        }

        public static readonly PropertyInfo<string?> PlayerCodeProperty = RegisterProperty<string?>(nameof(PlayerCode));
        public string? PlayerCode
        {
            get => GetProperty(PlayerCodeProperty);
            private set => LoadProperty(PlayerCodeProperty, value);
        }

        public static readonly PropertyInfo<string?> PlayerNameProperty = RegisterProperty<string?>(nameof(PlayerName));
        public string? PlayerName
        {
            get => GetProperty(PlayerNameProperty);
            private set => LoadProperty(PlayerNameProperty, value);
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        new IsInRole(
        //            AuthorizationActions.ReadProperty,
        //            PlayerNameProperty,
        //            "Manager"
        //            )
        //        );
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(PlayerInfo),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Data Access

        [FetchChild]
        private async Task FetchAsync(
            TeamListPlayerDao dao
            )
        {
            // Load values from persistent storage.
            await Task.Run(() =>
            {
                DataMapper.Map(dao, this);
            });
        }

        #endregion
    }
}
