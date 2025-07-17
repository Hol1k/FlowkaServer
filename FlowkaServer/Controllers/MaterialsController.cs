using FlowkaDbContext;
using FlowkaDbContext.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowkaServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaterialsController : ControllerBase
{
    private readonly FlowkaContext  _db;

    public MaterialsController(FlowkaContext db)
    {
        _db = db;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMaterials()
    {
        var materialOperations = await _db.MaterialOperations.ToListAsync();

        List<string> materialNames = materialOperations
            .Select(m => m.MaterialName)
            .Distinct()
            .ToList();

        List<MaterialDto> materials = new();
        foreach (var materialName in materialNames)
        {
            var materialCount = materialOperations
                .Where(m => m.MaterialName == materialName)
                .Sum(m => m.Quantity);
            
            materials.Add(new MaterialDto{Name = materialName, Count = materialCount});
        }
        
        return Ok(materials);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetMaterialsByName(string name)
    {
        var materialCount = await _db.MaterialOperations
            .Where(m => m.MaterialName == name)
            .SumAsync(m => m.Quantity);
            
        return Ok(new MaterialDto{Name = name, Count = materialCount});
    }

    [HttpPost]
    public async Task<IActionResult> AddMaterialOperation([FromBody] MaterialOperationEntity materialOperation)
    {
        await _db.MaterialOperations.AddAsync(new MaterialOperationEntity
            {
                MaterialName  = materialOperation.MaterialName,
                Quantity = materialOperation.Quantity
            });
        await _db.SaveChangesAsync();
        
        return Ok();
    }
}