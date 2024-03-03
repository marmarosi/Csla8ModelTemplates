using Csla8ModelTemplates.Contracts.Selection.WithCode;
using Csla8ModelTemplates.Contracts.Selection.WithId;
using Csla8ModelTemplates.Contracts.Selection.WithKey;
using Csla8ModelTemplates.Models.Selection.WithCode;
using Csla8ModelTemplates.Models.Selection.WithId;
using Csla8ModelTemplates.Models.Selection.WithKey;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoints for selections.
    /// </summary>
    [ApiController]
    [Route("api/selection")]
    [Produces("application/json")]
    public class SelectionController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public SelectionController(
            ILogger<SelectionController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region Choice with key

        /// <summary>
        /// Gets the key-name choice of the teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <returns>The key-name choice of the teams.</returns>
        [HttpGet("with-key")]
        [ProducesResponseType(typeof(List<KeyNameOptionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamChoiceWithKey(
            [FromQuery] TeamKeyChoiceCriteria criteria
            )
        {
            try
            {
                var choice = await TeamKeyChoice.GetAsync(Factory, criteria);
                return Ok(choice.ToDto<KeyNameOptionDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Choice with ID

        /// <summary>
        /// Gets the ID-name choice of the teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <returns>The ID-name choice of the teams.</returns>
        [HttpGet("with-id")]
        [ProducesResponseType(typeof(List<IdNameOptionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamChoiceWithId(
            [FromQuery] TeamIdChoiceCriteria criteria
            )
        {
            try
            {
                var choice = await TeamIdChoice.GetAsync(Factory, criteria);
                return Ok(choice.ToDto<IdNameOptionDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Choice with Guid

        /// <summary>
        /// Gets the Guid-name choice of the teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <returns>The Guid-name choice of the teams.</returns>
        [HttpGet("with-guid")]
        [ProducesResponseType(typeof(List<GuidNameOptionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamChoiceWithGuid(
            [FromQuery] TeamGuidChoiceCriteria criteria
            )
        {
            try
            {
                var choice = await TeamGuidChoice.GetAsync(Factory, criteria);
                return Ok(choice.ToDto<GuidNameOptionDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Choice with code

        /// <summary>
        /// Gets the code-name choice of the teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <returns>The code-name choice of the tenants.</returns>
        [HttpGet("with-code")]
        [ProducesResponseType(typeof(List<CodeNameOptionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamChoiceWithCode(
            [FromQuery] TeamCodeChoiceCriteria criteria
            )
        {
            try
            {
                var choice = await TeamCodeChoice.GetAsync(Factory, criteria);
                return Ok(choice.ToDto<CodeNameOptionDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
