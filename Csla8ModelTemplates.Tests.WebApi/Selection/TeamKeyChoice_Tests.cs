using Csla8ModelTemplates.Contracts.Selection.ByKey;
using Csla8ModelTemplates.WebApi.Controllers;
using Csla8RestApi.Dal.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.Tests.WebApi.Selection
{
    public class TeamKeyChoice_Tests
    {
        [Fact]
        public async Task GetTeamByKeyChoice_ReturnsAChoice()
        {
            // ********** Arrange
            var setup = TestSetup.GetInstance();
            var logger = setup.GetLogger<SelectionController>();
            var sut = new SelectionController(logger, setup.Csla);

            // ********** Act
            var actionResult = await sut.GetTeamByKeyChoice(
                new TeamByKeyChoiceCriteria { TeamName = "7" }
                );

            // ********** Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var choice = Assert.IsAssignableFrom<IList<ChoiceItemDto<long?>>>(okObjectResult.Value);

            // The choice must have 5 items.
            Assert.Equal(5, choice.Count);

            // The names must end with 7.
            foreach (var item in choice)
            {
                Assert.EndsWith("7", item.Name);
            }
        }
    }
}
