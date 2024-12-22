using Events.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure;

public class EventsDbContext : DbContext
{
    public EventsDbContext(DbContextOptions<EventsDbContext> options)
        : base(options) { }

    public DbSet<GroupDbModel> Groups { get; set; }

    public DbSet<EventDbModel> Events { get; set; }
}
