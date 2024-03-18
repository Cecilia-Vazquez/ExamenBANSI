CREATE PROCEDURE spConsultar

    @Nombre VARCHAR(255),
    @Descripcion VARCHAR(255)
AS
BEGIN   
--Retorna la informacion que cumple con los criterios
        SELECT * FROM BdiExamen.dbo.tbIExamen
        WHERE Nombre = @Nombre AND Descripcion = @Descripcion
END;

