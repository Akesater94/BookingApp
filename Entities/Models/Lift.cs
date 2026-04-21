using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models;

public class Lift
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Price { get; set; }
    public int Height { get; set; }
    public string Engine { get; set; } = null!;
    public int PlaformCapacityKg { get; set; }
    public bool Booked { get; set; }
    public ICollection<Customer> Customers { get; set; } = null!;
}
