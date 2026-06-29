use UNIVER
--1-2
select AUDITORIUM_TYPE.AUDITORIUM_TYPE,
max (AUDITORIUM_CAPACITY)[Максимальная вместимость],
min (AUDITORIUM_CAPACITY)[Минимальная вместимость],
avg (AUDITORIUM_CAPACITY)[Средняя вместимость],
sum (AUDITORIUM_CAPACITY)[Сумарная вместимость],
count (AUDITORIUM_CAPACITY)[Общее кол-во аудиторий]
from AUDITORIUM inner join AUDITORIUM_TYPE
on AUDITORIUM.AUDITORIUM_TYPE = AUDITORIUM_TYPE.AUDITORIUM_TYPE
group by AUDITORIUM_TYPE.AUDITORIUM_TYPE

--3
select *
	from (select case when NOTE BETWEEN 8 AND 10 then '8-10'
					  when NOTE BETWEEN 6 AND 7 then '6-7'
					  when NOTE BETWEEN 4 AND 5 then '4-5'
					  ELSE '-'
					  END [Оценки], COUNT(*) [Количество]
	from PROGRESS GROUP BY case
		when NOTE BETWEEN 8 AND 10 then '8-10'
		when NOTE BETWEEN 6 AND 7 then '6-7'
		when NOTE BETWEEN 4 AND 5 then '4-5' 
		ELSE '-'
		END ) AS T
			ORDER BY [Оценки] desc

--4
select GROUPS.FACULTY, GROUPS.PROFESSION, GROUPS.IDGROUP,
	round(avg(cast(PROGRESS.NOTE as float(1))),2) as [средн] from FACULTY
inner join GROUPS on GROUPS.FACULTY = FACULTY.FACULTY
inner join STUDENT on STUDENT.IDGROUP = GROUPS.IDGROUP
inner join PROGRESS on PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT
group by GROUPS.FACULTY, GROUPS.PROFESSION, GROUPS.IDGROUP
ORDER BY [средн] desc

--5
select GROUPS.FACULTY, GROUPS.PROFESSION, GROUPS.IDGROUP,
round(avg(cast(PROGRESS.NOTE as float(4))),2) as [средн] from FACULTY
inner join GROUPS on GROUPS.FACULTY = FACULTY.FACULTY
inner join STUDENT on STUDENT.IDGROUP = GROUPS.IDGROUP
inner join PROGRESS on PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT
where PROGRESS.SUBJECT = N'БД' or PROGRESS.SUBJECT = N'ОАиП'
group by GROUPS.FACULTY, GROUPS.PROFESSION, GROUPS.IDGROUP
ORDER BY [средн] desc

--6
select GROUPS.PROFESSION[Специальность], PROGRESS.SUBJECT[Дисциплина],
round(avg(cast(PROGRESS.NOTE as float)), 2) as [Средняя оценка]
from FACULTY
inner join GROUPS on FACULTY.FACULTY = GROUPS.FACULTY
inner join STUDENT on STUDENT.IDGROUP = GROUPS.IDGROUP
inner join PROGRESS on PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT
where FACULTY.FACULTY = 'ТОВ'
group by GROUPS.PROFESSION, PROGRESS.SUBJECT

--7
select SUBJECT, count(NOTE)[количество оценок]
	from PROGRESS
	group by SUBJECT, 
	NOTE having PROGRESS.NOTE = 8 or PROGRESS.NOTE = 9