using Csla8ModelTemplates.Contracts.Junction.Edit;
using Csla8ModelTemplates.Contracts.Junction.View;
using Csla8ModelTemplates.Models.Junction.Edit;
using Csla8ModelTemplates.Models.Junction.View;
using Csla8RestApi.Models.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoints for junction models.
    /// </summary>
    [ApiController]
    [Route("api/junction")]
    [Produces("application/json")]
    public class JunctionController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public JunctionController(
            ILogger<JunctionController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region View

        /// <summary>
        /// Gets the specified group details to display.
        /// </summary>
        /// <param name="groupId">The identifier of the group.</param>
        /// <returns>The requested group view.</returns>
        [HttpGet("{groupId}/view")]
        [ProducesResponseType(typeof(GroupViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroupView(
            string groupId
            )
        {
            try
            {
                GroupView group = await GroupView.GetAsync(Factory, groupId);
                return Ok(group.ToDto<GroupViewDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region New

        /// <summary>
        /// Gets a new group to edit.
        /// </summary>
        /// <returns>A new group.</returns>
        [HttpGet("new")]
        [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNewGroup()
        {
            try
            {
                Group group = await Group.NewAsync(Factory);
                return Ok(group.ToDto());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Create

        /// <summary>
        /// Creates a new group.
        /// </summary>
        /// <param name="dto">The data transer object of the group.</param>
        /// <returns>The created group.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(GroupDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateGroup(
            [FromBody] GroupDto dto
            )
        {
            try
            {
                return Created(Uri, await RetryOnDeadlock(async () =>
                {
                    Group group = await Group.BuildAsync(Factory, ChildFactory, dto);
                    if (group.IsValid)
                    {
                        group = await group.SaveAsync();
                    }
                    return group.ToDto();
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
        /// Gets the specified group to edit.
        /// </summary>
        /// <param name="groupId">The identifier of the group.</param>
        /// <returns>The requested group.</returns>
        [HttpGet("{groupId}")]
        [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroup(
            string groupId
            )
        {
            try
            {
                Group group = await Group.GetAsync(Factory, groupId);
                return Ok(group.ToDto());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the specified group.
        /// </summary>
        /// <param name="dto">The data transer object of the group.</param>
        /// <returns>The updated group.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateGroup(
            [FromBody] GroupDto dto
            )
        {
            try
            {
                return Ok(await RetryOnDeadlock(async () =>
                {
                    Group group = await Group.BuildAsync(Factory, ChildFactory, dto);
                    if (group.IsSavable)
                    {
                        group = await group.SaveAsync();
                    }
                    return group.ToDto();
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
        /// Deletes the specified group.
        /// </summary>
        /// <param name="groupId">The identifier of the group.</param>
        [HttpDelete("{groupId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteGroup(
            string groupId
            )
        {
            try
            {
                await RetryOnDeadlock(async () =>
                {
                    await Group.DeleteAsync(Factory, groupId);
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
