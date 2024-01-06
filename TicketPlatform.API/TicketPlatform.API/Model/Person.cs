namespace TicketPlatform.API.Model
{
    public class Person : Entity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
    }
}
