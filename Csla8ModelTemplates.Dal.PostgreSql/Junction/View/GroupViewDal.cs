using Csla8ModelTemplates.Contracts.Junction.View;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.PostgreSql.Junction.View
{
    /// <summary>
    /// Implements the data access functions of the read-only group object.
    /// </summary>
    [DalImplementation]
    public class GroupViewDal : DalBase<PostgreSqlContext>, IGroupViewDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public GroupViewDal(
            PostgreSqlContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Fetch

        /// <summary>
        /// Gets the specified group view.
        /// </summary>
        /// <param name="criteria">The criteria of the group.</param>
        /// <returns>The requested group view.</returns>
        public async Task<GroupViewDao> FetchAsync(
            GroupViewCriteria criteria
            )
        {
            // Get the specified group.
            var group = await DbContext.Groups
                .Include(e => e.Persons)
                .Where(e =>
                    e.GroupKey == criteria.GroupKey
                 )
                .Select(e => new GroupViewDao
                {
                    GroupKey = e.GroupKey,
                    GroupCode = e.GroupCode,
                    GroupName = e.GroupName,
                    Persons = e.Persons!
                        .Select(m => new GroupViewPersonDao
                        {
                            PersonKey = m.PersonKey,
                            PersonName = m.Person!.PersonName
                        })
                        .ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(JunctionText.Group_NotFound);

            return group;
        }

        #endregion Fetch
    }
}
