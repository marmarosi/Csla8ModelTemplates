using Csla8RestApi.Models.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Csla8RestApi.Tests.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoints for products.
    /// </summary>
    [Route("api/Product")]
    [ApiController]
    [Produces("application/json")]
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

    }
}
