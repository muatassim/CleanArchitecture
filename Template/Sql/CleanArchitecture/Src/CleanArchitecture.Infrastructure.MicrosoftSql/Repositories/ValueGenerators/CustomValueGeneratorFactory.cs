using CleanArchitecture.Infrastructure.MicrosoftSql.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;
namespace CleanArchitecture.Infrastructure.MicrosoftSql.Repositories.ValueGenerators;
public partial class CustomValueGeneratorFactory : ValueGeneratorFactory
{
    public override ValueGenerator Create(IProperty property, ITypeBase typeBase)
    { 
        string tableName = typeBase.GetTableName();
        string schemaName = typeBase.GetSchema();
        var fullTableName = string.IsNullOrEmpty(schemaName) ? tableName : $"{schemaName}.{tableName}";
        return fullTableName switch
        {
            $"{Database.Dbo.Name}.{Database.Dbo.Tables.Categories.Name}" => new CategoriesValueGenerator(),
            _ => throw new NotSupportedException($"No value generator available for type {typeBase.Name}")
        };
    }


}
