using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces;

public interface IBookingRepository
{
    Task AddBookingAsync(int customerId, int liftId, DateOnly date);
    Task<List<Booking>> GetLiftsAsync(int id);
    Task DeleteBookingAsync(Booking booking);


}
