using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalGameStore.DB
{
    public class Game
    {
        public int Game_Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int GenreId { get; set; }
        public int PublisherId { get; set; }
        public int Score { get; set; } // Hold the score for interestlist


    }
}
