using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branchs");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(b => b.Name).IsRequired().HasMaxLength(100);
        builder.Property(b => b.Code).IsRequired().HasMaxLength(10);
        builder.Property(b => b.City).IsRequired().HasMaxLength(100);
        builder.Property(b => b.State).IsRequired().HasMaxLength(100);
        builder.Property(b => b.Country).IsRequired().HasMaxLength(100);
        builder.Property(b => b.PostalCode).IsRequired().HasMaxLength(9);
        builder.Property(b => b.Phone).HasMaxLength(20);
        builder.Property(b => b.Email).HasMaxLength(100);

        builder.Property(b => b.Status)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}
