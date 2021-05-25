using FinancesCore.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancesCore.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                   .IsRequired()
                   .HasColumnType("varchar(200)");

            builder.HasMany(c => c.Transactions)
            .WithOne(t => t.Category)
            .HasForeignKey(t => t.CategoryId);

            builder.ToTable("Categories");
        }
    }
}