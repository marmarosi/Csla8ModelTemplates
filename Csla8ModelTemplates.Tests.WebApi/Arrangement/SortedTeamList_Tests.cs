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
                Assert.NotNull(item.TeamCode);
                Assert.NotNull(item.TeamName);
                string teamCode = item.TeamCode;
                string teamName = item.TeamName;
#pragma warning disable S6610 // "StartsWith" and "EndsWith" overloads that take a "char" should be used instead of the ones that take a "string"
                Assert.True(teamCode.EndsWith("5") || teamCode.EndsWith("50"));
                Assert.True(teamName.EndsWith("5") || teamName.EndsWith("50"));
#pragma warning restore S6610 // "StartsWith" and "EndsWith" overloads that take a "char" should be used instead of the ones that take a "string"
            }
        }
    }
}
