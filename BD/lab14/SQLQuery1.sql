USE UNIVER;
GO
---1
ALTER FUNCTION dbo.COUNT_STUDENTS(
    @faculty VARCHAR(20) = NULL,
    @prof VARCHAR(20) = NULL
)
RETURNS INT
AS
BEGIN
    DECLARE @cnt INT;

    SELECT @cnt = COUNT(*)
    FROM FACULTY F
    JOIN GROUPS G ON G.FACULTY = F.FACULTY
    JOIN STUDENT S ON S.IDGROUP = G.IDGROUP
    WHERE (F.FACULTY = @faculty OR @faculty IS NULL)
      AND (S.PROFESSION = @prof OR @prof IS NULL);

    RETURN @cnt;
END;
GO

--2
ALTER FUNCTION dbo.COUNT_PULPITS(@faculty VARCHAR(20))
RETURNS INT
AS
BEGIN
    RETURN (
        SELECT COUNT(*) FROM PULPIT WHERE FACULTY = @faculty
    );
END;
GO

--3
ALTER FUNCTION dbo.COUNT_GROUPS(@faculty VARCHAR(20))
RETURNS INT
AS
BEGIN
    RETURN (
        SELECT COUNT(*) FROM GROUPS WHERE FACULTY = @faculty
    );
END;
GO

---4
ALTER FUNCTION dbo.COUNT_PROFESSIONS(@faculty VARCHAR(20))
RETURNS INT
AS
BEGIN
    -- Считаем количество записей в таблице PROFESSION для данного факультета
    RETURN (
        SELECT COUNT(*) FROM PROFESSION WHERE FACULTY = @faculty
    );
END;
GO

---5
ALTER FUNCTION dbo.FACULTY_REPORT(@c INT)
RETURNS @fr TABLE (
    [Факультет] VARCHAR(50),
    [Количество кафедр] INT,
    [Количество групп] INT,
    [Количество студентов] INT,
    [Количество специальностей] INT
)
AS
BEGIN
    -- Объявляем курсор для перебора факультетов,
    -- где количество студентов > @c
    DECLARE cc CURSOR STATIC FOR
        SELECT FACULTY FROM FACULTY
        WHERE dbo.COUNT_STUDENTS(FACULTY, DEFAULT) > @c;

    DECLARE @f VARCHAR(30);

    OPEN cc;
    FETCH cc INTO @f;

    -- Для каждого факультета формируем и вставляем строку с данными в возвращаемую таблицу
    WHILE @@FETCH_STATUS = 0
    BEGIN
        INSERT INTO @fr
        VALUES (
            @f,
            dbo.COUNT_PULPITS(@f),           -- количество кафедр
            dbo.COUNT_GROUPS(@f),            -- количество групп
            dbo.COUNT_STUDENTS(@f, DEFAULT),-- количество студентов
            dbo.COUNT_PROFESSIONS(@f)        -- количество специальностей
        );
        FETCH cc INTO @f;
    END;

    CLOSE cc;
    DEALLOCATE cc;

    RETURN;
END;
GO