CREATE TABLE [dbo].[tbl_employees] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [first_name] VARCHAR (50) NULL,
    [last_name]  VARCHAR (50) NULL,
    [job_title]  VARCHAR (50) NULL,
    [department] VARCHAR (50) NULL,
    [username]   VARCHAR (50) NULL,
    [password]   VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC));
	
	