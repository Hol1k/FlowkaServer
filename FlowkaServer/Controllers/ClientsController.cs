using FlowkaDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowkaServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly FlowkaContext _db;

    public ClientsController(FlowkaContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetClients()
    {
        var clients = await _db.Clients.ToListAsync();
        return Ok(clients);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetClientById(int id)
    {
        var client = await _db.Clients.FindAsync(id);
        
        if (client == null) 
            return NotFound();
            
        return Ok(client);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] ClientEntity client)
    {
        await _db.Clients.AddAsync(client);
        await _db.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetClientById), new { Id = client.Id }, client);
    }
}