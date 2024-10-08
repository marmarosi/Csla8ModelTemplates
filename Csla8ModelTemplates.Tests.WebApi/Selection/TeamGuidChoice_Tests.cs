using Csla8ModelTemplates.Contracts.Selection.ByGuid;
using Csla8ModelTemplates.WebApi.Controllers;
using Csla8RestApi.Dal.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.Tests.WebApi.Selection
{
    public class TeamGuidChoice_Tests
    {
        [Fact]
        public async Task GetTeamByGuidChoice_ReturnsAChoice()
        {
            // ********** Arrange
            var setup = TestSetup.GetInstance();
            var logger = setup.GetLogger<SelectionController>();
            var sut = new SelectionController(logger, setup.Csla);

            // ********** Act
            var actionResult = await sut.GetTeamByGuidChoice(
                new TeamByGuidChoiceCriteria { TeamName = "5" }
                );

            // ********** Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var choice = Assert.IsAssignableFrom<IList<ChoiceItemDto<Guid?>>>(okObjectResult.Value);

            // The choice must have 5 items.
            Assert.Equal(6, choice.Count);

            // The names must end with 0.
            foreach (var item in choice)
            {
                Assert.Contains("5", item.Name);
            }
        }
    }
}
