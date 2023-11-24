using System.ComponentModel.DataAnnotations;

namespace TicketPlatform.API.Model
{
    public abstract class Entity<T>
    {
        [Key]
        public T? Id { get; set; }
    }
}
