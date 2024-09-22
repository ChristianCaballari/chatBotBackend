
USE ChatBot;
-------------------------------------------------
GO
CREATE PROC SP_Get_RespuestasGenerales(
  @Codigo NVARCHAR(255)
)
AS
BEGIN
   SET NOCOUNT ON
   SELECT Id, Codigo, Respuesta FROM RespuestasGenerales
          WHERE codigo = @Codigo;
END
EXEC SP_Get_RespuestasGenerales 'horario'
------------------------------------------
GO
SELECT * FROM Productos;
GO
CREATE PROC SP_Get_Promociones
AS
BEGIN
   SELECT Id,Nombre,Precio AS PrecioOriginal,'10%' AS Descuento,
    Precio * 0.9 AS PrecioConDescuento,foto AS Foto
          FROM Productos WHERE promocion = 1;
END
---------------------------------------------
GO
SELECT * FROM Categorias;
GO
CREATE PROC SP_Get_Categorias
AS
BEGIN
  DECLARE @Categorias NVARCHAR(MAX);
   
    SELECT @Categorias = 'Tenemos categorías como ' + STRING_AGG(Nombre, ', ')
    FROM Categorias;

    SELECT @Categorias AS Categorias;
END;
EXEC SP_Get_Categorias;
