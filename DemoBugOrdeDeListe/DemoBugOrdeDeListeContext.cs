using DemoBugOrdeDeListe.Models;
using Microsoft.EntityFrameworkCore;

public class DemoBugOrdeDeListeContext(DbContextOptions<DemoBugOrdeDeListeContext> options) : DbContext(options)
{
    public DbSet<Parent> Parent { get; set; } = default!;
    public DbSet<Enfant> Enfant { get; set; } = default!;
    public DbSet<EnfantAvecIndex> EnfantAvecIndex { get; set; } = default!;
}
