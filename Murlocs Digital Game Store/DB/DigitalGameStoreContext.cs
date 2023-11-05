using DigitalGameStore.DB;
using DigitalGameStore.RecommendGames;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DB;

public partial class DigitalGameStoreContext : DbContext
{
    public DbSet<Game> Game => Set<Game>();
    public DbSet<Publisher> Publisher => Set<Publisher>();
    public DbSet<Genre> Genre => Set<Genre>();
    public DbSet<GameGenres> GameGenres => Set<GameGenres>();


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($@"Data source = Resources/DigitalGameStore.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Game>(entity => { entity.HasKey(e => e.Game_Id); });
        modelBuilder.Entity<Publisher>(entity => { entity.HasKey(e => e.Publisher_ID); });
        modelBuilder.Entity<Genre>(entity => { entity.HasKey(e => e.Genre_ID); });
        modelBuilder.Entity<GameGenres>(entity => { entity.HasKey(e => e.GameGenres_ID); });
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