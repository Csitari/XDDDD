namespace Vizsgaremek_Backend.Models.JWT
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
    }
}
