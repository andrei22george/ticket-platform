namespace TicketPlatform.API.Model.In
{
    public record TicketIn(
        int UserId,
        int AdminId,
        int EventId,
        string QRCode);
}
