﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.Number).IsRequired();
        builder.Property(s => s.Date).IsRequired();
        builder.Property(s => s.CustomerId).IsRequired();
        builder.Property(s => s.BranchId).IsRequired();
        builder.Property(s => s.TotalSaleAmount).IsRequired();
    }
}
