UPDATE Venues 
SET ImageUrl = REPLACE(ImageUrl, 'https://127.0.0.1', 'http://127.0.0.1')
WHERE ImageUrl LIKE 'https://127.0.0.1%'