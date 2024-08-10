using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Csla8RestApi.Tests.Contracts.Junction.Edit;
using Csla8RestApi.Tests.Entities;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Junction.Edit
{
    /// <summary>
    /// Implements the data access functions of the editable user-role object.
    /// </summary>
    [DalImplementation]
    public class UserRoleDal : DalBase<RdbmsContext>, IUserRoleDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UserRoleDal(
            RdbmsContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Insert

        /// <summary>
        /// Creates a new user-role using the specified data.
        /// </summary>
        /// <param name="dao">The data of the user-role.</param>
        public async Task InsertAsync(
            UserRoleDao dao
            )
        {
            // Check unique user-role.
            var userRole = await DbContext.UserRoles
                .Where(e =>
                    e.UserKey == dao.UserKey &&
                    e.RoleKey == dao.RoleKey
                )
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (userRole is not null)
                throw new DataExistException(JunctionText.UserRole_Exists.With(dao.RoleName!));

            // Create the new user-role.
            Role role = await DbContext.Roles.FindAsync(dao.RoleKey)
                ?? throw new DataExistException(JunctionText.UserRole_NotFound.With(dao.RoleName!));
            userRole = new UserRole
            {
                UserKey = dao.UserKey,
                RoleKey = dao.RoleKey
            };
            await DbContext.UserRoles.AddAsync(userRole);

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new InsertFailedException(JunctionText.UserRole_InsertFailed.With(dao.RoleName!));

            // Return new data.
            dao.RoleName = role.RoleName;
        }

        #endregion Insert

        #region Delete

        /// <summary>
        /// Deletes the specified user-role.
        /// </summary>
        /// <param name="criteria">The criteria of the user-role.</param>
        public async Task DeleteAsync(
            UserRoleDao dao
            )
        {
            // Get the specified user-role.
            UserRole userRole = await DbContext.UserRoles
                .Where(e =>
                    e.UserKey == dao.UserKey &&
                    e.RoleKey == dao.RoleKey
                    )
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(JunctionText.UserRole_NotFound.With(dao.RoleName!));

            // Delete the user-role.
            DbContext.UserRoles.Remove(userRole);

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new DeleteFailedException(JunctionText.UserRole_DeleteFailed.With(dao.RoleName!));
        }

        #endregion Delete
    }
}
