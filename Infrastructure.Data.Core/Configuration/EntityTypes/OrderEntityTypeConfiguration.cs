using Domain.MainModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Core.Configuration.EntityTypes
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity.ToTable("order");

            entity.Property(e => e.OrderId).HasColumnName("orderId");

            entity.Property(e => e.Comment)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("comment");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt")
                .HasDefaultValueSql("(getutcdate())");

            entity.Property(e => e.PaymentMethod)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("paymentMethod")
                .IsFixedLength(true);

            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_User");
        }
    }
}