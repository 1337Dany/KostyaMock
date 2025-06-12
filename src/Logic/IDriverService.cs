using DTOs;
using Models;

namespace Logic;

public interface IDriverService
{
    public Task<List<DriverDTO>> GetAllDriversAsync();
    public Task<DriverDetailedDTO> GetDriverByIdAsync(int id);
}