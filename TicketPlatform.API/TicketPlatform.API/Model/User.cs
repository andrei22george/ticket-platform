using Dapper.Contrib.Extensions;

namespace TicketPlatform.API.Model
{
    [Table("[User]")]
    public class User : Person
    {
        public string Password { get; set; } = string.Empty;
        public int Age { get; set; }
        public int IdCard { get; set; }
    }
}
