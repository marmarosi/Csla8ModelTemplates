using Csla8ModelTemplates.Contracts.Complex.List;
using Csla8ModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.Tests.WebApi.Complex
{
    public class TeamList_Tests
    {
        [Fact]
        public async Task GetTeamList_ReturnsAList()
        {
            // ********** Arrange
            var setup = TestSetup.GetInstance();
            var logger = setup.GetLogger<ComplexController>();
            var sut = new ComplexController(logger, setup.Csla);

            // ********** Act
            TeamListCriteria criteria = new TeamListCriteria { TeamName = "6" };
            var actionResult = await sut.GetTeamList(criteria);

            // ********** Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var list = Assert.IsAssignableFrom<IList<TeamListItemDto>>(okObjectResult.Value);

            // The choice must have 5 items.
            Assert.Equal(5, list.Count);

            // The team code and names must end with 6.
            foreach (var team in list)
            {
                Assert.EndsWith("6", team.TeamCode);
                Assert.EndsWith("6", team.TeamName);
                Assert.True(team.Players.Count > 0);

                // The player code and names must contain 6.
                var player = team.Players[0];
                Assert.Contains("6", player.PlayerCode);
                Assert.Contains("6.", player.PlayerName);
            }
        }
    }
}
