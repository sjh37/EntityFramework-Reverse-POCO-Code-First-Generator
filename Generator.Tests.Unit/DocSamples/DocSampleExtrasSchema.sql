-- The second schema, for the settings the three-table DocSampleSchema.sql cannot show:
-- many-to-many, views, stored procedures, rowversion and extended properties.
--
-- Nothing runs this script. It exists to be read. DocSampleExtrasSchema.xml is the same schema in the
-- shape the efrpg tool would hand over.

CREATE TABLE dbo.Student
(
    StudentId   int           NOT NULL IDENTITY(1, 1),
    StudentName nvarchar(100) NOT NULL,
    CONSTRAINT PK_Student PRIMARY KEY (StudentId)
);

CREATE TABLE dbo.Course
(
    CourseId int           NOT NULL IDENTITY(1, 1),
    Title    nvarchar(200) NOT NULL,
    CONSTRAINT PK_Course PRIMARY KEY (CourseId)
);

-- Nothing but the two foreign keys, which is what makes this a mapping table
CREATE TABLE dbo.StudentCourse
(
    StudentId int NOT NULL,
    CourseId  int NOT NULL,
    CONSTRAINT PK_StudentCourse PRIMARY KEY (StudentId, CourseId),
    CONSTRAINT FK_StudentCourse_Student FOREIGN KEY (StudentId) REFERENCES dbo.Student (StudentId),
    CONSTRAINT FK_StudentCourse_Course  FOREIGN KEY (CourseId)  REFERENCES dbo.Course (CourseId)
);

CREATE TABLE dbo.Document
(
    DocumentId int           NOT NULL IDENTITY(1, 1),
    Title      nvarchar(100) NOT NULL,
    RowVersion rowversion    NOT NULL,
    ReviewedByUserId int      NULL,
    CONSTRAINT PK_Document PRIMARY KEY (DocumentId)
);

EXEC sp_addextendedproperty
    @name = N'MS_Description', @value = N'The title shown to users. Not the file name.',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE',  @level1name = N'Document',
    @level2type = N'COLUMN', @level2name = N'Title';
GO

-- snake_case, so the naming settings have something to change
CREATE TABLE dbo.order_line_item
(
    order_line_item_id int            NOT NULL IDENTITY(1, 1),
    unit_price         decimal(18, 2) NOT NULL,
    qty_ordered        int            NOT NULL,
    CONSTRAINT PK_order_line_item PRIMARY KEY (order_line_item_id)
);
GO

CREATE VIEW dbo.ActiveStudent
AS
    SELECT StudentId, StudentName FROM dbo.Student;
GO

-- Two result sets, which is the only shape UsePropertiesForStoredProcResultSets changes
CREATE PROCEDURE dbo.GetCourseReport @CourseId int
AS
    SELECT CourseId, Title FROM dbo.Course WHERE CourseId = @CourseId;

    SELECT s.StudentId, s.StudentName
    FROM   dbo.Student s
           JOIN dbo.StudentCourse sc ON sc.StudentId = s.StudentId
    WHERE  sc.CourseId = @CourseId;
GO

CREATE PROCEDURE dbo.GetStudentsByCourse @CourseId int
AS
    SELECT s.StudentId, s.StudentName
    FROM   dbo.Student s
           JOIN dbo.StudentCourse sc ON sc.StudentId = s.StudentId
    WHERE  sc.CourseId = @CourseId;
GO
