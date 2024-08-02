// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

namespace STX.EFxceptions.PostgreSQL.Base.Models.Exceptions
{
    public class ForeignKeyConstraintConflictPostgreSqlException : DbUpdateException
    {
        public ForeignKeyConstraintConflictPostgreSqlException(string message) : base(message) { }
    }
}
