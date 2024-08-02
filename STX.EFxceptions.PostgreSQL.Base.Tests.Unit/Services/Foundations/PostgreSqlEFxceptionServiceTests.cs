// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Reflection;
using Moq;
using Npgsql;
using STX.EFxceptions.PostgreSQL.Base.Brokers.DbErrorBroker;
using STX.EFxceptions.PostgreSQL.Base.Services.Foundations;
using Tynamix.ObjectFiller;

namespace STX.EFxceptions.PostgreSQL.Base.Tests.Unit.Services.Foundations
{
    public partial class PostgreSqlEFxceptionServiceTests
    {
        private readonly Mock<IPostgreSqlErrorBroker> postgreSqlErrorBrokerMock;
        private readonly IPostgreSqlEFxceptionService postgreSqlEFxceptionService;

        public PostgreSqlEFxceptionServiceTests()
        {
            this.postgreSqlErrorBrokerMock = new Mock<IPostgreSqlErrorBroker>();

            this.postgreSqlEFxceptionService = new PostgreSqlEFxceptionService(
                 postgreSqlErrorBroker: this.postgreSqlErrorBrokerMock.Object);
        }

        private string CreateRandomErrorMessage() => new MnemonicString().GetValue();

        private NpgsqlException CreatePostgreSqlException(string message, int errorCode)
        {
            ConstructorInfo constructorInfo = typeof(NpgsqlException)
                .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null,
                    new[] { typeof(string), typeof(int) }, null);

            var exception = (NpgsqlException)constructorInfo.Invoke(new object[] { message, errorCode });

            return exception;
        }
    }
}
