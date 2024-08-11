// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Npgsql;
using STX.EFxceptions.Abstractions.Models.Exceptions;
using STX.EFxceptions.PostgreSQL.Base.Models.Exceptions;

namespace STX.EFxceptions.PostgreSQL.Base.Tests.Unit.Services.Foundations
{
    public partial class PostgreSqlEFxceptionServiceTests
    {
        [Fact]
        public void ShouldThrowDbUpdateExceptionIfErrorCodeIsNotRecognized()
        {
            // given
            string sqlForeignKeyConstraintConflictErrorCode = "0000";
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

        [Fact]
        public void ShouldThrowInvalidColumnNamePostgreSqlException()
        {
            // given
            string postgreSqlInvalidColumnNameErrorCode = "42703";
            string randomErrorMessage = CreateRandomErrorMessage();

            NpgsqlException invalidColumnNameExceptionThrown =
                CreatePostgreSqlException(
                    message: randomErrorMessage,
                    errorCode: postgreSqlInvalidColumnNameErrorCode);

            string randomDbUpdateExceptionMessage = CreateRandomErrorMessage();

            var dbUpdateException = new DbUpdateException(
                message: randomDbUpdateExceptionMessage,
                innerException: invalidColumnNameExceptionThrown);

            var invalidColumnNamePostgreSqlException =
                new InvalidColumnNamePostgreSqlException(
                    message: invalidColumnNameExceptionThrown.Message);

            var expectedInvalidColumnNameException =
                new InvalidColumnNameException(
                    message: invalidColumnNamePostgreSqlException.Message,
                    innerException: invalidColumnNamePostgreSqlException);

            this.postgreSqlErrorBrokerMock.Setup(broker =>
                broker.GetErrorCode(invalidColumnNameExceptionThrown))
                    .Returns(postgreSqlInvalidColumnNameErrorCode);

            // when 
            InvalidColumnNameException actualInvalidColumnNameException =
                Assert.Throws<InvalidColumnNameException>(() =>
                    this.postgreSqlEFxceptionService
                        .ThrowMeaningfulException(dbUpdateException));

            // then
            actualInvalidColumnNameException.Should()
                .BeEquivalentTo(
                 expectation: expectedInvalidColumnNameException,
                 config: options => options
                     .Excluding(ex => ex.TargetSite)
                     .Excluding(ex => ex.StackTrace)
                     .Excluding(ex => ex.Source)
                     .Excluding(ex => ex.InnerException.TargetSite)
                     .Excluding(ex => ex.InnerException.StackTrace)
                     .Excluding(ex => ex.InnerException.Source));

            this.postgreSqlErrorBrokerMock.Verify(broker => broker
                .GetErrorCode(invalidColumnNameExceptionThrown),
                    Times.Once());

            this.postgreSqlErrorBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowInvalidObjectNamePostgreSqlException()
        {
            // given
            string postgreSqlInvalidObjectNameErrorCode = "42P01";
            string randomErrorMessage = CreateRandomErrorMessage();

            NpgsqlException invalidObjectNameExceptionThrown =
                CreatePostgreSqlException(
                    message: randomErrorMessage,
                    errorCode: postgreSqlInvalidObjectNameErrorCode);

            string randomDbUpdateExceptionMessage = CreateRandomErrorMessage();

            var dbUpdateException = new DbUpdateException(
                message: randomDbUpdateExceptionMessage,
                innerException: invalidObjectNameExceptionThrown);

            var invalidObjectNameSqlException =
                new InvalidObjectNamePostgreSqlException(
                    message: invalidObjectNameExceptionThrown.Message);

            var expectedInvalidObjectNameException =
                new InvalidObjectNameException(
                    message: invalidObjectNameSqlException.Message,
                    innerException: invalidObjectNameSqlException);

            this.postgreSqlErrorBrokerMock.Setup(broker =>
                broker.GetErrorCode(invalidObjectNameExceptionThrown))
                    .Returns(postgreSqlInvalidObjectNameErrorCode);

            // when
            InvalidObjectNameException actualInvalidObjectNameException =
                Assert.Throws<InvalidObjectNameException>(() =>
                    this.postgreSqlEFxceptionService
                        .ThrowMeaningfulException(dbUpdateException));

            // then
            actualInvalidObjectNameException.Should()
                .BeEquivalentTo(
                expectation: expectedInvalidObjectNameException,
                config: options => options
                    .Excluding(ex => ex.TargetSite)
                    .Excluding(ex => ex.StackTrace)
                    .Excluding(ex => ex.Source)
                    .Excluding(ex => ex.InnerException.TargetSite)
                    .Excluding(ex => ex.InnerException.StackTrace)
                    .Excluding(ex => ex.InnerException.Source));

            this.postgreSqlErrorBrokerMock.Verify(broker => broker
                .GetErrorCode(invalidObjectNameExceptionThrown),
                    Times.Once());

            this.postgreSqlErrorBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowForeignKeyConstraintConflictPostgreSqlException()
        {
            // given
            string postgreSqlForeignKeyConstraintConflictErrorCode = "23503";
            string randomErrorMessage = CreateRandomErrorMessage();

            NpgsqlException foreignKeyConstraintConflictExceptionThrown =
                CreatePostgreSqlException(
                    message: randomErrorMessage,
                    errorCode: postgreSqlForeignKeyConstraintConflictErrorCode);

            string randomDbUpdateExceptionMessage = CreateRandomErrorMessage();

            var dbUpdateException = new DbUpdateException(
                message: randomDbUpdateExceptionMessage,
                innerException: foreignKeyConstraintConflictExceptionThrown);

            var foreignKeyConstraintConflictSqlException =
                new ForeignKeyConstraintConflictPostgreSqlException(
                    message: foreignKeyConstraintConflictExceptionThrown.Message);

            var expectedForeignKeyConstraintConflictException =
                new ForeignKeyConstraintConflictException(
                    message: foreignKeyConstraintConflictSqlException.Message,
                    innerException: foreignKeyConstraintConflictSqlException);

            this.postgreSqlErrorBrokerMock.Setup(broker =>
                broker.GetErrorCode(foreignKeyConstraintConflictExceptionThrown))
                    .Returns(postgreSqlForeignKeyConstraintConflictErrorCode);

            // when
            ForeignKeyConstraintConflictException actualForeignKeyConstraintConflictException =
                Assert.Throws<ForeignKeyConstraintConflictException>(() =>
                    this.postgreSqlEFxceptionService
                        .ThrowMeaningfulException(dbUpdateException));

            // then
            actualForeignKeyConstraintConflictException.Should()
                .BeEquivalentTo(
                expectation: expectedForeignKeyConstraintConflictException,
                config: options => options
                    .Excluding(ex => ex.TargetSite)
                    .Excluding(ex => ex.StackTrace)
                    .Excluding(ex => ex.Source)
                    .Excluding(ex => ex.InnerException.TargetSite)
                    .Excluding(ex => ex.InnerException.StackTrace)
                    .Excluding(ex => ex.InnerException.Source));

            this.postgreSqlErrorBrokerMock.Verify(broker => broker
                .GetErrorCode(foreignKeyConstraintConflictExceptionThrown),
                    Times.Once());

            this.postgreSqlErrorBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowDuplicateKeyWithUniqueIndexPostgreSqlException()
        {
            // given
            string postgreSqlDuplicateKeyWithUniqueIndexErrorCode = "42710";
            string randomErrorMessage = CreateRandomErrorMessage();

            NpgsqlException duplicateKeyWithUniqueIndexExceptionThrown =
                CreatePostgreSqlException(
                    message: randomErrorMessage,
                    errorCode: postgreSqlDuplicateKeyWithUniqueIndexErrorCode);

            string randomDbUpdateExceptionMessage = CreateRandomErrorMessage();

            var dbUpdateException = new DbUpdateException(
                message: randomDbUpdateExceptionMessage,
                innerException: duplicateKeyWithUniqueIndexExceptionThrown);

            var duplicateKeyWithUniqueIndexSqlException =
                new DuplicateKeyWithUniqueIndexPostgreSqlException(
                    message: duplicateKeyWithUniqueIndexExceptionThrown.Message);

            var expectedDuplicateKeyWithUniqueIndexException =
                new DuplicateKeyWithUniqueIndexException(
                    message: duplicateKeyWithUniqueIndexSqlException.Message,
                    innerException: duplicateKeyWithUniqueIndexSqlException);

            this.postgreSqlErrorBrokerMock.Setup(broker =>
                broker.GetErrorCode(duplicateKeyWithUniqueIndexExceptionThrown))
                    .Returns(postgreSqlDuplicateKeyWithUniqueIndexErrorCode);

            // when
            DuplicateKeyWithUniqueIndexException actualDuplicateKeyWithUniqueIndexException =
                Assert.Throws<DuplicateKeyWithUniqueIndexException>(() =>
                    this.postgreSqlEFxceptionService
                        .ThrowMeaningfulException(dbUpdateException));

            // then
            actualDuplicateKeyWithUniqueIndexException.Should()
                .BeEquivalentTo(
                expectation: expectedDuplicateKeyWithUniqueIndexException,
                config: options => options
                    .Excluding(ex => ex.TargetSite)
                    .Excluding(ex => ex.StackTrace)
                    .Excluding(ex => ex.Source)
                    .Excluding(ex => ex.InnerException.TargetSite)
                    .Excluding(ex => ex.InnerException.StackTrace)
                    .Excluding(ex => ex.InnerException.Source));

            this.postgreSqlErrorBrokerMock.Verify(broker => broker
                .GetErrorCode(duplicateKeyWithUniqueIndexExceptionThrown),
                    Times.Once());

            this.postgreSqlErrorBrokerMock.VerifyNoOtherCalls();
        }
    }
}
