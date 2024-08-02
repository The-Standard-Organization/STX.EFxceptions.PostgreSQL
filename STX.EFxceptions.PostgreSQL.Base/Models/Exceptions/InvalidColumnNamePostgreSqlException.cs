// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

namespace STX.EFxceptions.PostgreSQL.Base.Models.Exceptions
{
    public class InvalidColumnNamePostgreSqlException : DbUpdateException
    {
        public InvalidColumnNamePostgreSqlException(string message) : base(message) { }
    }
}
