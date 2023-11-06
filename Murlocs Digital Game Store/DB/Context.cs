using DigitalGameStore.DB;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DB;

public partial class Context : DbContext
{
    public DbSet<Game> Game => Set<Game>();
    public DbSet<Publisher> Publisher => Set<Publisher>();
    public DbSet<Genre> Genre => Set<Genre>();
    public DbSet<GameGenres> GameGenres => Set<GameGenres>();

    public DbSet<Interest> Interest => Set<Interest>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($@"Data source = Resources/DigitalGameStore.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Game>(entity => { entity.HasKey(e => e.ID); });
        modelBuilder.Entity<Publisher>(entity => { entity.HasKey(e => e.ID); });
        modelBuilder.Entity<Genre>(entity => { entity.HasKey(e => e.ID); });
        modelBuilder.Entity<GameGenres>(entity => { entity.HasKey(e => e.ID); });
        modelBuilder.Entity<Interest>(entity => { entity.HasKey(e => e.ID); });

        /* entity.Property(e => e.Name);
         entity.Property(e => e.Price);
         entity.Property(e => e.Date);
         entity.HasOne(d => d.Publiser_ID).WithMany(p =>p.PublisherID)
             .HasForeignKey(d => d.Publiser_ID);
     });
     modelBuilder.Entity<Publisher>(entity =>
     {
         entity.HasKey(e => e.PublisherID);
         entity.Property(e => e.Name);
     });*/
    }

}