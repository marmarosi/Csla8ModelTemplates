using Csla8ModelTemplates.Contracts.Selection.ByCode;
using Csla8ModelTemplates.Contracts.Selection.ByGuid;
using Csla8ModelTemplates.Contracts.Selection.ById;
using Csla8ModelTemplates.Contracts.Selection.ByKey;
using Csla8ModelTemplates.Models.Selection.ByCode;
using Csla8ModelTemplates.Models.Selection.ByGuid;
using Csla8ModelTemplates.Models.Selection.ById;
using Csla8ModelTemplates.Models.Selection.ByKey;
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

        #region Choice by key

        /// <summary>
        /// Gets the key-name choice of the teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <returns>The key-name choice of the teams.</returns>
        [HttpGet("by-key")]
        [ProducesResponseType(typeof(List<ChoiceItemDto<long?>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamByKeyChoice(
            [FromQuery] TeamByKeyChoiceCriteria criteria
            )
        {
            try
            {
                var choice = await TeamByKeyChoice.GetAsync(Factory, criteria);
                return Ok(choice.ToDto<ChoiceItemDto<long?>>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Choice by ID

        /// <summary>
        /// Gets the ID-name choice of the teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <returns>The ID-name choice of the teams.</returns>
        [HttpGet("by-id")]
        [ProducesResponseType(typeof(List<ChoiceItemDto<string?>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamByIdChoice(
            [FromQuery] TeamByIdChoiceCriteria criteria
            )
        {
            try
            {
                var choice = await TeamByIdChoice.GetAsync(Factory, criteria);
                return Ok(choice.ToDto<ChoiceItemDto<string?>>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Choice by Guid

        /// <summary>
        /// Gets the Guid-name choice of the teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <returns>The Guid-name choice of the teams.</returns>
        [HttpGet("by-guid")]
        [ProducesResponseType(typeof(List<ChoiceItemDto<Guid?>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamByGuidChoice(
            [FromQuery] TeamByGuidChoiceCriteria criteria
            )
        {
            try
            {
                var choice = await TeamByGuidChoice.GetAsync(Factory, criteria);
                return Ok(choice.ToDto<ChoiceItemDto<Guid?>>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Choice by code

        /// <summary>
        /// Gets the code-name choice of the teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <returns>The code-name choice of the teams.</returns>
        [HttpGet("by-code")]
        [ProducesResponseType(typeof(List<ChoiceItemDto<string?>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamByCodeChoice(
            [FromQuery] TeamByCodeChoiceCriteria criteria
            )
        {
            try
            {
                var choice = await TeamByCodeChoice.GetAsync(Factory, criteria);
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
