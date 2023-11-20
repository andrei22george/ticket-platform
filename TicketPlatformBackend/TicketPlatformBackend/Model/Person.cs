namespace TicketPlatformBackend.Model
{
    public abstract class Person : Entity<int>
    {
        public string Name { get; set; } =  string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
