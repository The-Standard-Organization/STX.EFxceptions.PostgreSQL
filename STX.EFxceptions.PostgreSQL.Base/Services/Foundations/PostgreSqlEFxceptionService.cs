// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Npgsql;
using STX.EFxceptions.Abstractions.Brokers.DbErrorBroker;

namespace STX.EFxceptions.PostgreSQL.Base.Services.Foundations
{
    public partial class PostgreSqlEFxceptionService : IPostgreSqlEFxceptionService
    {
        private readonly IDbErrorBroker<NpgsqlException> postgreSqlErrorBroker;

        public PostgreSqlEFxceptionService(IDbErrorBroker<NpgsqlException> postgreSqlErrorBroker) =>
            this.postgreSqlErrorBroker = postgreSqlErrorBroker;

        public void ThrowMeaningfulException(DbUpdateException dbUpdateException)
        {
            ValidateInnerException(dbUpdateException);
        }
    }
}
