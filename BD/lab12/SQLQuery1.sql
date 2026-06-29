-----1
use UNIVER
set nocount on
if exists( select * from SYS.objects where OBJECT_ID = object_id (N'dbo.firstTable'))
drop table firstTable;

declare @c int, @flag char = 'c';
SET IMPLICIT_TRANSACTIONS ON
Create table firstTable(K int);
Insert firstTable values (1), (2), (3);
set @c = (select count(*) from firstTable);
print 'количество строк в таблице firstTable:' + cast (@c as varchar(2));
if @flag = 'c' commit;
else rollback ;
SET IMPLICIT_TRANSACTIONS OFF

if exists (select * from SYS.objects where OBJECT_ID = object_id(N'dbo.firstTable'))
print 'таблица firstTable есть';
else print 'таблицы firstTable нет';

select * from firstTable;

-----2
begin try
 begin tran
 insert AUDITORIUM values (N'1-1000', N'лк', 1, N'1-1');
 insert AUDITORIUM values (N'1-1000', N'лк', N'лк', N'1-1');
   commit tran;
end try
begin catch
 print N'ошибка:'+case
 when error_number() = 2627 and patindex('%AUDITORIUM_PK%', error_message()) >0
 then N'дублирование факультета'
 else N'неизвестная ошибка ' + cast(error_number() as varchar(5)) + error_message()
 end;
 if @@TRANCOUNT>0 rollback tran;
end catch;

-----3
declare @point nvarchar(32);
begin try
 begin tran
   insert AUDITORIUM values (N'2-7000000', N'лк', 1, N'1-1');
  set @point='p1'; save tran @point;
   insert AUDITORIUM values (N'2-6', N'лк', 1, N'1-1');
  set @point='p2' ; save tran @point;
   insert AUDITORIUM values (N'1-5', N'лк', 1, N'1-1');
 commit tran;
end try

begin catch
print 'ошибка:' + case when error_number() = 2627 and patindex('%AUDITORIUM_PK%', error_message()) > 0
then 'дублирование'
else 'неизвестная ошибка:' + cast(error_number() as varchar(5)) + error_message()
end
if @@trancount>0
 begin 
   print 'контрольная точка:'+@point;
   rollback tran @point;
--   commit tran;
 end;
end catch;

-- 4: READ UNCOMMITTED (Грязное чтение)

-- А:
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
BEGIN TRANSACTION;
 SELECT * FROM SUBJECT WHERE SUBJECT = 'ОАиП'; 
  -------------------------- t1 ------------------
  SELECT * FROM SUBJECT WHERE SUBJECT = 'ОАиП'; 
  -------------------------- t2 -----------------
COMMIT;

-- Б:
BEGIN TRANSACTION;
  UPDATE SUBJECT SET SUBJECT_NAME = 'ТЕСТ' WHERE SUBJECT = 'ОАиП';
  SELECT @@SPID as SPID, 'Изменено, но не зафиксировано' as Результат;
  -------------------------- t1 --------------------
  -------------------------- t2 --------------------
ROLLBACK; 
SELECT * FROM SUBJECT WHERE SUBJECT = 'ОАиП'; 


--5: READ COMMITTED (Защита от грязного чтения)

-- А:
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
BEGIN TRANSACTION;
  -------------------------- t1 ------------------
SELECT * FROM SUBJECT WHERE SUBJECT = 'ОАиП'; 
  -------------------------- t2 -----------------
SELECT * FROM SUBJECT WHERE SUBJECT = 'ОАиП'; 
COMMIT;

-- Б:
BEGIN TRANSACTION;
  UPDATE SUBJECT SET SUBJECT_NAME = 'ОАиП' WHERE SUBJECT = 'ОАиП';
  -------------------------- t1 --------------------
  COMMIT; 
  -------------------------- t2 --------------------

-- 6: REPEATABLE READ (Защита от неповторяющегося чтения)

-- А:
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ;
BEGIN TRANSACTION;
  SELECT * FROM FACULTY WHERE FACULTY = 'ИТ'; 
  -------------------------- t1 ------------------ 
  -------------------------- t2 -----------------
  SELECT * FROM FACULTY WHERE FACULTY = 'ИТ'; 
COMMIT;

-- Б: 
BEGIN TRANSACTION;
  -------------------------- t1 --------------------
  UPDATE FACULTY SET FACULTY_NAME = 'Информ. технологии' WHERE FACULTY = 'ИТ';
  COMMIT;
  -------------------------- t2 --------------------
--7: SERIALIZABLE (Полная изоляция)

-- А:
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
BEGIN TRANSACTION;
  SELECT * FROM STUDENT WHERE IDGROUP = 1;
  -------------------------- t1 -----------------
  -------------------------- t2 ------------------ 
  SELECT * FROM STUDENT WHERE IDGROUP = 1;
COMMIT;

-- Б:
BEGIN TRANSACTION;
  -------------------------- t1 --------------------
  INSERT INTO STUDENT (IDGROUP, NAME, BDAY) VALUES (1, 'Иван Фантомов', '2005-01-01');
  COMMIT; 
  -------------------------- t2 --------------------

-----8
select * from PULPIT

begin tran
update PULPIT set PULPIT_NAME = 'иииииииии тттттттт' where PULPIT.FACULTY = 'ИТ'
	begin tran 
	update PULPIT set PULPIT_NAME = 'Лес' where PULPIT.FACULTY = 'ИТ'
	commit;
	select * from PULPIT
rollback
select * from PULPIT