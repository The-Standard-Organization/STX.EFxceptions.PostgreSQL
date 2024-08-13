using Microsoft.EntityFrameworkCore;

namespace STX.EFxceptions.PostgreSQL.Base.Models.Exceptions
{
    public class DuplicateKeyWithUniqueIndexPostgreSqlException : DbUpdateException
    {
        public DuplicateKeyWithUniqueIndexPostgreSqlException(string message) : base(message) { }
    }
}
