using Csla8ModelTemplates.WebApi.Controllers;
using Csla8RestApi.Dal.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.Tests.WebApi.Tree
{
    public class RootFolderChoice_Tests
    {
        [Fact]
        public async Task GetRootFolderChoice_ReturnsAChoice()
        {
            // ********** Arrange
            var setup = TestSetup.GetInstance();
            var logger = setup.GetLogger<TreeController>();
            var sut = new TreeController(logger, setup.Csla);

            // ********** Act
            var actionResult = await sut.GetRootFolderChoice();

            // ********** Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var choice = Assert.IsAssignableFrom<IList<ChoiceItemDto<string?>>>(okObjectResult.Value);

            // The choice must have 3 items.
            Assert.Equal(3, choice.Count);

            // The names must start with 'Folder entry'.
            foreach (var item in choice)
            {
                Assert.StartsWith("Folder entry", item.Name);
            }
        }
    }
}
