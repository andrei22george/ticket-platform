CREATE TABLE Favourites (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(50),
    Description NVARCHAR(MAX), -- Adjust the length based on your needs
    Thumbnail NVARCHAR(MAX),   -- Adjust the length based on your needs
    DateAdded DATE
);
