using Csla8ModelTemplates.Contracts.Junction.Edit;
using Csla8ModelTemplates.Entities;
using Csla8ModelTemplates.Resources;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.MySql.Junction.Edit
{
    /// <summary>
    /// Implements the data access functions of the editable group-person object.
    /// </summary>
    [DalImplementation]
    public class GroupPersonDal : DalBase<MySqlContext>, IGroupPersonDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public GroupPersonDal(
            MySqlContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Insert

        /// <summary>
        /// Creates a new group-person using the specified data.
        /// </summary>
        /// <param name="dao">The data of the group-person.</param>
        public async Task InsertAsync(
            GroupPersonDao dao
            )
        {
            // Check unique group-person.
            var groupPerson = await DbContext.GroupPersons
                .Where(e =>
                    e.GroupKey == dao.GroupKey &&
                    e.PersonKey == dao.PersonKey
                )
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (groupPerson is not null)
                throw new DataExistException(DalText.GroupPerson_Exists.With(dao.PersonName!));

            // Create the new group-person.
            Person person = await DbContext.Persons.FindAsync(dao.PersonKey)
                ?? throw new DataExistException(DalText.GroupPerson_NotFound.With(dao.PersonName!));
            groupPerson = new GroupPerson
            {
                GroupKey = dao.GroupKey,
                PersonKey = dao.PersonKey
            };
            await DbContext.GroupPersons.AddAsync(groupPerson);

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new InsertFailedException(DalText.GroupPerson_InsertFailed.With(dao.PersonName!));

            // Return new data.
            dao.PersonName = person.PersonName;
        }

        #endregion Insert

        #region Delete

        /// <summary>
        /// Deletes the specified group-person.
        /// </summary>
        /// <param name="criteria">The criteria of the group-person.</param>
        public async Task DeleteAsync(
            GroupPersonDao dao
            )
        {
            // Get the specified group-person.
            GroupPerson groupPerson = await DbContext.GroupPersons
                .Where(e =>
                    e.GroupKey == dao.GroupKey &&
                    e.PersonKey == dao.PersonKey
                    )
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(DalText.GroupPerson_NotFound.With(dao.PersonName!));

            // Delete the group-person.
            DbContext.GroupPersons.Remove(groupPerson);

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new DeleteFailedException(DalText.GroupPerson_DeleteFailed.With(dao.PersonName!));
        }

        #endregion Delete
    }
}
