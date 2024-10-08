using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Selection.ByKey;
using Csla8RestApi.Tests.Models.Selection.ByKey;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoint for choice by key.
    /// </summary>
    [Route("api/by-key")]
    [ApiController]
    public class ChoiceByKeyController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public ChoiceByKeyController(
            ILogger<ChoiceByKeyController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region Choice by key

        /// <summary>
        /// Gets the key-name choice of the products.
        /// </summary>
        /// <param name="criteria">The criteria of the product choice.</param>
        /// <returns>The key-name choice of the products.</returns>
        [HttpGet("by-key")]
        [ProducesResponseType(typeof(List<ChoiceItemDto<long?>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductChoice(
            [FromQuery] ProductChoiceCriteria criteria
            )
        {
            try
            {
                var choice = await ProductChoice.GetAsync(Factory, criteria);
                return Ok(choice.ToDto<ChoiceItemDto<long?>>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
