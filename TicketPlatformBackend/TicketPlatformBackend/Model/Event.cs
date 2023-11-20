using Dapper.Contrib.Extensions;

namespace TicketPlatformBackend.Model
{
    [Table("Event")]
    internal class Event : Entity<int>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
