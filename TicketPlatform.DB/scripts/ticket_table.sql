CREATE TABLE Ticket (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EventTitle NVARCHAR(50),
    EventDescription NVARCHAR(MAX), -- You can adjust the length as needed
    EventThumbnail NVARCHAR(MAX),   -- You can adjust the length as needed
    EventDate DATE,
    Price FLOAT,
    QRCode NVARCHAR(MAX)            -- You can adjust the length as needed
);
