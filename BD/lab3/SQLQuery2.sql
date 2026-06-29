ALTER TABLE Товары
ADD CONSTRAINT CK_Товары_Цена CHECK (Цена > 0);
GO