using Csla8RestApi.Dal.Contracts;
using System.Text.Json.Serialization;

namespace Csla8ModelTemplates.Contracts.Complex.Set
{
    /// <summary>
    /// Defines the editable team set player data.
    /// </summary>
    public class TeamSetPlayerData
    {
        public string? PlayerCode { get; set; }
        public string? PlayerName { get; set; }
        [JsonIgnore]
#pragma warning disable S1104
        public string? __teamCode; // for error messages
#pragma warning restore S1104
    }

    /// <summary>
    /// Defines the data access object of the editable team set player object.
    /// </summary>
    public class TeamSetPlayerDao : TeamSetPlayerData
    {
        public long? PlayerKey { get; set; }
        public long? TeamKey { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the editable team set player object.
    /// </summary>
    public class TeamSetPlayerDto : TeamSetPlayerData
    {
        public string? PlayerId { get; set; }
        public string? TeamId { get; set; }

        public TeamSetPlayerDao ToDao()
        {
            return new TeamSetPlayerDao
            {
                PlayerKey = KeyHash.Decode(ID.Player, PlayerId),
                TeamKey = KeyHash.Decode(ID.Team, TeamId),
                PlayerCode = PlayerCode,
                PlayerName = PlayerName,
                __teamCode = __teamCode
            };
        }
    }
}
