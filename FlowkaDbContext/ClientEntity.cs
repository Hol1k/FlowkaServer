using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlowkaDbContext;

public class ClientEntity
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = "";
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = "";
    [MaxLength(1000)]
    public string Note { get; set; } = "";
    [JsonIgnore]
    public List<ServiceEntity> Services { get; set; } = new List<ServiceEntity>();
}