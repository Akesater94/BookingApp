using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;

namespace Services.Interfaces;

public interface ILiftRepository
{
    Task<List<Lift>> GetLiftsAsync();
    Task GetLiftByIdAsync(int id);
    Task UpdateBookedDateAsync(int id, DateOnly date);
    Task AddLiftAsync(Lift lift);
}
