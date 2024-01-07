using Dapper.Contrib.Extensions;

namespace TicketPlatform.API.Model
{
    [Table("Favourites")]
    public class Favourites : Entity<int>
    {
        public int UserId { get; set; }
        public int AdminId { get; set; }
        public int EventId { get; set; }
    }
}
