using AutoMapper;
using TicketPlatformBackend.Model;  
using TicketPlatformBackend.Model.In;

namespace TicketPlatformBackend
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<AdminIn, Admin>();
            CreateMap<UserIn, User>();
            CreateMap<EventIn, Event>();
            CreateMap<TicketIn, Ticket>();
        }
    }
}
