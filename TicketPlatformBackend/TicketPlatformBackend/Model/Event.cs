using Dapper.Contrib.Extensions;

namespace TicketPlatformBackend.Model
{
    [Table("Event")]
    internal class Event : Entity<int>
    {
        public string EventTitle { get; set; } = string.Empty;
        public string EventDescription { get; set; } = string.Empty;
        public string EventThumbnail { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
