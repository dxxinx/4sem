USE UNIVER;
GO

CREATE TABLE ТОВАРЫ  
(  
    Наименование nvarchar(20) primary key,  
    Цена real NOT NULL,
    Количество int CHECK (Количество >= 0)
) 
ON FG1; 
GO

CREATE TABLE Заказчики
(   
    Наименование_фирмы nvarchar(20) primary key,
    Адрес nvarchar(50),
    Расчетный_счет nvarchar(20) UNIQUE
)
ON FG1;  
GO

CREATE TABLE Заказы
(   
    Номер_заказа int primary key,
    Наименование_товара nvarchar(20) foreign key references ТОВАРЫ(Наименование),
    Цена_продажи real,
    Количество int DEFAULT 1,
    Дата_поставки date,
    Заказчик nvarchar(20) foreign key references Заказчики(Наименование_фирмы)
)
ON FG1;  
GO
