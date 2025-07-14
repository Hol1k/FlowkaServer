using Microsoft.EntityFrameworkCore;

namespace FlowkaDbContext;

public class FlowkaContext(DbContextOptions<FlowkaContext> options) : DbContext(options)
{
    public DbSet<ClientEntity> Clients { get; set; } =  null!;
}