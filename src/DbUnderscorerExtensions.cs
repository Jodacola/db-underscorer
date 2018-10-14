using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public static class DbUnderscorerExtensions
{
    /// <summary>
    ///     <para>
    ///         Ensures table names, columns names, key names, and indices are lowercased and underscored in the backing database.
    /// 
    ///     </para>
    ///     <para>
    ///         If using with DbContexts which add additional tables under the hood, such as IdentityDbContexts, this method should be called
    ///         after calling base.OnModelCreating, in order to ensure all extra tables are considered by this method.
    ///     </para>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="builder"></param>
    public static void UnderscoreDatabase(this DbContext context, ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            entity.Relational().TableName = entity.Relational().TableName.Underscore();
            entity.UnderscoreProperties();
            entity.UnderscoreKeys();
            entity.UnderscoreIndices();
        }
    }

    /// <summary>
    /// Underscores property names for the given IMutableEntityType.
    /// </summary>
    /// <param name="entity"></param>
    public static void UnderscoreProperties(this IMutableEntityType entity)
    {
        foreach (var property in entity.GetProperties())
        {
            property.Relational().ColumnName = property.Name.Underscore();
        }
    }

    /// <summary>
    /// Underscores primary and foreign keys for the given IMutableEntityType.
    /// </summary>
    /// <param name="entity"></param>
    public static void UnderscoreKeys(this IMutableEntityType entity)
    {
        foreach (var key in entity.GetKeys())
        {
            key.Relational().Name = key.Relational().Name.Underscore();
        }

        foreach (var key in entity.GetForeignKeys())
        {
            key.Relational().Name = key.Relational().Name.Underscore();
        }
    }

    /// <summary>
    /// Underscores index names for the given IMutableEntityType.
    /// </summary>
    /// <param name="entity"></param>
    public static void UnderscoreIndices(this IMutableEntityType entity)
    {
        foreach (var index in entity.GetIndexes())
        {
            index.Relational().Name = index.Relational().Name.Underscore();
        }
    }
}