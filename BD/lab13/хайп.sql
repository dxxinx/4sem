---1
use UNIVER
create procedure n1 @sub nvarchar(20), @stud int, @note int
as
begin

insert into progress(subject, idstudent, pdate, note)
values(@sub, @stud, getdate(), @note)

if(@note<4)
begin
update student set idgroup = idgroup + 1 where IDSTUDENT = @stud
end
else
print 'good note'
end

begin tran
exec n1 @sub = 'ОАиП', @stud = 1010, @note = 4
select idgroup, idstudent from student where idstudent = 1010
rollback

---2
create procedure n2 @id nvarchar(10), @gender char(1), @prep nvarchar(100), @kaf nvarchar(10)
as
begin
if exists (select * from pulpit where pulpit = @kaf)
begin
insert into TEACHER (teacher, teacher_name, gender,pulpit)
values(@id, @prep, @gender, @kaf)
end
else print 'error'
end

begin tran
exec n2 @id = 'KJK', @prep = 'Kaaa Jopa Kaka', @gender = 'м', @kaf = 'ИСиТ'
select * from teacher where teacher = 'KJK'
rollback

---3
create procedure n3 @aud nvarchar(10), @ac int
as
begin
if @ac < 30
begin
update auditorium set AUDITORIUM_CAPACITY = AUDITORIUM_CAPACITY + 10
end
else print 'not change'
end

begin tran
exec n3 @aud = '423-1', @ac = 90
select * from AUDITORIUM where auditorium = '423-1'
rollback

---4
create procedure n4 @sub nvarchar(10)
as
begin
if exists (select * from progress where subject = @sub)
print 'not delete'
else
begin
delete from subject where subject = @sub
print 'delete'
end
end

begin tran
exec n4 @sub = 'бб'
select * from subject where subject = 'бб'
rollback

---5
create procedure n5 @sub nvarchar(10), @sn nvarchar(50), @pl nvarchar(10)
as
begin
if exists (select * from pulpit where pulpit = @pl)
begin
insert into subject(subject, subject_name, pulpit)
values(@sub, @sn, @pl)
end
else print 'error'
end

begin tran
exec n5 @sub = 'vv', @sn = 'dfgfe', @pl = 'ИСиТ'
select * from subject where pulpit = 'ИСиТ'
rollback

---6
drop procedure n6
create procedure n6 @name nvarchar(10), @idg int
as
begin
if (select count(*) from groups where idgroup = @idg) < 5
begin
insert into student (idgroup, name, bday)
values (@idg, @name, getdate())
end
else print 'not add'
end

begin tran
exec n6 @name = 'Mam', @idg = 3
select * from student where idgroup = 3
rollback

---1.2
drop procedure n8
create procedure n8 @sub nvarchar(20), @nt int, @ids int
as
begin
insert into progress (idstudent, subject, pdate, note)
values (@ids, @sub, getdate(), @nt)
if @nt < 4
begin
update student set idgroup = idgroup +3 where idstudent = @ids
end
else print 'error'
end

begin tran
exec n8 @ids = 1001, @sub = 'ОАиП', @nt = 3
select * from student where idstudent = 1001
rollback

---2.2
drop procedure n9
create procedure n9 @teacher nvarchar(10), @name nvarchar(50), @pl nvarchar(10), @gen char(1)
as
begin
if exists(select*from pulpit where pulpit = @pl)
begin
insert into teacher(teacher, teacher_name, gender, pulpit)
values (@teacher, @name, @gen, @pl)
end
end

begin tran
exec n9 @teacher = 'JJJ', @name = 'fgh uio ggh', @gen = 'м', @pl = 'ИСиТ'
select * from teacher where pulpit = 'ИСиТ'
rollback