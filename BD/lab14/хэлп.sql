use univer
go

--1
create function f1 (@s nvarchar(20))
returns table
as return select note from progress where subject = @s
go
select * from dbo.f1('ОАиП')

--2
create function f2(@k nvarchar(10))
returns table
as return (select teacher_name, gender from teacher where pulpit = @k)
go

select * from dbo.f2('ИСиТ')

--3
create function f3(@id int) returns nvarchar(300)
as begin
declare c1 cursor local
for select
name from student where idgroup = @id
declare @s nvarchar(100)
declare @res nvarchar(300) = ' '
open c1
fetch c1 into @s
while @@FETCH_STATUS = 0     
begin
set @res = @res + rtrim(@s) + ', '
fetch c1 into @s
end
close c1
deallocate c1
return @res
end
go

select dbo.f3(2)

drop function f4

--4
create function f4(@f nvarchar(10))
returns int
as begin
return (select count(*) from GROUPS where faculty = @f)
end
go

select dbo.f4('ИЭФ')


--5
create function f5(@p nvarchar(10))
returns int
as begin
return(select count(*) from subject where pulpit = @p)
end
go

select dbo.f5('ИСиТ')


--6
create function f6(@id int)
returns table
as return (select name, bday from student where idgroup = @id)
go

select * from dbo.f6(2)

--7 функцию, которая выводит кол-во студентов сдавших экзамен по предмету, предмет в параметре
create function f7 (@sub nvarchar(50))
returns table
as return (select count(*) idstudent from progress where subject = @sub)
go

select * from dbo.f7('ОАиП')

--8
