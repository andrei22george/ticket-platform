CREATE TABLE Ticket (
    id INT IDENTITY(1,1) PRIMARY KEY,
    userid INT,
    adminid INT,
    eventid INT,
    qrcode NVARCHAR(MAX)
);
