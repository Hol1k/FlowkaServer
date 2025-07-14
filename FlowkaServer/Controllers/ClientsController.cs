using FlowkaDbContext;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetClients()
    {
        var clients = _db.Clients.ToList();
        return Ok(clients);
    }

    [HttpPost]
    public IActionResult CreateClient([FromBody] ClientEntity client)
    {
        _db.Clients.Add(client);
        _db.SaveChanges();
        return CreatedAtAction(nameof(GetClients), new { Id = client.Id }, client);
    }
}