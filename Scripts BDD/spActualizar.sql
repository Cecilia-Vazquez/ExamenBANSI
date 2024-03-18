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
        
        -- Si la actualizaci�n se realiza correctamente
        SELECT 0 AS CodigoRetorno, 'Registro actualizado satisfactoriamente' AS Descripcion;
    END TRY
    BEGIN CATCH
        -- Si ocurre alg�n error, se devuelve el c�digo de error y la descripci�n del error
        SELECT ERROR_NUMBER() AS CodigoRetorno, ERROR_MESSAGE() AS Descripcion;
    END CATCH
END;
