CREATE PROCEDURE [dbo].[AddStudentEnrolmentConfirmationEmailToQueue]
	@studentId int,
	@subjectId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @sender NVARCHAR(MAX)
	set @sender='dean@derpuniversity.example.com'

	declare @recipient NVARCHAR(max)
	set @recipient = (SELECT EmailAddress FROM Student WHERE Id = @studentId)

	declare @studentName NVARCHAR(MAX)
	set @studentName = (SELECT FirstName FROM Student WHERE Id = @studentId)

	declare @subjectCode NVARCHAR(10)
	set @subjectCode = (SELECT Code FROM [dbo].[Subject] WHERE Id = @subjectId)

	declare @body nvarchar(max)
	set @body = 'Dear ' +  @studentName + ',\n\n You have successfully enrolled in ' + @subjectCode  + '.\n\nBest of luck!\n\nSincerely,\nDean'

	INSERT INTO Email (Sender, Recipient, [Subject], Body) VALUES (@sender, @recipient, 'Enrolment confirmation', @body)

END


GO
/****** Object:  Table [dbo].[Email]    Script Date: 16/04/2013 11:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Email](
	[Sender] [nvarchar](100) NOT NULL,
	[Recipient] [nvarchar](100) NOT NULL,
	[Subject] [nvarchar](100) NOT NULL,
	[Body] [nvarchar](100) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Student]    Script Date: 16/04/2013 11:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Username] [nvarchar](8) NOT NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StudentSubjectEnrolment]    Script Date: 16/04/2013 11:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentSubjectEnrolment](
	[StudentId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
 CONSTRAINT [PK_StudentSubjectEnrolment] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC,
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subject]    Script Date: 16/04/2013 11:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[MaxStudents] [int] NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[StudentSubjectEnrolment]  WITH CHECK ADD  CONSTRAINT [FK_StudentSubjectEnrolment_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[StudentSubjectEnrolment] CHECK CONSTRAINT [FK_StudentSubjectEnrolment_Student]
GO
ALTER TABLE [dbo].[StudentSubjectEnrolment]  WITH CHECK ADD  CONSTRAINT [FK_StudentSubjectEnrolment_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([Id])
GO
ALTER TABLE [dbo].[StudentSubjectEnrolment] CHECK CONSTRAINT [FK_StudentSubjectEnrolment_Subject]
GO
/****** Object:  Trigger [dbo].[StudentEnrolledInSubject]    Script Date: 16/04/2013 11:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[StudentEnrolledInSubject]
ON [dbo].[StudentSubjectEnrolment]
AFTER INSERT
AS 
 BEGIN
 declare @stuid int
 declare @subid int
SELECT @stuid = StudentId FROM inserted
SELECT @subid = SubjectId FROM inserted


 EXEC

  [dbo].[AddStudentEnrolmentConfirmationEmailToQueue]
		@studentId = @stuid,
		@subjectId = @subid
END
GO
