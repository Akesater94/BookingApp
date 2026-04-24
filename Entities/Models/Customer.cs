using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Booking> Bookings { get; set; } = null!;
}
