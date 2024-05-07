using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Selection.WithGuid;
using Csla8RestApi.Tests.Models.Selection.WithGuid;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoints for selections.
    /// </summary>
    [Route("api/selection")]
    [ApiController]
    public class SelectWithGuidController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public SelectWithGuidController(
            ILogger<SelectWithGuidController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region Choice with Guid

        /// <summary>
        /// Gets the Guid-name choice of the products.
        /// </summary>
        /// <param name="criteria">The criteria of the product choice.</param>
        /// <returns>The Guid-name choice of the products.</returns>
        [HttpGet("choice")]
        [ProducesResponseType(typeof(List<ChoiceItemDto<Guid?>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductChoice(
            [FromQuery] ProductChoiceCriteria criteria
            )
        {
            try
            {
                var choice = await ProductChoice.GetAsync(Factory, criteria);
                return Ok(choice.ToDto<ChoiceItemDto<Guid?>>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
