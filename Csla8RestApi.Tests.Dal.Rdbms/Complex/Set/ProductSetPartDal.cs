using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Csla8RestApi.Tests.Contracts.Complex.Set;
using Csla8RestApi.Tests.Entities;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Complex.Set
{
    /// <summary>
    /// Implements the data access functions of the editable part object.
    /// </summary>
    [DalImplementation]
    public class ProductSetPartDal : DalBase<RdbmsContext>, IProductSetPartDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductSetPartDal(
            RdbmsContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Insert

        /// <summary>
        /// Creates a new part using the specified data.
        /// </summary>
        /// <param name="dao">The data of the part.</param>
        public async Task InsertAsync(
            ProductSetPartDao dao
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
                throw new DataExistException(ComplexText.ProductSetPart_PartCodeExists
                    .With(dao.__productCode!, dao.PartCode!));

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
                throw new InsertFailedException(ComplexText.ProductSetPart_InsertFailed
                    .With(dao.__productCode!, dao.PartCode!));

            // Return new data.
            dao.PartKey = part.PartKey;
        }

        #endregion Insert

        #region Update

        /// <summary>
        /// Updates an existing part using the specified data.
        /// </summary>
        /// <param name="dao">The data of the part.</param>
        public async Task UpdateAsync(
            ProductSetPartDao dao
            )
        {
            // Get the specified part.
            var part = await DbContext.Parts
                .Where(e =>
                    e.PartKey == dao.PartKey
                )
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(ComplexText.ProductSetPart_NotFound
                    .With(dao.__productCode!, dao.PartCode!));

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
                    throw new DataExistException(ComplexText.ProductSetPart_PartCodeExists
                        .With(dao.__productCode!, dao.PartCode!));
            }

            // Update the part.
            part.PartCode = dao.PartCode;
            part.PartName = dao.PartName;

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new UpdateFailedException(ComplexText.ProductSetPart_UpdateFailed
                    .With(dao.__productCode!, dao.PartCode!));

            // Return new data.
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the specified part.
        /// </summary>
        /// <param name="criteria">The criteria of the part.</param>
        public async Task DeleteAsync(
            ProductSetPartCriteria criteria
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
                ?? throw new DataNotFoundException(ComplexText.ProductSetPart_NotFound
                    .With(criteria.__productCode!, criteria.__partCode!));

            // Delete the part.
            DbContext.Parts.Remove(part);

            count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new DeleteFailedException(ComplexText.ProductSetPart_DeleteFailed
                    .With(criteria.__productCode!, criteria.__partCode!));
        }

        #endregion Delete
    }
}
