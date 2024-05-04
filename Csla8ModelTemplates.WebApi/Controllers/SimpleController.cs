using Csla8ModelTemplates.Contracts.Simple.Command;
using Csla8ModelTemplates.Contracts.Simple.Edit;
using Csla8ModelTemplates.Contracts.Simple.List;
using Csla8ModelTemplates.Contracts.Simple.Set;
using Csla8ModelTemplates.Contracts.Simple.View;
using Csla8ModelTemplates.Models.Simple.Command;
using Csla8ModelTemplates.Models.Simple.Edit;
using Csla8ModelTemplates.Models.Simple.List;
using Csla8ModelTemplates.Models.Simple.Set;
using Csla8ModelTemplates.Models.Simple.View;
using Csla8RestApi.Models.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoints for simple models.
    /// </summary>
    [Route("api/simple")]
    [ApiController]
    public class SimpleController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public SimpleController(
            ILogger<SimpleController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region List

        /// <summary>
        /// Gets a list of teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>The requested team list.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IList<SimpleTeamListItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamList(
            [FromQuery] SimpleTeamListCriteria criteria
            )
        {
            try
            {
                var list = await SimpleTeamList.GetAsync(Factory, criteria);
                return Ok(list.ToDto<SimpleTeamListItemDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region View

        /// <summary>
        /// Gets the specified team details to display.
        /// </summary>
        /// <param name="teamId">The identifier of the team.</param>
        /// <returns>The requested team view.</returns>
        [HttpGet("{id}/view")]
        [ProducesResponseType(typeof(SimpleTeamViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamView(
            string teamId
            )
        {
            try
            {
                var team = await SimpleTeamView.GetAsync(Factory, teamId);
                return Ok(team.ToDto<SimpleTeamViewDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region New

        /// <summary>
        /// Gets a new team to edit.
        /// </summary>
        /// <returns>The new team.</returns>
        [HttpGet("new")]
        [ProducesResponseType(typeof(SimpleTeamDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNewTeam()
        {
            try
            {
                var team = await SimpleTeam.NewAsync(Factory);
                return Ok(team.ToDto());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Create

        /// <summary>
        /// Creates a new team.
        /// </summary>
        /// <param name="dto">The data transer object of the team.</param>
        /// <returns>The created team.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(SimpleTeamDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTeam(
            [FromBody] SimpleTeamDto dto
            )
        {
            try
            {
                return Created(Uri, await RetryOnDeadlock(async () =>
                {
                    var team = await SimpleTeam.BuildAsync(Factory, ChildFactory, dto);
                    if (team.IsValid)
                    {
                        team = await team.SaveAsync();
                    }
                    return team.ToDto();
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
        /// Gets the specified team to edit.
        /// </summary>
        /// <param name="teamId">The identifier of the team.</param>
        /// <returns>The requested team.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SimpleTeamDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeam(
            string teamId
            )
        {
            try
            {
                var team = await SimpleTeam.GetAsync(Factory, teamId);
                return Ok(team.ToDto());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the specified team.
        /// </summary>
        /// <param name="dto">The data transer object of the team.</param>
        /// <returns>The updated team.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(SimpleTeamDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTeam(
            [FromBody] SimpleTeamDto dto
            )
        {
            try
            {
                return Ok(await RetryOnDeadlock(async () =>
                {
                    var team = await SimpleTeam.BuildAsync(Factory, ChildFactory, dto);
                    if (team.IsSavable)
                    {
                        team = await team.SaveAsync();
                    }
                    return team.ToDto();
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
        /// Deletes the specified team.
        /// </summary>
        /// <param name="teamId">The identifier of the team.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTeam(
            string teamId
            )
        {
            try
            {
                await RetryOnDeadlock(async () =>
                {
                    await SimpleTeam.DeleteAsync(Factory, teamId);
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
        /// Gets the specified team set to edit.
        /// </summary>
        /// <param name="criteria">The criteria of the team set.</param>
        /// <returns>The requested team set.</returns>
        [HttpGet("set")]
        [ProducesResponseType(typeof(IList<SimpleTeamSetItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamSet(
            [FromQuery] SimpleTeamSetCriteria criteria
            )
        {
            try
            {
                var teams = await SimpleTeamSet.GetAsync(Factory, criteria);
                return Ok(teams.ToDto());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Update-Set

        /// <summary>
        /// Updates the specified team set.
        /// </summary>
        /// <param name="criteria">The criteria of the team set.</param>
        /// <param name="dto">The data transer objects of the team set.</param>
        /// <returns>The updated team set.</returns>
        [HttpPut("set")]
        [ProducesResponseType(typeof(IList<SimpleTeamSetItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTeamSet(
            [FromQuery] SimpleTeamSetCriteria criteria,
            [FromBody] List<SimpleTeamSetItemDto> dto
            )
        {
            try
            {
                return Ok(await RetryOnDeadlock(async () =>
                {
                    var teams = await SimpleTeamSet.BuildAsync(Factory, ChildFactory, criteria, dto);
                    if (teams.IsSavable)
                    {
                        teams = await teams.SaveAsync();
                    }
                    return teams.ToDto();
                }));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Renames the specified team.
        /// </summary>
        /// <param name="dto">The data transer object of the rename team command.</param>
        /// <returns>True when the team was renamed; otherwise false.</returns>
        [HttpPatch]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> RenameTeamCommand(
            [FromBody] RenameTeamDto dto
            )
        {
            try
            {
                return Ok(await RetryOnDeadlock(async () =>
                {
                    var command = await RenameTeam.ExecuteAsync(Factory, dto);
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
