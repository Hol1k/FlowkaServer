using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FlowkaDbContext;

public class ServiceEntity
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = "";
    [MaxLength(1000)]
    public string Note { get; set; } = "";
    [MaxLength(100)]
    public string Price { get; set; } = "";
    public int Duration { get; set; }
    public bool IsComplete { get; set; }
    public int ClientId { get; set; }
    [JsonIgnore]
    [ValidateNever]
    public ClientEntity Client { get; set; } = null!;
}