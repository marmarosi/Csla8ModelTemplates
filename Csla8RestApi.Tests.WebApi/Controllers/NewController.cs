using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Simple.Edit;
using Csla8RestApi.Tests.Models.Simple.Edit;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoint for new.
    /// </summary>
    [Route("api/new")]
    [ApiController]
    public class NewController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public NewController(
            ILogger<NewController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region New

        /// <summary>
        /// Gets a new product to edit.
        /// </summary>
        /// <returns>The new product.</returns>
        [HttpGet("new")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNewProduct()
        {
            try
            {
                var product = await Product.NewAsync(Factory);
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
