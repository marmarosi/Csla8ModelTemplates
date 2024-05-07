using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Selection.WithId;
using Csla8RestApi.Tests.Models.Selection.WithId;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoints for selections.
    /// </summary>
    [Route("api/selection")]
    [ApiController]
    public class SelectWithIdController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public SelectWithIdController(
            ILogger<SelectWithIdController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region Choice with ID

        /// <summary>
        /// Gets the ID-name choice of the products.
        /// </summary>
        /// <param name="criteria">The criteria of the product choice.</param>
        /// <returns>The ID-name choice of the products.</returns>
        [HttpGet("choice")]
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
