using Csla8ModelTemplates.Contracts.Junction.Edit;
using Csla8ModelTemplates.Entities;
using Csla8ModelTemplates.Resources;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.MySql.Junction.Edit
{
    /// <summary>
    /// Implements the data access functions of the editable group object.
    /// </summary>
    [DalImplementation]
    public class GroupDal : DalBase<MySqlContext>, IGroupDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public GroupDal(
            MySqlContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Fetch

        /// <summary>
        /// Gets the specified group.
        /// </summary>
        /// <param name="criteria">The criteria of the group.</param>
        /// <returns>The requested group.</returns>
        public async Task<GroupDao> FetchAsync(
            GroupCriteria criteria
            )
        {
            // Get the specified group.
            var group = await DbContext.Groups
                .Where(e =>
                    e.GroupKey == criteria.GroupKey
                 )
                .Select(e => new GroupDao
                {
                    GroupKey = e.GroupKey,
                    GroupCode = e.GroupCode,
                    GroupName = e.GroupName,
                    Persons = e.Persons!.Select(m => new GroupPersonDao
                    {
                        GroupKey = m.GroupKey,
                        PersonKey = m.PersonKey,
                        PersonName = m.Person!.PersonName
                    }).ToList(),
                    Timestamp = e.Timestamp
                })
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(DalText.Group_NotFound);

            return group;
        }

        #endregion Fetch

        #region Insert

        /// <summary>
        /// Creates a new group using the specified data.
        /// </summary>
        /// <param name="dao">The data of the group.</param>
        public async Task InsertAsync(
            GroupDao dao
            )
        {
            // Check unique group code.
            var group = await DbContext.Groups
                .Where(e =>
                    e.GroupCode == dao.GroupCode
                )
                .FirstOrDefaultAsync();
            if (group is not null)
                throw new DataExistException(DalText.Group_GroupCodeExists.With(dao.GroupCode!));

            // Create the new group.
            group = new Group
            {
                GroupCode = dao.GroupCode,
                GroupName = dao.GroupName
            };
            await DbContext.Groups.AddAsync(group);

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new InsertFailedException(DalText.Group_InsertFailed);

            // Return new data.
            dao.GroupKey = group.GroupKey;
            dao.Timestamp = group.Timestamp;
        }

        #endregion Insert

        #region Update

        /// <summary>
        /// Updates an existing group using the specified data.
        /// </summary>
        /// <param name="dao">The data of the group.</param>
        public async Task UpdateAsync(
            GroupDao dao
            )
        {
            // Get the specified group.
            var group = await DbContext.Groups
                .Where(e =>
                    e.GroupKey == dao.GroupKey
                )
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(DalText.Group_NotFound);
            if (group.Timestamp != dao.Timestamp)
                throw new ConcurrencyException(DalText.Group_Concurrency);

            // Check unique group code.
            if (group.GroupCode != dao.GroupCode)
            {
                int exist = await DbContext.Groups
                    .Where(e => e.GroupCode == dao.GroupCode && e.GroupKey != group.GroupKey)
                    .CountAsync();
                if (exist > 0)
                    throw new DataExistException(DalText.Group_GroupCodeExists.With(dao.GroupCode!));
            }

            // Update the group.
            group.GroupCode = dao.GroupCode;
            group.GroupName = dao.GroupName;
            group.Timestamp = DateTime.Now; // Force update timestamp.

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new UpdateFailedException(DalText.Group_UpdateFailed);

            // Return new data.
            dao.Timestamp = group.Timestamp;
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the specified group.
        /// </summary>
        /// <param name="criteria">The criteria of the group.</param>
        public async Task DeleteAsync(
            GroupCriteria criteria
            )
        {
            // Get the specified group.
            var group = await DbContext.Groups
                .Where(e =>
                    e.GroupKey == criteria.GroupKey
                 )
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(DalText.Group_NotFound);

            // Check or delete references
            //int dependents = 0;

            //dependents = await DbContext.Others.CountAsync(e => e.GroupKey == criteria.GroupKey);
            //if (dependents > 0)
            //    throw new DeleteFailedException(DalText.Group_Delete_Others);

            // Delete the group.
            DbContext.Groups.Remove(group);

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new DeleteFailedException(DalText.Group_DeleteFailed);
        }

        #endregion Delete
    }
}
