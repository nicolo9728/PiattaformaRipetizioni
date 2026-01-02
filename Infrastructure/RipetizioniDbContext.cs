using System;
using Microsoft.EntityFrameworkCore;
using RipetizioniApp.Models;

namespace RipetizioniApp.Infrastructure;

public class RipetizioniDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<Utente> Utenti { get; private set; }
    public DbSet<Docente> Docenti { get; private set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration["DatabaseConnectionString"]);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RipetizioniDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}

