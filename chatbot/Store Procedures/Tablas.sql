
CREATE DATABASE ChatBot;

USE ChatBot;

GO;
CREATE TABLE Categorias(
   Id TINYINT NOT NULL IDENTITY(1,1) PRIMARY KEY, -- Clave primaria que identifica de forma única cada categoría
   Nombre NVARCHAR(150) NOT NULL, -- Nombre de la categoría
);
GO
--TABLA PRODUCTOS => coProducto (nIdProduct, cNombProdu, nPrecioProd,nIdCategori).
CREATE TABLE Productos(
   Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, -- Clave primaria que identifica de forma única cada producto y se incrementa automáticamente
   Nombre NVARCHAR(150) NOT NULL, -- Nombre del producto
   Precio Decimal(18,2) NOT NULL, -- Precio del producto con hasta 18 dígitos en total y 2 decimales
   foto NVARCHAR(MAX),
   IdCategoria TINYINT NOT NULL, -- Clave foránea que hace referencia a la categoría del producto
   CONSTRAINT FK_IdCategoria FOREIGN KEY (IdCategoria) REFERENCES Categorias(Id) -- Relación con la tabla coCategoria
);

ALTER TABLE Productos
ADD promocion BIT;

SELECT * FROM Productos;
UPDATE Productos SET promocion = 1 WHERE Id =14



GO;
CREATE PROC SP_Insert_Product(
   @Nombre NVARCHAR(150),
   @Precio Decimal(18,2) ,
   @foto NVARCHAR(MAX),
   @IdCategoria TINYINT
)
AS
BEGIN
   INSERT INTO Productos(Nombre,Precio,foto,IdCategoria)
          VALUES(@Nombre,@Precio,@foto,@IdCategoria)
	SELECT SCOPE_IDENTITY();
END
GO

ALTER PROC SP_Get_Productos
AS
BEGIN
   SET NOCOUNT ON
   SELECT P.Id,P.Nombre,P.Precio,P.foto AS Foto,C.Nombre AS Categoria
          FROM Productos P
		  INNER JOIN Categorias C
		  ON P.IdCategoria = c.Id
END

----------------------------------------------------------------------------
CREATE TABLE Usuario (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Correo NVARCHAR(150) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL
);

CREATE TABLE OpcionesChatbot (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Opcion NVARCHAR(150) NOT NULL
);
ALTER TABLE OpcionesChatbot
ADD codigo NVARCHAR(255);

CREATE TABLE PreguntasChatbot (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Pregunta NVARCHAR(255) NOT NULL,
    IdOpcionChatbot INT,
    FOREIGN KEY (IdOpcionChatbot) REFERENCES OpcionesChatbot(Id)
);
ALTER TABLE PreguntasChatbot
ADD codigo NVARCHAR(50);


CREATE TABLE InteraccionesChatbot (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT,
    IdPregunta INT,
    Fecha DATE DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(Id),
    FOREIGN KEY (IdPregunta) REFERENCES PreguntasChatbot(Id)
);

-----------------------------------------
GO
CREATE PROC Sp_Insert_Usuario(
 @Correo NVARCHAR(150),
 @Nombre NVARCHAR(100) 
)
AS
BEGIN
   SET NOCOUNT ON
   INSERT INTO Usuario(Correo,Nombre)VALUES(@Correo,@Nombre);
   SELECT SCOPE_IDENTITY();
END;
----------------------------------------------
GO
ALTER PROC SP_Insert_OpcionesChatbot(
  @Opcion NVARCHAR(150) ,
  @Codigo NVARCHAR(255)
)
AS
BEGIN
  SET NOCOUNT ON
  INSERT INTO OpcionesChatbot(Opcion,codigo)VALUES(@Opcion,@Codigo);
  SELECT SCOPE_IDENTITY();
