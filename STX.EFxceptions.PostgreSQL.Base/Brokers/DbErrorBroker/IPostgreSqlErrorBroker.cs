// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Npgsql;
using STX.EFxceptions.Abstractions.Brokers.DbErrorBroker;

namespace STX.EFxceptions.PostgreSQL.Base.Brokers.DbErrorBroker
{
    public interface IPostgreSqlErrorBroker : IDbErrorBroker<NpgsqlException>
    { }
}
