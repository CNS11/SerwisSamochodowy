CREATE TRIGGER [dbo].[stanymagTRIGG] 
   ON  [dbo].[ZuzyteCzesci]
   AFTER INSERT
AS 
BEGIN
  SET NOCOUNT ON;
  DECLARE @Idczesci int
  DECLARE @StanMag int
  DECLARE @Ilosc int

  select @Idczesci= i.IdCzesci from inserted i
  select @Ilosc= i.ilosc from inserted i
  UPDATE [dbo].[Magazyn]
SET StanMagazynowy = StanMagazynowy-@Ilosc
WHERE IdCzesci = @Idczesci;
SET NOCOUNT ON;
  END