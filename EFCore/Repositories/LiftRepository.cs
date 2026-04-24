using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Repositories;

public class LiftRepository(BookingDbContext context) : ILiftRepository
{
    public async Task AddLiftAsync(Lift lift)
    {
        await context.AddAsync(lift);
        context.SaveChanges();
    }

    public async Task GetLiftByIdAsync(int id)
    {
        await context.Lifts.Where(l => l.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Lift>> GetLiftsAsync()
    {
        return await context.Lifts
            .Include(l => l.Bookings)
            .ToListAsync();
    }

    public async Task UpdateBookedDateAsync(int id, DateOnly date)
    {
        Lift? lift = await context.Lifts
            .FirstOrDefaultAsync(l => l.Id == id);
        if (lift != null)
        {
            await context.SaveChangesAsync();
        }
    }
}
