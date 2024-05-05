using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Tests.Contracts.Simple.Command;
using Csla8RestApi.Tests.Contracts.Simple.Edit;
using Csla8RestApi.Tests.Contracts.Simple.List;
using Csla8RestApi.Tests.Contracts.Simple.Set;
using Csla8RestApi.Tests.Contracts.Simple.View;
using Csla8RestApi.Tests.Models.Simple.Command;
using Csla8RestApi.Tests.Models.Simple.Edit;
using Csla8RestApi.Tests.Models.Simple.List;
using Csla8RestApi.Tests.Models.Simple.Set;
using Csla8RestApi.Tests.Models.Simple.View;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoints for products.
    /// </summary>
    [Route("api/product")]
    [ApiController]
    public class ProductController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public ProductController(
            ILogger<ProductController> logger,
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

        #region View

        /// <summary>
        /// Gets the specified product details to display.
        /// </summary>
        /// <param name="productId">The identifier of the product .</param>
        /// <returns>The requested product  view.</returns>
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

        #region Command

        /// <summary>
        /// Executes the clear product command.
        /// </summary>
        /// <param name="dto">The data transer object of the clear product command.</param>
        /// <returns>True when the command succeeded; otherwise false.</returns>
        [HttpPatch]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ClearProductCommand(
            [FromBody] ClearProductDto dto
            )
        {
            try
            {
                return Ok(await RetryOnDeadlock(async () =>
                {
                    var command = await ClearProduct.ExecuteAsync(Factory, dto);
                    return command.Result;
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
