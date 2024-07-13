using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Simple.Set;
using Csla8RestApi.Tests.Models.Simple.Set;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoint for update set.
    /// </summary>
    [Route("api/update-set")]
    [ApiController]
    public class UpdateSetController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public UpdateSetController(
            ILogger<UpdateSetController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region Update-Set

        /// <summary>
        /// Updates the specified product set.
        /// </summary>
        /// <param name="criteria">The criteria of the product set.</param>
        /// <param name="dto">The data transer objects of the product set.</param>
        /// <returns>The updated product set.</returns>
        [HttpPut("set")]
        [ProducesResponseType(typeof(IList<ProductSetItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProductSet(
            [FromQuery] ProductSetCriteria criteria,
            [FromBody] List<ProductSetItemDto> dto
            )
        {
            try
            {
                return Ok(await RetryOnDeadlock(async () =>
                {
                    var products = await ProductSet.BuildAsync(Factory, ChildFactory, criteria, dto);
                    if (products.IsSavable)
                    {
                        products = await products.SaveAsync();
                    }
                    return products.ToDto();
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
