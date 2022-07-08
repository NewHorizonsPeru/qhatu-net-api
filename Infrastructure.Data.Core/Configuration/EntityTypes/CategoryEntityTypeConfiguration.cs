using Domain.MainModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Core.Configuration.EntityTypes
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.ToTable("category");

            entity.Property(e => e.CategoryId).HasColumnName("categoryId");

            entity.Property(e => e.Active).HasColumnName("active");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt")
                .HasDefaultValueSql("(getutcdate())");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("description");
        }
    }
}