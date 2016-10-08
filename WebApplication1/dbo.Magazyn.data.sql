SET IDENTITY_INSERT [dbo].[Magazyn] ON
INSERT INTO [dbo].[Magazyn] ([IdCzesci], [Nazwa], [Numer_Seryjny], [CenaZakupu], [StanMagazynowy]) VALUES (1, N'Chłodnica', N'0000000001', CAST(150.00 AS Decimal(18, 2)), 8)
INSERT INTO [dbo].[Magazyn] ([IdCzesci], [Nazwa], [Numer_Seryjny], [CenaZakupu], [StanMagazynowy]) VALUES (4, N'Maska', N'0000000002', CAST(130.00 AS Decimal(18, 2)), 10)
INSERT INTO [dbo].[Magazyn] ([IdCzesci], [Nazwa], [Numer_Seryjny], [CenaZakupu], [StanMagazynowy]) VALUES (5, N'Felga', N'0000000003', CAST(50.00 AS Decimal(18, 2)), 4)
INSERT INTO [dbo].[Magazyn] ([IdCzesci], [Nazwa], [Numer_Seryjny], [CenaZakupu], [StanMagazynowy]) VALUES (6, N'Szyba przednia', N'0000000004', CAST(100.00 AS Decimal(18, 2)), 6)
INSERT INTO [dbo].[Magazyn] ([IdCzesci], [Nazwa], [Numer_Seryjny], [CenaZakupu], [StanMagazynowy]) VALUES (7, N'Drzwi prawe', N'0000000005', CAST(200.00 AS Decimal(18, 2)), 9)
INSERT INTO [dbo].[Magazyn] ([IdCzesci], [Nazwa], [Numer_Seryjny], [CenaZakupu], [StanMagazynowy]) VALUES (9, N'Fotel', N'000000020', CAST(200.00 AS Decimal(18, 2)), 11)
INSERT INTO [dbo].[Magazyn] ([IdCzesci], [Nazwa], [Numer_Seryjny], [CenaZakupu], [StanMagazynowy]) VALUES (10, N'Klapa bagażnika', N'0000000011', CAST(117.00 AS Decimal(18, 2)), 4)
SET IDENTITY_INSERT [dbo].[Magazyn] OFF
