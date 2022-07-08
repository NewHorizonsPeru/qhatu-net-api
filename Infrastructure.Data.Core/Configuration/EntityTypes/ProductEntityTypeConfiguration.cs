using Domain.MainModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Core.Configuration.EntityTypes
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("product");

            entity.Property(e => e.ProductId).HasColumnName("productId");

            entity.Property(e => e.Active).HasColumnName("active");

            entity.Property(e => e.CategoryId).HasColumnName("categoryId");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt")
                .HasDefaultValueSql("(getutcdate())");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");

            entity.Property(e => e.ImageUrl)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("imageUrl");

            entity.Property(e => e.SalePrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("salePrice");

            entity.Property(e => e.Sku)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sku");

            entity.HasOne(d => d.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_category");
        }
    }
}