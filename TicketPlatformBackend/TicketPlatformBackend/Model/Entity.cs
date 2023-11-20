using System.ComponentModel.DataAnnotations;

namespace TicketPlatformBackend.Model
{
    public abstract class Entity<T>
    {
        [Key]
        public T? Id { get; set; }
    }
}
