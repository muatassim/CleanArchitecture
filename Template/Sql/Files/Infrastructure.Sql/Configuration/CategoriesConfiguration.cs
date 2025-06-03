using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Infrastructure.MicrosoftSql.Constants;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 
namespace CleanArchitecture.Infrastructure.MicrosoftSql.Configuration
{
    internal sealed class CategoriesConfiguration : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.ToTable(name: Database.Dbo.Tables.Categories.Name, schema: Database.Dbo.Name);
            builder.HasKey(l => new { l.Id });
            //properties
            builder.Property(l => l.Id)
                .HasColumnName(Database.Dbo.Tables.Categories.Columns.Id)
                // Ensure this property is set as Identity
                .ValueGeneratedOnAdd() 
                // Identity Column Strategy
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                .IsRequired();
            builder.Property(l => l.CategoryName)
                .HasColumnName(Database.Dbo.Tables.Categories.Columns.CategoryName)
                .IsRequired()
                .HasMaxLength(maxLength: 15);

            builder.Property(l => l.Description)
                .HasColumnName(Database.Dbo.Tables.Categories.Columns.Description)
                .IsRequired(false)
                .HasMaxLength(maxLength: 8);

           
        }
    }
}
