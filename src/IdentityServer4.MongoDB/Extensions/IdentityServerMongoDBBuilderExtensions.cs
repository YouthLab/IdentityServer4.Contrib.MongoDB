﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.MongoDB.Configuration;
using IdentityServer4.MongoDB.DbContexts;
using IdentityServer4.MongoDB.Interfaces;
using IdentityServer4.MongoDB.Services;
using IdentityServer4.MongoDB.Stores;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServerMongoDBBuilderExtensions
    {
        public static IIdentityServerBuilder AddConfigurationStore(
           this IIdentityServerBuilder builder, Action<MongoDBConfiguration> setupAction)
        {
            builder.Services.Configure(setupAction);

            return builder.AddConfigurationStore();
        }

        public static IIdentityServerBuilder AddConfigurationStore(
            this IIdentityServerBuilder builder, IConfiguration configuration)
        {
            builder.Services.Configure<MongoDBConfiguration>(configuration);

            return builder.AddConfigurationStore();
        }

        public static IIdentityServerBuilder AddOperationalStore(
           this IIdentityServerBuilder builder, Action<MongoDBConfiguration> setupAction)
        {
            builder.Services.Configure(setupAction);

            return builder.AddOperationalStore();
        }

        public static IIdentityServerBuilder AddOperationalStore(
            this IIdentityServerBuilder builder, IConfiguration configuration)
        {
            builder.Services.Configure<MongoDBConfiguration>(configuration);

            return builder.AddOperationalStore();
        }

        private static IIdentityServerBuilder AddConfigurationStore(
            this IIdentityServerBuilder builder)
        {
            builder.Services.AddScoped<IConfigurationDbContext, ConfigurationDbContext>();

            builder.Services.AddTransient<IClientStore, ClientStore>();
            builder.Services.AddTransient<IScopeStore, ScopeStore>();
            builder.Services.AddTransient<ICorsPolicyService, CorsPolicyService>();

            return builder;
        }

        private static IIdentityServerBuilder AddOperationalStore(
            this IIdentityServerBuilder builder)
        {
            builder.Services.AddScoped<IPersistedGrantDbContext, PersistedGrantDbContext>();

            builder.Services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();

            return builder;
        }
    }
}