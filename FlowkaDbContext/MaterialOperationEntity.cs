using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlowkaDbContext;

public class MaterialOperationEntity
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string MaterialName { get; set; } = "";
    public int Quantity { get; set; }
    public int? ServiceId { get; set; }
    [JsonIgnore]
    public ServiceEntity? Service { get; set; }
}