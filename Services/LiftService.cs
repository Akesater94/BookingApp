using Entities.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services;

public class LiftService(ILiftRepository liftRepository) : ILiftService
{
    public async Task AddLiftAsync(Lift lift)
    {
        await liftRepository.AddLiftAsync(lift);
    }

    public async Task GetLiftByIdAsync(int id)
    {
        await liftRepository.GetLiftByIdAsync(id);
    }

    public async Task<List<Lift>> GetLiftsAsync()
    {
        return await liftRepository.GetLiftsAsync();
    }

    public async Task UpdateBookedDateAsync(int id, DateOnly date)
    {
        await liftRepository.UpdateBookedDateAsync(id, date);
    }
}
