using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Simple.Edit;
using Csla8RestApi.Tests.Models.Simple.Edit;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoint for delete.
    /// </summary>
    [Route("api/delete")]
    [ApiController]
    public class DeleteController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public DeleteController(
            ILogger<DeleteController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the specified product.
        /// </summary>
        /// <param name="productId">The identifier of the product.</param>
        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProduct(
            string productId
            )
        {
            try
            {
                await RetryOnDeadlock(async () =>
                {
                    await Product.DeleteAsync(Factory, productId);
                });
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
