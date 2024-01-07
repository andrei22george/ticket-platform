-- Assuming you already have the Ticket table created

-- Insert 10 records
INSERT INTO Ticket (userid, adminid, eventid, qrcode)
VALUES
(1, 2, 3, 'QRCode1'),
(4, 5, 6, 'QRCode2'),
(7, 8, 9, 'QRCode3'),
(2, 3, 4, 'QRCode4'),
(5, 6, 7, 'QRCode5'),
(8, 9, 10, 'QRCode6'),
(3, 4, 5, 'QRCode7'),
(6, 7, 8, 'QRCode8'),
(9, 10, 1, 'QRCode9'),
(10, 1, 2, 'QRCode10');
