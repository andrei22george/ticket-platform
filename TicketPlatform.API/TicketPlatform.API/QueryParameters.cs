namespace TicketPlatform.API
{
    public class QueryParameters
    {
        public int Page { get; set; } = 1;
        public bool SortByAsc { get; set; } = true;
        public string EventName { get; set; } = string.Empty;
        public DateTime EventDate { get; set; } = new DateTime(1753, 1, 1);
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
