using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBase.Configuration
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyDataFixForSqlite(this ModelBuilder modelBuilder)
        {
            var converter = new ValueConverter<DateTimeOffset, long>(
                            dateTimeOffset => dateTimeOffset.ToUnixTimeMilliseconds(),
                            unixTime => DateTimeOffset.FromUnixTimeMilliseconds(unixTime).ToLocalTime());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                var dateTimeProperties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset) || p.PropertyType == typeof(DateTimeOffset?));

                foreach (var property in properties)
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                }

                foreach (var property in dateTimeProperties)
                {
                    //modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(new DateTimeToBinaryConverter());
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(converter);
                }
            }
        }
    }
}
