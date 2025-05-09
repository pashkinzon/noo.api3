using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Core.Utils.Richtext;
using Noo.Api.Core.Utils.Richtext.Delta;

namespace Noo.Api.Core.DataAbstraction.Db;

public static class DbContextExtensions
{
    public static void RegisterModels(this ModelBuilder modelBuilder)
    {
        var modelTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(
                t => t.IsClass &&
                !t.IsAbstract &&
                t.GetCustomAttribute<ModelAttribute>() != null
            );

        foreach (var modelType in modelTypes)
        {
            if (!modelType.IsSubclassOf(typeof(BaseModel)))
            {
                throw new InvalidOperationException($"Model {modelType.Name} must inherit from BaseModel");
            }

            modelBuilder.Entity(modelType);

            modelBuilder.Entity(modelType)
                .HasKey("Id");

            modelBuilder.Entity(modelType)
                .Property("Id")
                .HasConversion(
                    new ValueConverter<Ulid, byte[]>(
                        v => v.ToByteArray(),
                        v => new Ulid(v)
                    )
                );

            modelBuilder.Entity(modelType)
                .Property("CreatedAt")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity(modelType)
                .Property("UpdatedAt")
                .ValueGeneratedOnUpdate();
        }
    }

    public static void UseRichTextColumns(this ModelBuilder modelBuilder)
    {
        var richTextProperties = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseModel)))
            .SelectMany(t => t.GetProperties())
            .Where(p => p?.GetCustomAttribute<RichTextColumnAttribute>() != null);

        foreach (var property in richTextProperties)
        {
            var richTextAttribute = property.GetCustomAttribute<RichTextColumnAttribute>()!;

            var converter = new ValueConverter<IRichTextType, string?>(
                v => v.ToString(),
                v => new DeltaRichText(v)
            );

            modelBuilder.Entity(property.DeclaringType!)
                .Property(property.Name)
                .HasConversion(converter)
                .HasColumnType(richTextAttribute.TypeName)
                .HasCharSet(richTextAttribute.Charset)
                .UseCollation(richTextAttribute.Collation);
        }
    }
}
