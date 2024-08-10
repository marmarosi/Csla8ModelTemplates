using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Simple.Set;
using Csla8RestApi.Tests.Models.Simple.Set;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoint for read set.
    /// </summary>
    [Route("api/read-set")]
    [ApiController]
    public class ReadSetController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public ReadSetController(
            ILogger<ReadSetController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region Read-Set

        /// <summary>
        /// Gets the specified product set to edit.
        /// </summary>
        /// <param name="criteria">The criteria of the product set.</param>
        /// <returns>The requested product set.</returns>
        [HttpGet("set")]
        [ProducesResponseType(typeof(IList<ProductSetItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductSet(
            [FromQuery] ProductSetCriteria criteria
            )
        {
            try
            {
                var products = await ProductSet.GetAsync(Factory, criteria);
                return Ok(products.ToDto());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
