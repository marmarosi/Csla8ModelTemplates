using Csla8RestApi.Models.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoints for teams.
    /// </summary>
    [Route("api/team")]
    [ApiController]
    [Produces("application/json")]
    public class EmptyController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public EmptyController(
            ILogger<EmptyController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

    }
}
