// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Reflection;
using System.Runtime.Serialization;
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

        private NpgsqlException CreatePostgreSqlException(string message, string errorCode)
        {
            NpgsqlException postgreSqlException =
                (NpgsqlException)FormatterServices.GetUninitializedObject(typeof(NpgsqlException));

            FieldInfo messageField = typeof(NpgsqlException).GetField(
                name: "_message",
                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic);

            if (messageField != null)
                messageField.SetValue(postgreSqlException, message);

            FieldInfo errorCodeField = typeof(NpgsqlException).GetField(
                name: "_errorCode",
                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic);

            if (errorCodeField != null)
                errorCodeField.SetValue(postgreSqlException, errorCode);

            return postgreSqlException;
        }
    }
}
