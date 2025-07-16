using FlowkaDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowkaServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToolsController : ControllerBase
{
    private readonly FlowkaContext _db;

    public ToolsController(FlowkaContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetTools()
    {
        var tools = await _db.Tools
            .Include(t => t.Services)
            .ToListAsync();
        
        return Ok(tools);
    }

    [HttpPost]
    public async Task<IActionResult> AddTool([FromBody] ToolEntity tool)
    {
        await _db.Tools.AddAsync(tool);
        await _db.SaveChangesAsync();
        
        return Ok();
    }
}