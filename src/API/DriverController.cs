using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API;

[ApiController]
[Route("api/drivers")]
public class DriverController(IDriverService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
    {
        try
        {
            var drivers = await service.GetAllDriversAsync();
            return Ok(drivers);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    //Get driver by ID (/api/drivers/{id}). This endpoint should return detailed information, 
    //such as car number, car manufacturer name, car model name. 

    [HttpGet("/id")]
    public async Task<ActionResult<IEnumerable<Driver>>> GetDriverById(
        [FromQuery] int id
    )
    {
        try
        {
            var drivers = await service.GetDriverByIdAsync(id);
            return Ok(drivers);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}