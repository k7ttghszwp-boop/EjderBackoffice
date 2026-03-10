using Ejder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ejder.Infrastructure.Configurations;

public class TourConfiguration : IEntityTypeConfiguration<Tour>
{
    public void Configure(EntityTypeBuilder<Tour> builder)
    {
        builder.ToTable("Tours");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name_TR)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.Name_EN)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(t => t.DiscountedPrice)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(t => t.Category)
            .WithMany(c => c.Tours)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
