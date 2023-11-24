using AutoMapper;
using TicketPlatform.API.Model.In;
using TicketPlatform.API.Model;

namespace TicketPlatform.API
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
