using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Core.Utils;
using Noo.Api.Core.Utils.Richtext;
using Noo.Api.Core.Utils.Richtext.Delta;

namespace Noo.Api.Core.DataAbstraction.Db;

public static class DbContextExtensions
{
    /// <summary>
    /// Registers all models in the assembly marked with ModelAttribute with the DbContext.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
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
                .Property("CreatedAt")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity(modelType)
                .Property("UpdatedAt")
                .ValueGeneratedOnUpdate();
        }
    }

    /// <summary>
    /// Configures many-to-many tables in the database.
    /// Makes readable jsoin table names and configures ON DELETE CASCADE.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public static void ConfigureManyToManyTables(this ModelBuilder modelBuilder)
    {
        var skipNavs = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(et => et.GetSkipNavigations());

        foreach (var skipNav in skipNavs)
        {
            var joinEntityType = skipNav.JoinEntityType;

            if (joinEntityType == null)
            {
                continue;
            }

            var rightName = skipNav.TargetEntityType.ClrType.GetCustomAttribute<ModelAttribute>()?.Name;
            var leftName = skipNav.DeclaringEntityType.ClrType
                .GetCustomAttribute<ModelAttribute>()?.Name;

            var propName = skipNav.Name;

            if (rightName == null || leftName == null)
            {
                throw new InvalidOperationException(
                    $"Join entity type {joinEntityType.Name} does not have a valid name attribute."
                );
            }

            var tableName = string.CompareOrdinal(leftName, rightName) <= 0
                ? $"{leftName}_mm_{propName}_{rightName}"
                : $"{rightName}_mm_{propName}_{leftName}";

            joinEntityType.SetTableName(tableName);

            foreach (var fk in joinEntityType.GetForeignKeys())
            {
                var principal = fk.PrincipalEntityType.ClrType.Name;

                principal = principal.EndsWith("Model") ? principal[0..^5] : principal;

                fk.Properties[0].SetColumnName(StringHelpers.ToSnakeCase($"{principal}Id"));
                fk.DeleteBehavior = DeleteBehavior.Cascade;
            }
        }
    }

    /// <summary>
    /// Configures rich text columns in the database.
    /// </summary>
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
