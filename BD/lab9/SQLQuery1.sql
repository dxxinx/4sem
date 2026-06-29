-----------1-------------
declare @a char = 'D', 
        @b varchar(6)= 'Diana', 
		@c datetime=getdate(), 
		@d time, 
		@e int, 
		@f smallint, 
		@g tinyint, 
		@h numeric(12,5);
set @d='15:00';
set @e=1;
select @f= 1, @g=1, @h = 3.1415692;
select @a a, @b b, @c c , @d d;
print 'e='+ cast(@e as varchar (10))
print 'f='+ cast(@f as varchar (10))
print 'g='+ cast(@g as varchar (10))
print 'h='+ cast(@h as varchar (10))

-----------2-------------
use UNIVER
declare @a1 int, @b1 int,@c1 int,@d1 int, @o int=(select cast(sum(AUDITORIUM.AUDITORIUM_CAPACITY)as int) from AUDITORIUM)
if @o>200
begin 
select @a1=(select cast(count(*)as int) from AUDITORIUM),
@b1=(select cast(avg(AUDITORIUM_CAPACITY) as numeric(8,3)) from AUDITORIUM)
set @c1= (select cast(count(*)as int )from AUDITORIUM where AUDITORIUM_CAPACITY<@b1)
set @d1= (@c1*100)/@b1
select  @a1 'количество аудиторий', @b1 'средняя вместимость', @c1 'кол-во ауд. < ср вместимость', @d1 '%'
end
else print 'размер общей вместимости:'+ cast(@o as varchar(4))

-----------3-------------
select 
@@ROWCOUNT 'число обработанных строк',
@@VERSION 'версия SQL Server',
@@SPID 'системный идентификатор процесса',
@@ERROR 'код последней ошибки', 
@@SERVERNAME 'имя сервера',
@@TRANCOUNT 'уровень вложенности транзакции',
@@FETCH_STATUS 'результат считывания строк',
@@NESTLEVEL 'уровень вложенности текущей процедуры'

-----------4-------------
--1
declare @z numeric(6,3), @t int=3, @x int=3
if @t>@x 
begin 

set @z=power(sin(@t),2);
select @z 'z='
end
else if @t<@x 
begin
set @z=4*(@t+@x)
select  @z 'z='
end
else if @t=@x 
begin
set @z=1-exp(@X-2) ;
select @z 'z='
end

--2
DECLARE @FIO VARCHAR(30) = 'Воронович Диана Игоревна';
DECLARE @LastName VARCHAR(30) = SUBSTRING(@FIO, 1, CHARINDEX(' ', @FIO) - 1);
DECLARE @FirstName CHAR(1) = SUBSTRING(@FIO, CHARINDEX(' ', @FIO) + 1, 1);
DECLARE @Fatherhood CHAR(1) = SUBSTRING(@FIO, CHARINDEX(' ', @FIO, CHARINDEX(' ', @FIO) + 1) + 1, 1);
SELECT @LastName + ' ' + @FirstName + '. ' + @Fatherhood + '.' AS 'фио';
--3
declare 
@years int = year(getdate()),
@dec int = month(getdate()) + 1;
select STUDENT.NAME, BDAY , @years - year(bday) as N'Возраст'
from STUDENT
where month(BDAY) = @dec;

--4
DECLARE @groupNumber INT = 5;

SELECT PROGRESS.PDATE AS 'Дата проведения экзамена',
       DATENAME(WEEKDAY, PROGRESS.PDATE) AS 'День недели'
FROM GROUPS
INNER JOIN STUDENT ON STUDENT.IDGROUP = GROUPS.IDGROUP
INNER JOIN PROGRESS ON STUDENT.IDSTUDENT = PROGRESS.IDSTUDENT
WHERE GROUPS.IDGROUP = @groupNumber
  AND PROGRESS.SUBJECT = 'СУБД';

-----------5-------------
declare @note int = (select NOTE from PROGRESS where IDSTUDENT='1005')
if @note>6
begin
select PROGRESS.SUBJECT ,PROGRESS.NOTE,PROGRESS.IDSTUDENT
from PROGRESS
end
else 
begin
select @note 'оценки'
end

-----------6------------- 
select case when NOTE BETWEEN 8 AND 10 then '8-10'
					  when NOTE BETWEEN 6 AND 7 then '6-7'
					  when NOTE BETWEEN 4 AND 5 then '4-5'
					  ELSE '-'
					  END [Оценки], COUNT(*) [Количество]
	from PROGRESS GROUP BY case
		when NOTE BETWEEN 8 AND 10 then '8-10'
		when NOTE BETWEEN 6 AND 7 then '6-7'
		when NOTE BETWEEN 4 AND 5 then '4-5' 
		ELSE '-'
		END 

-----------7------------- 
create table #temporarytable32
(
tname varchar(10),
tnote int,
age int
)
declare @i int=0
while @i<10
begin
insert #temporarytable32(tname,tnote,age)
values ('Kate', floor(10*rand()), 19)
set @i=@i+1;
end

select * from #temporarytable32

-----------8------------- 
declare @xx int=1
print @xx+1
print @xx+2
return
print @xx+9

-----------9------------- 
begin try
update AUDITORIUM set AUDITORIUM_CAPACITY = 'Строка' where AUDITORIUM_CAPACITY = '15'
end try
begin catch
print ERROR_NUMBER()
print ERROR_MESSAGE()
print ERROR_LINE()
print ERROR_PROCEDURE()
print ERROR_SEVERITY()
print ERROR_STATE()
end catch