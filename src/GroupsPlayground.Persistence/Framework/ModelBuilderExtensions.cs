using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace GroupsPlayground.Persistence.Framework
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder UseValueConverter(
            this ModelBuilder modelBuilder, ValueConverter converter)
        {
            var type = converter.ModelClrType;

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType
                    .ClrType
                    .GetProperties()
                    .Where(p => p.PropertyType == type);

                foreach (var property in properties)
                {
                    modelBuilder
                        .Entity(entityType.Name)
                        .Property(property.Name)
                        .HasConversion(converter);
                }
            }

            return modelBuilder;
        }

        public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class
        {
            ValueConverter<T, string> converter = new ValueConverter<T, string>
            (
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<T>(v)
            );

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.HasColumnType("nvarchar(max)");

            return propertyBuilder;
        }
    }
}