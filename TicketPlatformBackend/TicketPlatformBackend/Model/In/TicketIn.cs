namespace TicketPlatformBackend.Model.In
{
    public record TicketIn(
        string EventTitle,
        string EventDescription,
        string EventThumbnail,
        DateTime EventDate,
        float Price,
        string QRCode);
}
