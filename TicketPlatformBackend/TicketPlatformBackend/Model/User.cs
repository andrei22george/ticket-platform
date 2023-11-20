using Dapper.Contrib.Extensions;

namespace TicketPlatformBackend.Model
{
    [Table("User")]
    public class User : Person
    {
        public string Password { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
