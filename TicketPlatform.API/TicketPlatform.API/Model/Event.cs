﻿using Dapper.Contrib.Extensions;

namespace TicketPlatform.API.Model
{
    [Table("Event")]
    public class Event : Entity<int>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public string Venue { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int TotalTickets { get; set; }
    }
}
