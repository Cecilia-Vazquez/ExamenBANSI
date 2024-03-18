CREATE PROCEDURE spAgregar 
  
    @Nombre VARCHAR(255),
    @Descripcion VARCHAR(255)
AS
BEGIN
    BEGIN TRY
	
        INSERT INTO BdiExamen.dbo.tbIExamen(Nombre, Descripcion)
        VALUES ( @Nombre, @Descripcion);
        
        -- Si la inserci�n se realiza correctamente
        SELECT 0 AS CodigoRetorno, 'Registro insertado satisfactoriamente' AS Descripcion;
    END TRY
    BEGIN CATCH
        -- Si ocurre alg�n error, se devuelve el c�digo de error y la descripci�n del error
        SELECT ERROR_NUMBER() AS CodigoRetorno, ERROR_MESSAGE() AS Descripcion;
    END CATCH
END;
