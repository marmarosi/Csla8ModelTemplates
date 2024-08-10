using Csla8ModelTemplates.Contracts.Simple.List;
using Csla8ModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.Tests.WebApi.Simple
{
    public class SimpleTeamList_Tests
    {
        [Fact]
        public async Task GetTeamList_ReturnsAList()
        {
            // ********** Arrange
            var setup = TestSetup.GetInstance();
            var logger = setup.GetLogger<SimpleController>();
            var sut = new SimpleController(logger, setup.Csla);

            // ********** Act
            var actionResult = await sut.GetTeamList(
                new SimpleTeamListCriteria { TeamName = "9" }
                );

            // ********** Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var list = Assert.IsAssignableFrom<IList<SimpleTeamListItemDto>>(okObjectResult.Value);

            // The list must have 5 items.
            Assert.Equal(5, list.Count);

            // The code and names must end with 9.
            foreach (var item in list)
            {
                Assert.EndsWith("9", item.TeamCode);
                Assert.EndsWith("9", item.TeamName);
            }
        }
    }
}
