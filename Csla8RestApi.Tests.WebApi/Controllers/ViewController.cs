using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Simple.View;
using Csla8RestApi.Tests.Models.Simple.View;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoint for view.
    /// </summary>
    [Route("api/view")]
    [ApiController]
    public class ViewController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public ViewController(
            ILogger<ViewController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region View

        /// <summary>
        /// Gets the specified product details to display.
        /// </summary>
        /// <param name="productId">The identifier of the product.</param>
        /// <returns>The requested product view.</returns>
        [HttpGet("{productId}/view")]
        [ProducesResponseType(typeof(ProductViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductView(
            string productId
            )
        {
            try
            {
                var product = await ProductView.GetAsync(Factory, productId);
                return Ok(product.ToDto<ProductViewDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
