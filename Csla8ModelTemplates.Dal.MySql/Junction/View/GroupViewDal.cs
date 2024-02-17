using Csla8ModelTemplates.Contracts.Junction.View;
using Csla8ModelTemplates.Resources;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.MySql.Junction.View
{
    /// <summary>
    /// Implements the data access functions of the read-only group object.
    /// </summary>
    [DalImplementation]
    public class GroupViewDal : DalBase<MySqlContext>, IGroupViewDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public GroupViewDal(
            MySqlContext dbContext
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
        public GroupViewDao Fetch(
            GroupViewCriteria criteria
            )
        {
            // Get the specified group.
            var group = DbContext.Groups
                .Include(e => e.Persons)
                .Where(e =>
                    e.GroupKey == criteria.GroupKey
                 )
                .Select(e => new GroupViewDao
                {
                    GroupKey = e.GroupKey,
                    GroupCode = e.GroupCode,
                    GroupName = e.GroupName,
                    Persons = e.Persons
                        .Select(m => new GroupViewPersonDao
                        {
                            PersonKey = m.PersonKey,
                            PersonName = m.Person.PersonName
                        })
                        .ToList()
                })
                .AsNoTracking()
                .FirstOrDefault()
                ?? throw new DataNotFoundException(DalText.Group_NotFound);

            return group;
        }

        #endregion Fetch
    }
}
