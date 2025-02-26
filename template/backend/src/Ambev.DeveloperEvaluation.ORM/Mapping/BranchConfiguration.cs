using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branchs");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Code).IsRequired().HasMaxLength(10);
        builder.Property(u => u.City).IsRequired().HasMaxLength(100);
        builder.Property(u => u.State).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Country).IsRequired().HasMaxLength(100);
        builder.Property(u => u.PostalCode).IsRequired().HasMaxLength(9);
        builder.Property(u => u.Phone).HasMaxLength(20);
        builder.Property(u => u.Email).HasMaxLength(100);

        builder.Property(u => u.Status)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}
