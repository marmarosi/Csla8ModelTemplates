using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Simple.Command;
using Csla8RestApi.Tests.Models.Simple.Command;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoint for command.
    /// </summary>
    [Route("api/command")]
    [ApiController]
    public class CommandController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public CommandController(
            ILogger<CommandController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region Command

        /// <summary>
        /// Executes the clear product command.
        /// </summary>
        /// <param name="dto">The data transer object of the clear product command.</param>
        /// <returns>True when the command succeeded; otherwise false.</returns>
        [HttpPatch]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ClearProductCommand(
            [FromBody] ClearProductDto dto
            )
        {
            try
            {
                return Ok(await RetryOnDeadlock(async () =>
                {
                    var command = await ClearProduct.ExecuteAsync(Factory, dto);
                    return command.Result;
                }));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
