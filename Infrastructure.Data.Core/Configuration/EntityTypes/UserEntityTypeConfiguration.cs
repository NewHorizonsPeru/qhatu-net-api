using Domain.MainModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Core.Configuration.EntityTypes
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("user");

            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("createAt")
                .HasDefaultValueSql("(getutcdate())");

            entity.Property(e => e.DocumentNumber)
                .IsRequired()
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("documentNumber");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("email");

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("firstName");

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("lastName");

            entity.Property(e => e.MobilePhone)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("mobilePhone");

            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("password");

            entity.Property(e => e.PhotoUrl)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("photoUrl");
        }
    }
}