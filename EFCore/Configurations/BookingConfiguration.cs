using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Configurations;

internal class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasData(
            new Booking {Id = 1, CustomerId = 1, LiftId = 1, BookedDate = (new DateOnly(2026, 06, 02)) },
            new Booking {Id = 2, CustomerId = 1, LiftId = 2, BookedDate = (new DateOnly(2026, 06, 10)) },
            new Booking {Id = 3, CustomerId = 2, LiftId = 1, BookedDate = (new DateOnly(2026, 06, 12)) },
            new Booking {Id = 4, CustomerId = 2, LiftId = 1, BookedDate = (new DateOnly(2026, 05, 31)) },
            new Booking {Id = 5, CustomerId = 2, LiftId = 3, BookedDate = (new DateOnly(2026, 06, 29)) },
            new Booking {Id = 6, CustomerId = 3, LiftId = 1, BookedDate = (new DateOnly(2026, 05, 20)) }
            );
    }
}
