using DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace Logic;

public class DriverService : IDriverService
{
    private readonly AppDbContext _context;

    public DriverService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<DriverDTO>> GetAllDriversAsync()
    {
        var query = await _context.Drivers
            .OrderBy(d => d.FirstName)
            .Select(d => new DriverDTO
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Birthday = d.Birthday
            })
            .ToListAsync();

        if (query.IsNullOrEmpty())
        {
            throw new Exception("No drivers found");
        }
        return query;
    }

    public async Task<DriverDetailedDTO> GetDriverByIdAsync(int id)
    {
        var query = await _context.Drivers
            .Where(d => d.Id == id)
            .Include(d => d.Car)
            .ThenInclude(d => d.CarManufacture)
            .Select(d => new DriverDetailedDTO()
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Birthday = d.Birthday,
                    CarNumber = d.Car.Number,
                    CarManufacturerName = d.Car.CarManufacture.Name,
                    CarModelName = d.Car.ModelName
                }
            ).FirstOrDefaultAsync();

        if (query == null)
        {
            throw new Exception("No Driver found");
        }

        return query;
    }
}