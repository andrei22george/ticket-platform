using Dapper.Contrib.Extensions;

namespace TicketPlatformBackend.Model
{
    [Table("Ticket")]
    public class Ticket : Entity<int>
    {
        public string EventTitle { get; set; } = string.Empty;
        public string EventDescription { get; set; } = string.Empty;
        public string EventThumbnail { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public float Price { get; set; }
        public string QRCode { get; set; } = string.Empty;
    }
}
