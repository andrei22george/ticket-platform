using Dapper.Contrib.Extensions;

namespace TicketPlatform.API.Model
{
    [Table("Cart")]
    public class Cart
    {
        public int IdEvent { get; set; }
        public int IdUser { get; set; }
        public int TicketsNumber { get; set; }
    }
}
