using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Csla8RestApi.Tests.Contracts.Complex.Edit;
using Csla8RestApi.Tests.Entities;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Complex.Edit
{
    /// <summary>
    /// Implements the data access functions of the editable player object.
    /// </summary>
    [DalImplementation]
    public class ProductPartDal : DalBase<RdbmsContext>, IProductPartDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductPartDal(
            RdbmsContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Insert

        /// <summary>
        /// Creates a new player using the specified data.
        /// </summary>
        /// <param name="dao">The data of the player.</param>
        public async Task InsertAsync(
            ProductPartDao dao
            )
        {
            // Check unique part code.
            var part = await DbContext.Parts
                .Where(e =>
                    e.ProductKey == dao.ProductKey &&
                    e.PartCode == dao.PartCode
                )
                .FirstOrDefaultAsync();
            if (part is not null)
                throw new DataExistException(ComplexText.Part_PartCodeExists.With(dao.PartCode!));

            // Create the new part.
            part = new Part
            {
                ProductKey = dao.ProductKey,
                PartCode = dao.PartCode,
                PartName = dao.PartName
            };
            await DbContext.Parts.AddAsync(part);

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new InsertFailedException(ComplexText.Part_InsertFailed.With(part.PartCode!));

            // Return new data.
            dao.PartKey = part.PartKey;
        }

        #endregion Insert

        #region Update

        /// <summary>
        /// Updates an existing player using the specified data.
        /// </summary>
        /// <param name="dao">The data of the player.</param>
        public async Task UpdateAsync(
            ProductPartDao dao
            )
        {
            // Get the specified part.
            var part = await DbContext.Parts
                .Where(e =>
                    e.PartKey == dao.PartKey
                )
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(ComplexText.Part_NotFound);

            // Check unique part code.
            if (part.PartCode != dao.PartCode)
            {
                int exist = await DbContext.Parts
                    .Where(e =>
                        e.ProductKey == dao.ProductKey &&
                        e.PartCode == dao.PartCode &&
                        e.PartKey != part.PartKey
                    )
                    .CountAsync();
                if (exist > 0)
                    throw new DataExistException(ComplexText.Part_PartCodeExists.With(dao.PartCode!));
            }

            // Update the part.
            part.PartCode = dao.PartCode;
            part.PartName = dao.PartName;

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new UpdateFailedException(ComplexText.Part_UpdateFailed.With(part.PartCode!));

            // Return new data.
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the specified player.
        /// </summary>
        /// <param name="criteria">The criteria of the player.</param>
        public async Task DeleteAsync(
            ProductPartCriteria criteria
            )
        {
            int count = 0;

            // Get the specified part.
            var part = await DbContext.Parts
                .Where(e =>
                    e.PartKey == criteria.PartKey
                 )
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(ComplexText.Part_NotFound);

            // Delete the part.
            DbContext.Parts.Remove(part);

            count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new DeleteFailedException(ComplexText.Part_DeleteFailed.With(part.PartCode!));
        }

        #endregion Delete
    }
}
