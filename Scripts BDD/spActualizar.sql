CREATE PROCEDURE spActualizar
    @Id INT,
    @Nombre VARCHAR(255),
    @Descripcion VARCHAR(255)
AS
BEGIN
    BEGIN TRY
        UPDATE BdiExamen.dbo.tbIExamen
        SET Nombre = @Nombre,
            Descripcion = @Descripcion
        WHERE idExamen = @Id;
        
        -- Si la actualización se realiza correctamente
        SELECT 0 AS CodigoRetorno, 'Registro actualizado satisfactoriamente' AS Descripcion;
    END TRY
    BEGIN CATCH
        -- Si ocurre algún error, se devuelve el código de error y la descripción del error
        SELECT ERROR_NUMBER() AS CodigoRetorno, ERROR_MESSAGE() AS Descripcion;
    END CATCH
END;
