using DigitalGameStore.DB;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DB;

<<<<<<<< HEAD:Murlocs Digital Game Store/DB/GameContext.cs
public partial class GameContext : DbContext {
    public DbSet<Game> Game { get; set; }
    public DbSet<Genre> Genre { get; set; }
    public DbSet<GameGenres> GameGenres { get; set; }
    public DbSet<InterestTable> InterestTable { get; set; }
========
public partial class Context : DbContext {
    public DbSet<Game> Game => Set<Game>();
    public DbSet<Publisher> Publisher => Set<Publisher>();
    public DbSet<Genre> Genre => Set<Genre>();
    public DbSet<GameGenres> GameGenres => Set<GameGenres>();
    
>>>>>>>> origin/fredrik:Murlocs Digital Game Store/DB/Context.cs

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($@"Data source = Resources/DigitalGameStore.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Game>(entity => { entity.HasKey(e => e.Game_Id); });
        modelBuilder.Entity<Publisher>(entity => { entity.HasKey(e => e.Publisher_Id); });
        modelBuilder.Entity<Genre>(entity => { entity.HasKey(e => e.Genre_Id); });
        modelBuilder.Entity<GameGenres>(entity => { entity.HasKey(e => e.GameGenres_Id); });
        modelBuilder.Entity<InterestTable>(entity => { entity.HasKey(e => e.Interest_Id); });

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