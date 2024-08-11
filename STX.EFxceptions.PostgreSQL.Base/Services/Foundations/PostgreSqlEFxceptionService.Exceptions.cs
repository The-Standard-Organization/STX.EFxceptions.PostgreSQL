// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

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
            catch (InvalidColumnNamePostgreSqlException invalidColumnNamePostgreSqlException)
            {
                throw new InvalidColumnNameException(
                    message: invalidColumnNamePostgreSqlException.Message,
                    innerException: invalidColumnNamePostgreSqlException);
            }
            catch (InvalidObjectNamePostgreSqlException invalidObjectNamePostgreSqlException)
            {
                throw new InvalidObjectNameException(
                    message: invalidObjectNamePostgreSqlException.Message,
                    innerException: invalidObjectNamePostgreSqlException);
            }
        }

        private void ConvertAndThrowMeaningfulException(string sqlErrorCode, string message)
        {
            switch (sqlErrorCode)
            {
                case "42703":
                    throw new InvalidColumnNamePostgreSqlException(message);

                case "42P01":
                    throw new InvalidObjectNamePostgreSqlException(message);

                case "23503":
                    throw new ForeignKeyConstraintConflictPostgreSqlException(message);
            }
        }
    }
}
