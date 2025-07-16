using Microsoft.EntityFrameworkCore;

namespace FlowkaDbContext;

public class FlowkaContext(DbContextOptions<FlowkaContext> options) : DbContext(options)
{
    public DbSet<ClientEntity> Clients { get; set; } =  null!;
    public DbSet<ServiceEntity> Services { get; set; } =  null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientEntity>()
            .HasMany(c => c.Services)
            .WithOne(s => s.Client)
            .HasForeignKey(s => s.ClientId);
        
        base.OnModelCreating(modelBuilder);
    }
}