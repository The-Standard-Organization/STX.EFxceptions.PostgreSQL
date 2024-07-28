// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Npgsql;

namespace STX.EFxceptions.PostgreSQL.Base.Brokers.DbErrorBroker
{
    public class PostgreSqlErrorBroker : IPostgreSqlErrorBroker
    {
        public int GetErrorCode(NpgsqlException postgreSqlException) =>
             postgreSqlException.ErrorCode;
    }
}
