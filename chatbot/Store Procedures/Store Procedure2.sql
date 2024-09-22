USE Chatbot;
Alter PROC SP_Get_OpcionesChatbot
AS
BEGIN
   SELECT Id,Opcion,Codigo FROM OpcionesChatbot;
END

------------------------------------
GO
ALTER PROC SP_Get_Preguntas_OpcionesChat_Bot(
 @IdOpcionChatbot INT
)
AS
BEGIN 
    SET NOCOUNT ON
	SELECT Id, Pregunta,Codigo FROM PreguntasChatbot WHERE IdOpcionChatbot = @IdOpcionChatbot;
	SELECT Id, Opcion,Codigo FROM OpcionesChatbot  WHERE Id = @IdOpcionChatbot;
END
------------------------------------------------------------------
ALTER TABLE OpcionesChatbot
ADD CONSTRAINT UQ_OpcionesChatbot_Codigo UNIQUE (Codigo);

CREATE TABLE RespuestasGenerales(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Codigo NVARCHAR(255),
    Respuesta NVARCHAR(255) NOT NULL,
    FOREIGN KEY (Codigo) REFERENCES OpcionesChatbot(codigo)
);
GO


INSERT INTO RespuestasGenerales(codigo, Respuesta)VALUES('contact','Si necesitas ponerte en contacto con nosotros, puedes enviarnos un correo electrónico a contacto@ejemplo.com. También puedes llamarnos al número +1 (555) 123-4567. Estaremos encantados de ayudarte con cualquier consulta que tengas.')
INSERT INTO RespuestasGenerales(codigo, Respuesta)VALUES('horario',' Nuestro horario de atención es de lunes a viernes, de 9:00 a 18:00. Estaremos encantados de ayudarte con cualquier consulta que tengas.')
INSERT INTO RespuestasGenerales(codigo, Respuesta)VALUES('tiendas','Puedes encontrarnos en la Tienda Centro, ubicada en Calle Principal 123, Ciudad. Si prefieres, también puedes visitar la Tienda Norte, en Avenida Norte 456, Ciudad. Finalmente, nuestra Tienda Sur se encuentra en Paseo del Sur 789, Ciudad.')
INSERT INTO RespuestasGenerales(codigo, Respuesta)VALUES('acerca','ShoppInApp es una tienda líder en productos diversos, incluyendo Deporte, Tecnología, Belleza, Salud y Ropa. Fundada en 2015, ofrecemos calidad y precios competitivos, todo en un solo lugar. ¡Descubre la comodidad de comprar con nosotros!')

SELECT * FROM  PreguntasChatbot;
UPDATE PreguntasChatbot SET codigo = 'oferta' WHERE Id = 3;
SELECT * FROM OpcionesChatbot;

SELECT * FROM OpcionesChatbot;
SELECT * FROM RespuestasGenerales;
SELECT * FROM Productos