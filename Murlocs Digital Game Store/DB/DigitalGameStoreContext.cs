using Microsoft.EntityFrameworkCore;

namespace DB; 

public partial class DigitalGameStoreContext : DbContext {
    public DbSet<Product> Product => Set<Product>();
    public DbSet<Publisher> Publisher => Set<Publisher>();
    public DbSet<Users> Users => Set<Users>();
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        string dbFilePath = Path.Combine( "Resources", "DigitalGameStore.db");
        optionsBuilder.UseSqlite($@"Data source = /Users/fredrikgaaso/Library/Mobile Documents/com~apple~CloudDocs/Desktop/HK/Eksamen-shit/eksamen_software/Murlocs Digital Game Store/Resources/DigitalGameStore.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        modelBuilder.Entity<Product>(entity => { entity.HasKey(e => e.Product_Id); });
        modelBuilder.Entity<Publisher>(entity => { entity.HasKey(e => e.Publisher_ID); });
        modelBuilder.Entity<Users>(entity => { entity.HasKey(e => e.UserID); });
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