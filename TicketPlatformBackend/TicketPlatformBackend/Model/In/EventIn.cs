namespace TicketPlatformBackend.Model.In
{
    public record EventIn(
        string Title,
        string Description,
        string Thumbnail,
        DateTime Date);
}
