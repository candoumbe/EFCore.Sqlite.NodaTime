using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VerifyTests.EntityFramework;

namespace Microsoft.EntityFrameworkCore.Sqlite
{
    public class NodaTimeContext : DbContext
    {
        private readonly TestLoggerFactory _loggerFactory = new();

        public string Sql => _loggerFactory.Logger.Sql;

        public string Parameters => _loggerFactory.Logger.Parameters;

        public DbSet<NodaTimeTypes> NodaTimeTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var builder = new SqliteConnectionStringBuilder
            {
                DataSource = "NodaTime." + Guid.NewGuid() + ".db",
                Cache = SqliteCacheMode.Private,
            };

            options
                .UseSqlite(builder.ConnectionString, x => x.UseNodaTime())
                .UseLoggerFactory(_loggerFactory)
                .EnableSensitiveDataLogging()
                .EnableRecording();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var model = modelBuilder.Model;

            model.RemoveAnnotation(CoreAnnotationNames.ProductVersion);

            modelBuilder.Entity<NodaTimeTypes>()
                .HasData(new NodaTimeTypes
                {
                    LocalDateTime = QueryTests.LocalDateTimeQueryTests.Value,
                    LocalDate = QueryTests.LocalDateQueryTests.Value,
                    LocalTime = QueryTests.LocalTimeQueryTests.Value,
                    Instant = QueryTests.InstantQueryTests.Value,
                    Id = 1,
                });
        }
    }
}
