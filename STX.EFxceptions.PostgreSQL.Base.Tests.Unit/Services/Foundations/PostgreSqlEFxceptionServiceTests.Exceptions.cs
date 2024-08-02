// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Npgsql;

namespace STX.EFxceptions.PostgreSQL.Base.Tests.Unit.Services.Foundations
{
    public partial class PostgreSqlEFxceptionServiceTests
    {
        [Fact]
        public void ShouldThrowDbUpdateExceptionIfErrorCodeIsNotRecognized()
        {
            // given
            int sqlForeignKeyConstraintConflictErrorCode = 0000;
            string randomErrorMessage = CreateRandomErrorMessage();

            NpgsqlException foreignKeyConstraintConflictExceptionThrown =
                CreatePostgreSqlException(
                    message: randomErrorMessage,
                    errorCode: sqlForeignKeyConstraintConflictErrorCode);

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: foreignKeyConstraintConflictExceptionThrown);

            DbUpdateException expectedDbUpdateException = dbUpdateException;

            this.postgreSqlErrorBrokerMock.Setup(broker =>
                broker.GetErrorCode(foreignKeyConstraintConflictExceptionThrown))
                    .Returns(sqlForeignKeyConstraintConflictErrorCode);

            // when
            DbUpdateException actualDbUpdateException =
                Assert.Throws<DbUpdateException>(() => this.postgreSqlEFxceptionService
                    .ThrowMeaningfulException(dbUpdateException));

            // then
            actualDbUpdateException.Should()
                .BeEquivalentTo(
                expectation: expectedDbUpdateException,
                config: options => options
                    .Excluding(ex => ex.TargetSite)
                    .Excluding(ex => ex.StackTrace)
                    .Excluding(ex => ex.Source)
                    .Excluding(ex => ex.InnerException.TargetSite)
                    .Excluding(ex => ex.InnerException.StackTrace)
                    .Excluding(ex => ex.InnerException.Source));

            this.postgreSqlErrorBrokerMock.Verify(broker => broker
                .GetErrorCode(foreignKeyConstraintConflictExceptionThrown), Times.Once());

            postgreSqlErrorBrokerMock.VerifyNoOtherCalls();
        }
    }
}
