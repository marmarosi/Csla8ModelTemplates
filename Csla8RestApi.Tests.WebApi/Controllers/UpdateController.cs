using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Simple.Edit;
using Csla8RestApi.Tests.Models.Simple.Edit;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoint for update.
    /// </summary>
    [Route("api/update")]
    [ApiController]
    public class UpdateController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public UpdateController(
            ILogger<UpdateController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region Update

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="dto">The data transer object of the product.</param>
        /// <returns>The updated product.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct(
            [FromBody] ProductDto dto
            )
        {
            try
            {
                return Ok(await RetryOnDeadlock(async () =>
                {
                    var product = await Product.BuildAsync(Factory, ChildFactory, dto);
                    if (product.IsSavable)
                    {
                        product = await product.SaveAsync();
                    }
                    return product.ToDto();
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
