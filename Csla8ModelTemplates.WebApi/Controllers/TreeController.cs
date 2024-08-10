using Csla8ModelTemplates.Contracts.Tree.View;
using Csla8ModelTemplates.Models.Tree.Choice;
using Csla8ModelTemplates.Models.Tree.View;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoints for trees.
    /// </summary>
    [Route("api/tree")]
    [ApiController]
    [Produces("application/json")]
    public class TreeController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public TreeController(
            ILogger<TreeController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region Choice

        /// <summary>
        /// Gets the ID-name choice of the trees.
        /// </summary>
        /// <returns>The ID-name choice of the trees.</returns>
        [HttpGet("choice")]
        [ProducesResponseType(typeof(List<ChoiceItemDto<string?>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRootFolderChoice()
        {
            try
            {
                var choice = await RootFolderChoice.GetAsync(Factory);
                return Ok(choice.ToDto<ChoiceItemDto<string?>>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Tree

        /// <summary>
        /// Gets the specified folder tree.
        /// </summary>
        /// <param name="rootId">The identifier of the root folder.</param>
        /// <returns>The requested folder tree.</returns>
        [HttpGet("{rootId}")]
        [ProducesResponseType(typeof(List<FolderNodeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFolderTree(
            string rootId
            )
        {
            try
            {
                var tree = await FolderTree.GetAsync(Factory, rootId);
                return Ok(tree.ToDto<FolderNodeDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
