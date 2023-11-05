using Microsoft.EntityFrameworkCore;

namespace DigitalGameStore.RecommendGames
{
    public class GameContext : DbContext
    {
        public DbSet<Games> Games { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Game_Genres> Game_Genres { get; set; }
        public DbSet<Interest_List> Interest_List { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=digitalGameStore.db");
        }
    }

    public class Games
    {
        public int Game_id { get; set; }
        public string Name { get; set; }
        public int Publisher_ID { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Score { get; set; }
        public Publisher Publisher { get; set; }  // Navigation property
    }

    public class Publisher
    {
        public int Publisher_ID { get; set; }
        public string PublisherName { get; set; }
    }

    public class Genres
    {
        public int Genre_Id { get; set; }
        public string GenreName { get; set; }
    }

    public class Game_Genres
    {
        public int Game_id { get; set; }
        public int Genre_id { get; set; }
        public Games Game { get; set; }  // Navigation property
        public Genres Genre { get; set; }  // Navigation property
    }

    public class Interest_List
    {
        public int Interest_id { get; set; }
        public int Game_id { get; set; }
        public Games Game { get; set; }  // Navigation property
    }
}
