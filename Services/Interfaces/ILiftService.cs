using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces;

public interface ILiftService
{
    Task<List<Lift>> GetLiftsAsync();
    Task GetLiftByIdAsync(int id);
    Task UpdateBookedDateAsync(int id, DateOnly date);
    Task AddLiftAsync(Lift lift);
}
