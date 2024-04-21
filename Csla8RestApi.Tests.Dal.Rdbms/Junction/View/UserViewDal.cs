using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Csla8RestApi.Tests.Contracts.Junction.View;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Junction.View
{
    /// <summary>
    /// Implements the data access functions of the read-only user object.
    /// </summary>
    [DalImplementation]
    public class UserViewDal : DalBase<RdbmsContext>, IUserViewDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UserViewDal(
            RdbmsContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Fetch

        /// <summary>
        /// Gets the specified user view.
        /// </summary>
        /// <param name="criteria">The criteria of the user.</param>
        /// <returns>The requested user view.</returns>
        public async Task<UserViewDao> FetchAsync(
            UserViewCriteria criteria
            )
        {
            // Get the specified user.
            var user = await DbContext.Users
                .Include(e => e.Roles)
                .Where(e =>
                    e.UserKey == criteria.UserKey
                 )
                .Select(e => new UserViewDao
                {
                    UserKey = e.UserKey,
                    UserCode = e.UserCode,
                    UserName = e.UserName,
                    Roles = e.Roles!
                        .Select(m => new UserViewRoleDao
                        {
                            RoleKey = m.RoleKey,
                            RoleName = m.Role!.RoleName
                        })
                        .ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(JunctionText.User_NotFound);

            return user;
        }

        #endregion Fetch
    }
}
