using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Simple.Edit;
using Csla8RestApi.Tests.Models.Simple.Edit;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoint for create.
    /// </summary>
    [Route("api/create")]
    [ApiController]
    public class CreateController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public CreateController(
            ILogger<CreateController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region Create

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="dto">The data transer object of the product.</param>
        /// <returns>The created product.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProduct(
            [FromBody] ProductDto dto
            )
        {
            try
            {
                return Created(Uri, await RetryOnDeadlock(async () =>
                {
                    var product = await Product.BuildAsync(Factory, ChildFactory, dto);
                    if (product.IsValid)
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
