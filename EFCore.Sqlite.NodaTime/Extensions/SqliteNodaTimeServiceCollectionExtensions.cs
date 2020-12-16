using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Sqlite.Query.Internal;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class SqliteNodaTimeServiceCollectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkSqliteNodaTime(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            new EntityFrameworkRelationalServicesBuilder(services)
                .TryAddProviderSpecificServices(x => x
                    .TryAddSingletonEnumerable<IEvaluatableExpressionFilterPlugin, SqliteNodaTimeEvaluatableExpressionFilterPlugin>());

            return services;
        }
    }
}