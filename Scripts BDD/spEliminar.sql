CREATE PROCEDURE spEliminar
    @Id INT
   
AS
BEGIN
    BEGIN TRY
        DELETE BdiExamen.dbo.tbIExamen       
        WHERE idExamen = @Id;
        
        -- Si la eliminacion se realiza correctamente
        SELECT 0 AS CodigoRetorno, 'Registro eliminado satisfactoriamente' AS Descripcion;
    END TRY
    BEGIN CATCH
        -- Si ocurre alg�n error, se devuelve el c�digo de error y la descripci�n del error
        SELECT ERROR_NUMBER() AS CodigoRetorno, ERROR_MESSAGE() AS Descripcion;
    END CATCH
END;
