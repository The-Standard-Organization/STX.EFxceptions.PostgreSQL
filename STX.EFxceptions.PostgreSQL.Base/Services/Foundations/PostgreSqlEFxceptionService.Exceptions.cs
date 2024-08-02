// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using STX.EFxceptions.Abstractions.Models.Exceptions;
using STX.EFxceptions.PostgreSQL.Base.Models.Exceptions;

namespace STX.EFxceptions.PostgreSQL.Base.Services.Foundations
{
    public partial class PostgreSqlEFxceptionService
    {
        public delegate void ReturningExceptionFunction();

        public void TryCatch(ReturningExceptionFunction returningExceptionFunction)
        {
            try
            {
                returningExceptionFunction();
            }
            catch (ForeignKeyConstraintConflictPostgreSqlException foreignKeyConstraintConflictPostgreSqlException)
            {
                throw new ForeignKeyConstraintConflictException(
                    message: foreignKeyConstraintConflictPostgreSqlException.Message,
                    innerException: foreignKeyConstraintConflictPostgreSqlException);
            }
        }
    }
}
