using Csla8ModelTemplates.Contracts.Arrangement.Sorting;
using Csla8ModelTemplates.WebApi.Controllers;
using Csla8RestApi.Dal.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.Tests.WebApi.Arrangement
{
    public class SortedTeamList_Tests
    {
        [Fact]
        public async Task GetSortedTeamList_ReturnsAList()
        {
            // ********** Arrange
            TestSetup setup = TestSetup.GetInstance();
            var logger = setup.GetLogger<ArrangementController>();
            var sut = new ArrangementController(logger, setup.Csla);

            // ********** Act
            SortedTeamListCriteria criteria = new SortedTeamListCriteria
            {
                TeamName = "5",
                SortBy = SortedTeamListSortBy.TeamCode,
                SortDirection = SortDirection.Descending
            };
            var actionResult = await sut.GetSortedTeamList(criteria);

            // ********** Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var list = Assert.IsAssignableFrom<IList<SortedTeamListItemDto>>(okObjectResult.Value);

            // The list must have 6 items.
            Assert.Equal(6, list.Count);

            // The code and names must end with 5 or 50.
            foreach (var item in list)
            {
                Assert.True(item.TeamCode.EndsWith("5") || item.TeamCode.EndsWith("50"));
                Assert.True(item.TeamName.EndsWith("5") || item.TeamName.EndsWith("50"));
            }
        }
    }
}
