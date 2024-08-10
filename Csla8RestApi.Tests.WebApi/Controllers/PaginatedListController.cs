using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Arrangement.Pagination;
using Csla8RestApi.Tests.Models.Arrangement.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoint for paginated list.
    /// </summary>
    [Route("api/paginated-list")]
    [ApiController]
    public class PaginatedListController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public PaginatedListController(
            ILogger<PaginatedListController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region Pagination

        /// <summary>
        /// Gets the specified page of products.
        /// </summary>
        /// <param name="criteria">The criteria of the product list.</param>
        /// <returns>The requested page of the product list.</returns>
        [HttpGet("paginated")]
        [ProducesResponseType(typeof(PaginatedList<ProductListItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductList(
            [FromQuery] ProductListCriteria criteria
            )
        {
            try
            {
                var list = await ProductList.GetAsync(Factory, criteria);
                return Ok(list.ToDto<PaginatedList<ProductListItemDto>>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
