using Dapper.Contrib.Extensions;

namespace TicketPlatformBackend.Model
{
    [Table("Admin")]
    public class Admin : Person
    {
        public string Password { get; set; } = string.Empty;
    }
}
