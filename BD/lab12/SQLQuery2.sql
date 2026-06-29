use UNIVER
--4 Сессия Б:
BEGIN TRANSACTION;
  UPDATE SUBJECT SET SUBJECT_NAME = 'ТЕСТ' WHERE SUBJECT = 'ОАиП';
  SELECT * FROM SUBJECT WHERE SUBJECT = 'ОАиП'; 

  -------------------------- t1 --------------------
  -------------------------- t2 --------------------
ROLLBACK; 
SELECT * FROM SUBJECT WHERE SUBJECT = 'ОАиП'; 
--5 Сессия Б
BEGIN TRANSACTION;
  UPDATE SUBJECT SET SUBJECT_NAME = 'НОВОЕ НАЗВАНИЕ' WHERE SUBJECT = 'ОАиП';
  -------------------------- t1 --------------------
  COMMIT; 
  -------------------------- t2 --------------------
--6 Сессия Б
BEGIN TRANSACTION;
  -------------------------- t1 --------------------
  UPDATE FACULTY SET FACULTY_NAME = 'Информ. технологии' WHERE FACULTY = 'ИТ';
  COMMIT;
  -------------------------- t2 --------------------
--7 Сессия Б
BEGIN TRANSACTION;
  -------------------------- t1 --------------------
  INSERT INTO STUDENT (IDGROUP, NAME, BDAY) VALUES (1, 'Иван Фантомов', '2005-01-01');
  COMMIT; 
  -------------------------- t2 --------------------
