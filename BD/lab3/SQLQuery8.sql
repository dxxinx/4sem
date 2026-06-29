SELECT * FROM Товары;
SELECT Наименование, Цена FROM Товары;
SELECT count(*) From Товары; 
UPDATE Товары
SET Цена = Цена * 1.10
WHERE Наименование = 'Компьютер';
SELECT * FROM Товары WHERE Наименование = 'Компьютер';