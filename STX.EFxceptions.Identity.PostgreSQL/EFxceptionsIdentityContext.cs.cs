// ----------------------------------------------------------------------------------
// Copyright(c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------


using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using STX.EFxceptions.Abstractions.Brokers.DbErrorBroker;
using STX.EFxceptions.Abstractions.Services.EFxceptions;
using STX.EFxceptions.Identity.Core;
using STX.EFxceptions.PostgreSQL.Base.Brokers.DbErrorBroker;
using STX.EFxceptions.PostgreSQL.Base.Services.Foundations;

namespace STX.EFxceptions.Identity.PostgreSQL
{
    public abstract class EFxceptionsIdentityContext<TUser, TRole, TKey>
         : IdentityDbContextBase<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>,
             IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>, NpgsqlException, string>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        public EFxceptionsIdentityContext(DbContextOptions options) : base(options)
        { }

        protected EFxceptionsIdentityContext() : base()
        { }

        protected override IDbErrorBroker<NpgsqlException, string> CreateErrorBroker() =>
            new PostgreSqlErrorBroker();

        protected override IEFxceptionService CreateEFxceptionService(
            IDbErrorBroker<NpgsqlException, string> errorBroker)
        {
            return new PostgreSqlEFxceptionService(errorBroker);
        }
    }

    public abstract class EFxceptionsIdentityContext<
        TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
        : IdentityDbContextBase<TUser, TRole, TKey,
            TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken, NpgsqlException, string>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TUserToken : IdentityUserToken<TKey>
    {
        public EFxceptionsIdentityContext(DbContextOptions options) : base(options)
        { }

        protected EFxceptionsIdentityContext() : base()
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