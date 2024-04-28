using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Csla8RestApi.Tests.Contracts.Junction.Edit;
using Csla8RestApi.Tests.Entities;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Junction.Edit
{
    /// <summary>
    /// Implements the data access functions of the editable user object.
    /// </summary>
    [DalImplementation]
    public class UserDal : DalBase<RdbmsContext>, IUserDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UserDal(
            RdbmsContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Fetch

        /// <summary>
        /// Gets the specified user.
        /// </summary>
        /// <param name="criteria">The criteria of the user.</param>
        /// <returns>The requested user.</returns>
        public async Task<UserDao> FetchAsync(
            UserCriteria criteria
            )
        {
            // Get the specified user.
            var user = await DbContext.Users
                .Where(e =>
                    e.UserKey == criteria.UserKey
                 )
                .Select(e => new UserDao
                {
                    UserKey = e.UserKey,
                    UserCode = e.UserCode,
                    UserName = e.UserName,
                    Roles = e.Roles!.Select(m => new UserRoleDao
                    {
                        UserKey = m.UserKey,
                        RoleKey = m.RoleKey,
                        RoleName = m.Role!.RoleName
                    }).ToList(),
                    Timestamp = e.Timestamp
                })
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(JunctionText.User_NotFound);

            return user;
        }

        #endregion Fetch

        #region Insert

        /// <summary>
        /// Creates a new user using the specified data.
        /// </summary>
        /// <param name="dao">The data of the user.</param>
        public async Task InsertAsync(
            UserDao dao
            )
        {
            // Check unique user code.
            var user = await DbContext.Users
                .Where(e =>
                    e.UserCode == dao.UserCode
                )
                .FirstOrDefaultAsync();
            if (user is not null)
                throw new DataExistException(JunctionText.User_UserCodeExists.With(dao.UserCode!));

            // Create the new user.
            user = new User
            {
                UserCode = dao.UserCode,
                UserName = dao.UserName
            };
            await DbContext.Users.AddAsync(user);

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new InsertFailedException(JunctionText.User_InsertFailed);

            // Return new data.
            dao.UserKey = user.UserKey;
            dao.Timestamp = user.Timestamp;
        }

        #endregion Insert

        #region Update

        /// <summary>
        /// Updates an existing user using the specified data.
        /// </summary>
        /// <param name="dao">The data of the user.</param>
        public async Task UpdateAsync(
            UserDao dao
            )
        {
            // Get the specified user.
            var user = await DbContext.Users
                .Where(e =>
                    e.UserKey == dao.UserKey
                )
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(JunctionText.User_NotFound);
            if (user.Timestamp != dao.Timestamp)
                throw new ConcurrencyException(JunctionText.User_Concurrency);

            // Check unique user code.
            if (user.UserCode != dao.UserCode)
            {
                int exist = await DbContext.Users
                    .Where(e => e.UserCode == dao.UserCode && e.UserKey != user.UserKey)
                    .CountAsync();
                if (exist > 0)
                    throw new DataExistException(JunctionText.User_UserCodeExists.With(dao.UserCode!));
            }

            // Update the user.
            user.UserCode = dao.UserCode;
            user.UserName = dao.UserName;
            user.Timestamp = DateTime.Now; // Force update timestamp.

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new UpdateFailedException(JunctionText.User_UpdateFailed);

            // Return new data.
            dao.Timestamp = user.Timestamp;
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="criteria">The criteria of the user.</param>
        public async Task DeleteAsync(
            UserCriteria criteria
            )
        {
            // Get the specified user.
            var user = await DbContext.Users
                .Where(e =>
                    e.UserKey == criteria.UserKey
                 )
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(JunctionText.User_NotFound);

            // Check or delete references
            //int dependents = 0;

            //dependents = await DbContext.Others.CountAsync(e => e.UserKey == criteria.UserKey);
            //if (dependents > 0)
            //    throw new DeleteFailedException(JunctionText.User_Delete_Others);

            // Delete the user.
            DbContext.Users.Remove(user);

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new DeleteFailedException(JunctionText.User_DeleteFailed);
        }

        #endregion Delete
    }
}
