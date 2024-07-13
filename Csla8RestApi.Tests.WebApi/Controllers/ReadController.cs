using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Simple.Edit;
using Csla8RestApi.Tests.Models.Simple.Edit;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoint for read.
    /// </summary>
    [Route("api/read")]
    [ApiController]
    public class ReadController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public ReadController(
            ILogger<ReadController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region Read

        /// <summary>
        /// Gets the specified product to edit.
        /// </summary>
        /// <param name="productId">The identifier of the product.</param>
        /// <returns>The requested product.</returns>
        [HttpGet("{productId}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProduct(
            string productId
            )
        {
            try
            {
                var product = await Product.GetAsync(Factory, productId);
                return Ok(product.ToDto());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
