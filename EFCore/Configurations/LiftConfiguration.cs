using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Configurations;

internal class LiftConfiguration : IEntityTypeConfiguration<Lift>
{
    public void Configure(EntityTypeBuilder<Lift> builder)
    {
        builder.HasData(
            new Lift { Id = 1, Name = "Sax lift", Price = 200, Engine = "Elektrisk", Height = 10, PlaformCapacityKg = 2000 },
            new Lift { Id = 2, Name = "Bom lift", Price = 400, Engine = "Diesel", Height = 20, PlaformCapacityKg = 3000 },
            new Lift { Id = 3, Name = "Bil lift", Price = 300, Engine = "Elektrisk", Height = 30, PlaformCapacityKg = 2500 }
            );
    }
}
