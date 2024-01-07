using Dapper.Contrib.Extensions;

namespace TicketPlatform.API.Model
{
    [Table("Ticket")]
    public class Ticket : Entity<int>
    {
        public int UserId { get; set; }
        public int AdminId { get; set; }
        public int EventId { get; set; }
        public string QRCode { get; set; } = string.Empty;
    }
}
