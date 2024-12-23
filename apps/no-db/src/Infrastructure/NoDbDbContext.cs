using Microsoft.EntityFrameworkCore;

namespace NoDb.Infrastructure;

public class NoDbDbContext : DbContext
{
    public NoDbDbContext(DbContextOptions<NoDbDbContext> options)
        : base(options) { }
}
