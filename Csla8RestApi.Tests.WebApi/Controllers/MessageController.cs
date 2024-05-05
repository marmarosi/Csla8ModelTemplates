using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Tree.View;
using Csla8RestApi.Tests.Models.Tree.View;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoints for messages.
    /// </summary>
    [Route("api/message")]
    [ApiController]
    public class MessageController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public MessageController(
            ILogger<MessageController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region Tree

        /// <summary>
        /// Gets the specified message tree.
        /// </summary>
        /// <param name="rootId">The identifier of the root message.</param>
        /// <returns>The requested message tree.</returns>
        [HttpGet("{rootId}")]
        [ProducesResponseType(typeof(List<MessageNodeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMessageTree(
            string rootId
            )
        {
            try
            {
                var tree = await MessageTree.GetAsync(Factory, rootId);
                return Ok(tree.ToDto<MessageNodeDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