END
----------------------------------------
GO
CREATE PROC SP_Insert_PreguntasChatbot(
    @Pregunta NVARCHAR(255),
    @IdOpcionChatbot INT
)
AS
 BEGIN
  SET NOCOUNT ON;
  INSERT INTO PreguntasChatbot(Pregunta,IdOpcionChatbot)VALUES(@Pregunta,@IdOpcionChatbot);
  SELECT SCOPE_IDENTITY();
 END
 ------------------------------------------------------------------
 GO
 CREATE PROC sp_Insert_InteraccionesChatbot(
    @IdUsuario INT,
    @IdPregunta INT
 )
 AS
 BEGIN
    INSERT INTO InteraccionesChatbot(IdUsuario,IdPregunta)VALUES(@IdUsuario,@IdPregunta);
	SELECT SCOPE_IDENTITY();
 END
 ----------------------------------------------------------------

SELECT * FROM Categorias;
SELECT * FROM Productos;

DELETE Productos WHERE IdCategoria = 3;
UPDATE Categorias SET Nombre = 'Salud y Belleza' WHERE Id =3;
INSERT INTO Categorias(Nombre)VALUES('Tecnología');
;


EXEC  SP_Insert_OpcionesChatbot 'Quiero Información de productos'; --Con esto responde a una pregunta

EXEC  SP_Insert_OpcionesChatbot '¿Cómo puedo contactarlos?','contact'; --Cuatro
EXEC  SP_Insert_OpcionesChatbot 'Horarios de atención al cliente','horario'; --Cinco
EXEC  SP_Insert_OpcionesChatbot 'Información sobre tiendas físicas', 'tiendas';--Seis
EXEC  SP_Insert_OpcionesChatbot 'Quiero información sobre ShoppInApp', 'acerca';--Seis
EXEC  SP_Insert_OpcionesChatbot 'Quiero hacer', 'generica';--Seis
Información sobre ofertas actuales.


EXEC  SP_Insert_PreguntasChatbot '¿Cuáles son las categorías disponibles?' ,3; --Dos
EXEC  SP_Insert_PreguntasChatbot '¿Cuáles son los productos disponibles?', 3; --Tres
EXEC  SP_Insert_PreguntasChatbot '¿Productos en oferta?', 3; --Tres

SELECT * FROM OpcionesChatbot;
SELECT * FROM PreguntasChatbot;

--DELETE OpcionesChatbot;
SELECT * From Usuario;
UPDATE OpcionesChatbot SET codigo = 'multiple' WHERE Id =3;

SELECT * FROM RespuestasGenerales;
SELECT * FROM OpcionesChatbot;
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



SELECT * FROM Productos WHERE Nombre LIKE '%gorras%';
SELECT * FROM Productos WHERE LOWER(Nombre) LIKE LOWER('%gorras%');
SELECT * FROM Productos WHERE SOUNDEX(LOWER(Nombre)) = SOUNDEX(LOWER('gorras'));

SELECT * FROM Productos 
WHERE LOWER(Nombre) LIKE LOWER(CONCAT('%', REPLACE('medias', 's', ''), '%'));

'gorras|informacion'

UPDATE Productos SET promocion = 0 WHERE Id = 17;



--CREAR UN TYPE
CREATE TYPE ListadoPalabras AS TABLE (Nombre NVARCHAR(150));
GO


ALTER PROC Sp_Busqueda_Generica(
  @Palabras ListadoPalabras READONLY
)
AS
BEGIN
  SET NOCOUNT ON
  SELECT P.Id, P.Nombre, P.Precio,P.foto AS Foto, C.Nombre AS Categoria
   FROM Productos P
   INNER JOIN Categorias C
   ON C.Id = P.IdCategoria
	WHERE EXISTS (
		SELECT 1
		FROM @Palabras Pa
		WHERE LOWER(P.Nombre) LIKE LOWER(CONCAT('%', REPLACE(LOWER(Pa.Nombre),'s',''), '%'))
		)
END;



SELECT * FROM Productos 
WHERE LOWER(Nombre) LIKE LOWER(CONCAT('%', REPLACE('cascos', 's', ''), '%'));
SELECT * FROM Productos

SELECT Nombre FROM Productos 








