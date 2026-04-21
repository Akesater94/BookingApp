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
            new Lift { Id = 1, Name = "Scissor lift", Price = 200, Engine = "Electric", Height = 10, Booked = false, PlaformCapacityKg = 2000 },
            new Lift { Id = 2, Name = "Telescopic lift", Price = 400, Engine = "Diesel", Height = 20, Booked = false, PlaformCapacityKg = 3000 },
            new Lift { Id = 3, Name = "Carmounted lift", Price = 300, Engine = "Electric", Height = 30, Booked = false, PlaformCapacityKg = 2500 }
            );
    }
}
