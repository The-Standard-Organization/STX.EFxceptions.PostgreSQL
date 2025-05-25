
// ----------------------------------------------------------------------------------
// Copyright(c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Npgsql;
using STX.EFxceptions.Abstractions.Brokers.DbErrorBroker;
using STX.EFxceptions.Abstractions.Services.EFxceptions;
using STX.EFxceptions.Core;
using STX.EFxceptions.PostgreSQL.Base.Brokers.DbErrorBroker;
using STX.EFxceptions.PostgreSQL.Base.Services.Foundations;

namespace STX.EFxceptions.PostgreSQL
{
    public abstract class EFxceptionsContext : DbContextBase<NpgsqlException, string>
    {
        public EFxceptionsContext(DbContextOptions<EFxceptionsContext> options)
            : base(options)
        { }

        protected EFxceptionsContext()
            : base()
        { }

        protected override IDbErrorBroker<NpgsqlException, string> CreateErrorBroker() =>
            new PostgreSqlErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<NpgsqlException, string> errorBroker)
        {
            return new PostgreSqlEFxceptionService(errorBroker);
        }
    }
}
