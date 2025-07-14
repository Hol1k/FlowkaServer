using System.ComponentModel.DataAnnotations;

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
}