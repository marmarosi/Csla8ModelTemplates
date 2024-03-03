using Csla8ModelTemplates.Contracts.Junction.View;
using Csla8ModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.Tests.WebApi.Junction
{
    public class GroupView_Tests
    {
        [Fact]
        public async Task GetGroupView_ReturnsAView()
        {
            // ********** Arrange
            var setup = TestSetup.GetInstance();
            var logger = setup.GetLogger<JunctionController>();
            var sut = new JunctionController(logger, setup.Csla);

            // ********** Act
            var actionResult = await sut.GetGroupView("oQLOyK85x6g");

            // ********** Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var group = Assert.IsAssignableFrom<GroupViewDto>(okObjectResult.Value);

            // The code and name must end with 17.
            Assert.Equal("G-08", group.GroupCode);
            Assert.EndsWith("8", group.GroupName);
            Assert.True(group.Persons.Count > 0);

            // The code and name must end with 17.
            var groupPerson = group.Persons[0];
            Assert.StartsWith("Person #", groupPerson.PersonName);
        }
    }
}
