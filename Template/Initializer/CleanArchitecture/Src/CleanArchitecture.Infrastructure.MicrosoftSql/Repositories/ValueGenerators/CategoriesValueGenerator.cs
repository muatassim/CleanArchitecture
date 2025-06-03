using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;
namespace CleanArchitecture.Infrastructure.MicrosoftSql.Repositories.ValueGenerators;

public class CategoriesValueGenerator : ValueGenerator<int>
{
    private static int _currentMax = 0;
    public override bool GeneratesTemporaryValues => false;
    public override int Next(EntityEntry entry)
    {
        if (_currentMax <= 0)
            _currentMax = entry.Context.Set<Categories>().Max(c => c.Id) + 1;
        else
            _currentMax++;
        return _currentMax;
    }
}
