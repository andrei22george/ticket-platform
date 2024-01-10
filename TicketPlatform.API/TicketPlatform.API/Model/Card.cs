using Dapper.Contrib.Extensions;

namespace TicketPlatform.API.Model
{
    [Table("Card")]
    public class Card : Entity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public DateTime ExpDate { get; set; }
        public int IdUser { get; set; }
    }
}
