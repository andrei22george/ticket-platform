namespace TicketPlatform.API.Model.In
{
    public record CartIn(
        int IdEvent,
        int IdUser,
        string TicketsNumber);
}
