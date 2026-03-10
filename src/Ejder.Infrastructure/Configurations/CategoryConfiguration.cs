using Ejder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ejder.Infrastructure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name_TR)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Name_EN)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.IsActive)
            .HasDefaultValue(true);
    }
}
