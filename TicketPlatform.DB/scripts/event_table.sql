CREATE TABLE Event
(
    id INT IDENTITY(1,1) PRIMARY KEY,
    title NVARCHAR(50),
    description NVARCHAR(MAX), -- Assuming a potentially longer description
    thumbnail NVARCHAR(50),
    date DATE
);
