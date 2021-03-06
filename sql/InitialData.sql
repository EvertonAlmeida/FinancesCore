USE [FinancesCore]
GO
INSERT [dbo].[Categories] ([Id], [CreatedAt], [UpdatedAt], [Title]) VALUES (N'6e0dce79-881a-4eeb-94bf-08d926a672a1', CAST(N'2021-06-03T12:44:37.4978016' AS DateTime2), CAST(N'2021-06-03T15:49:01.5956739' AS DateTime2), N'Services')
INSERT [dbo].[Categories] ([Id], [CreatedAt], [UpdatedAt], [Title]) VALUES (N'134b5672-9ba0-4ee3-94c1-08d926a672a1', CAST(N'2021-06-03T12:45:08.8949262' AS DateTime2), NULL, N'Books')
INSERT [dbo].[Categories] ([Id], [CreatedAt], [UpdatedAt], [Title]) VALUES (N'fd05d433-eed0-4eb1-257a-08d929e85ac2', CAST(N'2021-06-07T16:13:38.1471689' AS DateTime2), NULL, N'Sports')
GO
INSERT [dbo].[Transactions] ([Id], [CreatedAt], [UpdatedAt], [Title], [Value], [Type], [Active], [CategoryId]) VALUES (N'adf0b722-46e1-44ac-b9c7-08d929e83e06', CAST(N'2021-06-07T16:12:49.9592122' AS DateTime2), NULL, N'Dot Net core App', CAST(1000.00 AS Decimal(18, 2)), 1, 1, N'6e0dce79-881a-4eeb-94bf-08d926a672a1')
INSERT [dbo].[Transactions] ([Id], [CreatedAt], [UpdatedAt], [Title], [Value], [Type], [Active], [CategoryId]) VALUES (N'5e6f464e-ae08-437d-b9c9-08d929e83e06', CAST(N'2021-06-07T16:17:26.2007387' AS DateTime2), NULL, N'Buiding Microservices with C#', CAST(250.00 AS Decimal(18, 2)), 2, 1, N'134b5672-9ba0-4ee3-94c1-08d926a672a1')
GO
IF EXISTS (Select top 1 Id from [dbo].[AspNetUsers])
BEGIN
  INSERT [dbo].[AspNetUserClaims] ([UserId], [ClaimType], [ClaimValue]) VALUES ((Select top 1 Id from [dbo].[AspNetUsers]), N'Category', N'Add, Edit')
INSERT [dbo].[AspNetUserClaims] ([UserId], [ClaimType], [ClaimValue]) VALUES ((Select top 1 Id from [dbo].[AspNetUsers]), N'Transaction', N'Add, Edit, Delete')
END;
GO