using Csla8ModelTemplates.Contracts.Simple.Command;
using Csla8ModelTemplates.WebApi.Controllers;
using Csla8RestApi.Models.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.Tests.WebApi.Simple
{
    public class RenameTeam_Tests : TestBase
    {
        [Fact]
        public async Task RenameTeam_ReturnsTrue()
        {
            // ********** Arrange
            var setup = TestSetup.GetInstance();
            var logger = setup.GetLogger<SimpleController>();
            var sut = new SimpleController(logger, setup.Csla);

            // ********** Act
            var dto = new RenameTeamDto
            {
                TeamId = "oZkzGJ6G794",
                TeamName = "Team Thirty Seven"
            };
            var actionResult = await sut.RenameTeamCommand(dto);

            // ********** Assert
            if (IsDeadlock(actionResult, "RenameTeam - Execute")) return;
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var success = Assert.IsAssignableFrom<bool>(okObjectResult.Value);

            Assert.True(success);
        }
    }
}
