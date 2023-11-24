using Dapper.Contrib.Extensions;

namespace TicketPlatform.API.Model
{
    [Table("Admin")]
    public class Admin : Person
    {
        public string Password { get; set; } = string.Empty;
    }
}
