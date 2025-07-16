using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlowkaDbContext;

public class ToolEntity
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = "";
    public DateOnly LastMaintenanceDate { get; set; }
    public int MaintenancePeriodDays { get; set; }
    public bool IsActive { get; set; }
    [JsonIgnore]
    public List<ServiceEntity> Services { get; set; } = new List<ServiceEntity>();
}