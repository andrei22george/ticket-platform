namespace TicketPlatform.API.Model.In
{
    public record EventIn(
        string Title,
        string Description,
        string Thumbnail,
        DateTime Date,
        float Price,
        string Venue,
        string City,
        int TotalTickets);
}
