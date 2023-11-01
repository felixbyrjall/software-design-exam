namespace DB; 

public class Users {

        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int MurlocCoins { get; set; }
        public string Email { get; set; } = string.Empty;
}