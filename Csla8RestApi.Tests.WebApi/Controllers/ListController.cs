using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Simple.List;
using Csla8RestApi.Tests.Models.Simple.List;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoint for list.
    /// </summary>
    [Route("api/list")]
    [ApiController]
    public class ListController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public ListController(
            ILogger<ListController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region List

        /// <summary>
        /// Gets a list of products.
        /// </summary>
        /// <param name="criteria">The criteria of the product list.</param>
        /// <returns>The requested product list.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IList<ProductListItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductList(
            [FromQuery] ProductListCriteria criteria
            )
        {
            try
            {
                var list = await ProductList.GetAsync(Factory, criteria);
                return Ok(list.ToDto<ProductListItemDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
