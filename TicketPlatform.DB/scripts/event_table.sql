CREATE TABLE EventTable (
    id INT PRIMARY KEY IDENTITY(1,1),
    title NVARCHAR(255) NOT NULL,
    description NVARCHAR(MAX),
    thumbnail NVARCHAR(MAX),
    date DATE,
    price DECIMAL(10,2),
    venue NVARCHAR(255),
    city NVARCHAR(100),
    total_tickets INT
);
