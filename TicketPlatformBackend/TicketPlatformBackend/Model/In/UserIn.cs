﻿namespace TicketPlatformBackend.Model.In
{
    public record UserIn(
        string Name,
        string Email,
        string Password,
        int Age);
}
