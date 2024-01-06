using Dapper.Contrib.Extensions;

namespace TicketPlatform.API.Model
{
    [Table("Favourites")]
    public class Favourites : Entity<int>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
