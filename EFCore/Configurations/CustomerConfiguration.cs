using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasData(
            new Customer { Id = 1, Name = "Tofta plåt och ventilation" },
            new Customer { Id = 2, Name = "PEAB"},
            new Customer { Id = 3, Name = "Assemblin"}
        );
    }
}
