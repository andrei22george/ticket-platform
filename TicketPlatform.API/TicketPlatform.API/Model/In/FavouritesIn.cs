namespace TicketPlatform.API.Model.In
{
    public record FavouritesIn(
        int UserId,
        int AdminId,
        int EventId);
}
