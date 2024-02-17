using Csla8ModelTemplates.Contracts.Arrangement.Full;
using Csla8ModelTemplates.Contracts.Arrangement.Pagination;
using Csla8ModelTemplates.Contracts.Arrangement.Sorting;
using Csla8ModelTemplates.Models.Arrangement.Full;
using Csla8ModelTemplates.Models.Arrangement.Pagination;
using Csla8ModelTemplates.Models.Arrangement.Sorting;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Csla8ModelTemplates.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoints for sorting and pagination.
    /// </summary>
    [ApiController]
    [Route("api/arrangement")]
    [Produces("application/json")]
    public class ArrangementController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        /// <param name="csla">The CSLA helper service.</param>
        public ArrangementController(
            ILogger<ArrangementController> logger,
            ICslaService csla
            ) : base(logger, csla)
        { }

        #endregion

        #region Sorting

        /// <summary>
        /// Gets the specified teams sorted.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>The requested team list.</returns>
        [HttpGet("sorted")]
        [ProducesResponseType(typeof(List<SortedTeamListItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSortedTeamList(
            [FromQuery] SortedTeamListCriteria criteria
            )
        {
            try
            {
                SortedTeamList list = await SortedTeamList.Get(Factory, criteria);
                return Ok(list.ToDto<SortedTeamListItemDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Pagination

        /// <summary>
        /// Gets the specified page of teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>The requested page of the team list.</returns>
        [HttpGet("paginated")]
        [ProducesResponseType(typeof(PaginatedList<PaginatedTeamListItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaginatedTeamList(
            [FromQuery] PaginatedTeamListCriteria criteria
            )
        {
            try
            {
                var list = await PaginatedTeamList.Get(Factory, criteria);
                return Ok(list.ToDto<PaginatedList<PaginatedTeamListItemDto>>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Arranged

        /// <summary>
        /// Gets the specified page of sorted teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>The requested page of the sorted team list.</returns>
        [HttpGet("arranged")]
        [ProducesResponseType(typeof(PaginatedList<ArrangedTeamListItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetArrangedTeamList(
            [FromQuery] ArrangedTeamListCriteria criteria
            )
        {
            try
            {
                var list = await ArrangedTeamList.Get(Factory, criteria);
                return Ok(list.ToDto<PaginatedList<ArrangedTeamListItemDto>>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
