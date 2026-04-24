using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models;

public class Booking
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int LiftId { get; set; }
    public DateOnly? BookedDate { get; set; }
    public Customer Customer { get; set; } = null!;
    public Lift Lift { get; set; } = null!;
}
