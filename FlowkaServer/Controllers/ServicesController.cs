using FlowkaDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowkaServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly FlowkaContext _db;

    public ServicesController(FlowkaContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        var services = await _db.Services.ToListAsync();
        return Ok(services);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetServiceById(int id)
    {
        var service = await _db.Services.FindAsync(id);
        
        if (service == null) 
            return NotFound();
        
        return Ok(service);
    }

    [HttpPost]
    public async Task<IActionResult> AddService([FromBody] ServiceEntity service)
    {
        await _db.Services.AddAsync(service);
        await _db.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetServiceById), new { Id = service.Id }, service);
    }
}