using Microsoft.EntityFrameworkCore;

namespace MaterialAccounting.DataAccess.Postgres;
public class MaterialAccountingDbContext : DbContext
{
    public MaterialAccountingDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<MyClass> Test { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MaterialAccountingDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

public sealed class MyClass
{
    public int MyProperty { get; set; }
}
