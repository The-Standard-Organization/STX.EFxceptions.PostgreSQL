﻿// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using STX.EFxceptions.Abstractions.Brokers.DbErrorBroker;

namespace STX.EFxceptions.PostgreSQL.Base.Services.Foundations
{
    public partial class PostgreSqlEFxceptionService : IPostgreSqlEFxceptionService
    {
        private readonly IDbErrorBroker<NpgsqlException, string> postgreSqlErrorBroker;

        public PostgreSqlEFxceptionService(IDbErrorBroker<NpgsqlException, string> postgreSqlErrorBroker) =>
            this.postgreSqlErrorBroker = postgreSqlErrorBroker;

        public void ThrowMeaningfulException(DbUpdateException dbUpdateException) =>
        TryCatch(() =>
        {
            ValidateInnerException(dbUpdateException);

            NpgsqlException postgreSqlException = GetPostgreSqlException(dbUpdateException.InnerException);
            string postgreSqlErrorCode = this.postgreSqlErrorBroker.GetErrorCode(postgreSqlException);

            ConvertAndThrowMeaningfulException(postgreSqlErrorCode, postgreSqlException.Message);

            throw dbUpdateException;
        });

        private NpgsqlException GetPostgreSqlException(Exception exception) =>
             (NpgsqlException)exception;

    }
}
