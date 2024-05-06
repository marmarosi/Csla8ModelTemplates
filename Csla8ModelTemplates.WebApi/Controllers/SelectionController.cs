using Csla8ModelTemplates.Contracts.Selection.WithCode;
using Csla8ModelTemplates.Contracts.Selection.WithGuid;
using Csla8ModelTemplates.Contracts.Selection.WithId;
using Csla8ModelTemplates.Contracts.Selection.WithKey;
using Csla8ModelTemplates.Models.Selection.WithCode;
using Csla8ModelTemplates.Models.Selection.WithGuid;
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
        [ProducesResponseType(typeof(List<ChoiceItemDto<long?>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamWithKeyChoice(
            [FromQuery] TeamWithKeyChoiceCriteria criteria
            )
        {
            try
            {
                var choice = await TeamWithKeyChoice.GetAsync(Factory, criteria);
                return Ok(choice.ToDto<ChoiceItemDto<long?>>());
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
        [ProducesResponseType(typeof(List<ChoiceItemDto<string?>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamWithIdChoice(
            [FromQuery] TeamWithIdChoiceCriteria criteria
            )
        {
            try
            {
                var choice = await TeamWithIdChoice.GetAsync(Factory, criteria);
                return Ok(choice.ToDto<ChoiceItemDto<string?>>());
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
        [ProducesResponseType(typeof(List<ChoiceItemDto<Guid?>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamWithGuidChoice(
            [FromQuery] TeamWithGuidChoiceCriteria criteria
            )
        {
            try
            {
                var choice = await TeamWithGuidChoice.GetAsync(Factory, criteria);
                return Ok(choice.ToDto<ChoiceItemDto<Guid?>>());
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
        /// <returns>The code-name choice of the teams.</returns>
        [HttpGet("with-code")]
        [ProducesResponseType(typeof(List<ChoiceItemDto<string?>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamWithCodeChoice(
            [FromQuery] TeamWithCodeChoiceCriteria criteria
            )
        {
            try
            {
                var choice = await TeamWithCodeChoice.GetAsync(Factory, criteria);
                return Ok(choice.ToDto<ChoiceItemDto<string?>>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
