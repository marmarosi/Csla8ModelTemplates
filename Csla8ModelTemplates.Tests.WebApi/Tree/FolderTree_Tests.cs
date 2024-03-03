using Csla8ModelTemplates.Contracts.Tree.View;
using Csla8ModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.Tests.WebApi.Tree
{
    public class FolderTree_Tests
    {
        [Fact]
        public async Task GetFolderTree_ReturnsATree()
        {
            // ********** Arrange
            TestSetup setup = TestSetup.GetInstance();
            var logger = setup.GetLogger<TreeController>();
            var sut = new TreeController(logger, setup.Csla);

            // ********** Act
            var actionResult = await sut.GetFolderTree("7x95p9vYaZz");

            // ********** Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var tree = Assert.IsAssignableFrom<IList<FolderNodeDto>>(okObjectResult.Value);

            // The tree must have one root node.
            Assert.Single(tree);

            // Level 1 - root node
            var nodeLevel1 = tree[0];
            Assert.Equal(1, nodeLevel1.Level);
            Assert.True(nodeLevel1.Children.Count > 0);

            // Level 2
            var nodeLevel2 = nodeLevel1.Children[0];
            Assert.Equal(2, nodeLevel2.Level);
            Assert.True(nodeLevel2.Children.Count > 0);

            // Level 3
            var nodeLevel3 = nodeLevel2.Children[0];
            Assert.Equal(3, nodeLevel3.Level);
            Assert.True(nodeLevel3.Children.Count > 0);

            // Level 4
            var nodeLevel4 = nodeLevel3.Children[0];
            Assert.Equal(4, nodeLevel4.Level);
            Assert.Empty(nodeLevel4.Children);
        }
    }
}
