namespace TicketPlatform.API.Model.In
{
    public record CardIn(
        string Name,
        string CardNumber,
        string CVV,
        DateTime ExpDate,
        int IdUser);
}
