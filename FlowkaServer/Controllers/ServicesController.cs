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
        var services = await _db.Services
            .Include(s => s.MaterialOperations)
            .Include(s => s.Tools)
            .ToListAsync();
        return Ok(services);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetServiceById(int id)
    {
        var service = await _db.Services.FindAsync(id);
        
        if (service == null) 
            return NotFound();
        
        service.MaterialOperations = await _db.MaterialOperations.Where(s => s.ServiceId == id).ToListAsync();
        
        return Ok(service);
    }

    [HttpPost]
    public async Task<IActionResult> AddService([FromBody] ServiceEntity service)
    {
        var connectedClient = await _db.Clients.FindAsync(service.ClientId);
        
        if (connectedClient != null)
            service.Client = connectedClient;
        
        var toolsInDb = await _db.Tools.ToListAsync();
        var toolsInService = service.Tools;
        var newToolsList = new List<ToolEntity>();
        foreach (var toolInService in toolsInService)
        {
            var toolInDb = toolsInDb.FirstOrDefault(t => t.Id == toolInService.Id);

            newToolsList.Add(toolInDb ?? toolInService);
        }
        service.Tools = newToolsList;
        
        var materialsInService = service.MaterialOperations;
        var newMaterialsList = new List<MaterialOperationEntity>();
        foreach (var materialInService in materialsInService)
        {
            var newMaterial = new MaterialOperationEntity
            {
                MaterialName = materialInService.MaterialName,
                Quantity = materialInService.Quantity,
                Service = service
            };
            
            newMaterialsList.Add(newMaterial);
        }
        service.MaterialOperations = newMaterialsList;
        
        await _db.Services.AddAsync(service);
        
        await _db.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetServiceById), new { Id = service.Id }, service);
    }
}