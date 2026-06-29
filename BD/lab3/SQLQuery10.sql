ALTER DATABASE UNIVER
ADD FILEGROUP FG1;
GO

ALTER DATABASE UNIVER
ADD FILE
(
    name = N'UNIVER_FG1_Data',
    filename = N'C:\4_sem\BD\lab3\UNIVER_FG1_Data.ndf', 
    size = 5120Kb,
    maxsize = 512MB,
    filegrowth = 10%
)
TO FILEGROUP FG1;
GO