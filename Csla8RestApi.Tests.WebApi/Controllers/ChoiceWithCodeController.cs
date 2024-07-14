using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Selection.WithCode;
using Csla8RestApi.Tests.Models.Selection.WithCode;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoint for choice with code.
    /// </summary>
    [Route("api/with-code")]
    [ApiController]
    public class ChoiceWithCodeController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public ChoiceWithCodeController(
            ILogger<ChoiceWithCodeController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region Choice with code

        /// <summary>
        /// Gets the code-name choice of the products.
        /// </summary>
        /// <param name="criteria">The criteria of the product choice.</param>
        /// <returns>The code-name choice of the products.</returns>
        [HttpGet("with-code")]
        [ProducesResponseType(typeof(List<ChoiceItemDto<string?>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductChoice(
            [FromQuery] ProductChoiceCriteria criteria
            )
        {
            try
            {
                var choice = await ProductChoice.GetAsync(Factory, criteria);
                return Ok(choice.ToDto<ChoiceItemDto<string?>>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
