using ErrorOr;

namespace TicketPlatform.API.ServiceErrors
{
    public class Errors
    {
        public static class Admin
        {
            public static Error NotFound => Error.NotFound(
                code: "Admin.NotFound",
                description: "Admin not found");

            public static Error InvalidEmail => Error.Validation(
                code: "Admin.InvalidEmail",
                description: "Invalid admin email");

            public static Error InvalidPhone => Error.Validation(
                code: "Admin.InvalidPhone",
                description: "Invalid admin phone");

            public static Error InvalidPassword => Error.Validation(
                code: "Admin.InvalidPassword",
                description: "Invalid admin password");

            public static Error FailedValidation => Error.Validation(
                code: "Admin.FailedValidation",
                description: "AdminIn Validation Failed");
        }

        public static class User
        {
            public static Error NotFound => Error.NotFound(
                code: "User.NotFound",
                description: "User not found");

            public static Error InvalidEmail => Error.Validation(
                code: "User.InvalidEmail",
                description: "Invalid user email");

            public static Error InvalidPhone => Error.Validation(
                code: "User.InvalidPhone",
                description: "Invalid user phone");

            public static Error InvalidPassword => Error.Validation(
                code: "User.InvalidPassword",
                description: "Invalid user password");

            public static Error InvalidAge => Error.Validation(
                    code: "User.InvalidAge",
                    description: "Invalid user age");

            public static Error FailedValidation => Error.Validation(
                code: "User.FailedValidation",
                description: "UserIn Validation Failed");
        }

        public static class Event
        {
            public static Error NotFound => Error.NotFound(
                code: "Event.NotFound",
                description: "Event not found");

            public static Error InvalidURL => Error.Validation(
                code: "Event.InvalidThumbnailURL",
                description: "Invalid event thumbnail URL");

            public static Error InvalidDate => Error.Validation(
                code: "Event.InvalidDate",
                description: "Invalid event date");
        }

        public static class Ticket
        {
            public static Error NotFound => Error.NotFound(
                code: "Ticket.NotFound",
                description: "Ticket not found");

            public static Error InvalidThumbnailURL => Error.Validation(
                code: "Ticket.InvalidThumbnailURL",
                description: "Invalid ticket thumbnail URL");

            public static Error InvalidDate => Error.Validation(
                code: "Ticket.InvalidDate",
                description: "Invalid ticket date");

            public static Error InvalidPrice => Error.Validation(
                code: "Ticket.InvalidPrice",
                description: "Invalid ticket price");

            public static Error InvalidQRCode => Error.Validation(
                code: "Ticket.InvalidORCode",
                description: "Invalid ticket QRCode");
        }
    }
}
