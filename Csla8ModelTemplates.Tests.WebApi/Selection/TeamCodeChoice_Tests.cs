using Csla8ModelTemplates.Contracts.Selection.WithCode;
using Csla8ModelTemplates.WebApi.Controllers;
using Csla8RestApi.Dal.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.Tests.WebApi.Selection
{
    public class TeamCodeChoice_Tests
    {
        [Fact]
        public async Task GetTeamWithCodeChoice_ReturnsAChoice()
        {
            // ********** Arrange
            var setup = TestSetup.GetInstance();
            var logger = setup.GetLogger<SelectionController>();
            var sut = new SelectionController(logger, setup.Csla);

            // ********** Act
            var actionResult = await sut.GetTeamWithCodeChoice(
                new TeamWithCodeChoiceCriteria { TeamName = "9" }
                );

            // ********** Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var choice = Assert.IsAssignableFrom<IList<ChoiceItemDto<string?>>>(okObjectResult.Value);

            // The choice must have 5 items.
            Assert.Equal(5, choice.Count);

            // The codes and names must end with 9.
            foreach (var option in choice)
            {
                Assert.EndsWith("9", option.Value);
                Assert.EndsWith("9", option.Name);
            }
        }
    }
}
