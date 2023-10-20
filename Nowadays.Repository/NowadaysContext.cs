using Microsoft.EntityFrameworkCore;
using Nowadays.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nowadays.Repository;

public class NowadaysContext : DbContext
{
    public NowadaysContext(DbContextOptions<NowadaysContext> options) : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
    public override int SaveChanges()
    {
        OnBeforeSave();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSave()
    {
        foreach (var item in ChangeTracker.Entries())
        {
            if (item.Entity is BaseEntity entityReference)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        {
                            entityReference.CreatedDate = DateTime.Now;
                            break;
                        }
                    case EntityState.Modified:
                        {
                            entityReference.UpdatedDate = DateTime.Now;
                            break;
                        }
                }
            }

        }
    }
}