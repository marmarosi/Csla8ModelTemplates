using Csla8ModelTemplates.Contracts.Simple.View;
using Csla8ModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.Tests.WebApi.Simple
{
    public class SimpleTeamView_Tests
    {
        [Fact]
        public async Task GetTeamView_ReturnsAView()
        {
            // ********** Arrange
            var setup = TestSetup.GetInstance();
            var logger = setup.GetLogger<SimpleController>();
            var sut = new SimpleController(logger, setup.Csla);

            // ********** Act
            var actionResult = await sut.GetTeamView("d9A30RLG8pZ");

            // ********** Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var team = Assert.IsAssignableFrom<SimpleTeamViewDto>(okObjectResult.Value);

            // The code and name must end with 31.
            Assert.Equal("T-0031", team.TeamCode);
            Assert.EndsWith("31", team.TeamName);
        }
    }
}
