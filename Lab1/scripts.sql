CREATE OR ALTER FUNCTION dbo.udf_GetMaxAppointmentByService (@ServiceName NVARCHAR(100))
RETURNS @ResultTable TABLE (
    MessageOrStatus NVARCHAR(250),
    AppointmentId INT,
    AppointmentDate DATETIME,
    ClientName NVARCHAR(100)
)
AS
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM dbo.AppointmentDetails ad
        JOIN dbo.Services s ON ad.ServiceId = s.Id
        WHERE s.Name = @ServiceName
    )
    BEGIN
        INSERT INTO @ResultTable (MessageOrStatus)
        VALUES (CONCAT(N'Відсутні замовлення товару «', @ServiceName, N'»'));
    END
    ELSE
    BEGIN
        WITH ServiceAppointments AS (
            SELECT ad.AppointmentId, a.AppointmentDate, c.FullName AS ClientName,
                   SUM(s2.Price) OVER(PARTITION BY ad.AppointmentId) as TotalAppPrice
            FROM dbo.AppointmentDetails ad
            JOIN dbo.Services s ON ad.ServiceId = s.Id
            JOIN dbo.Appointments a ON ad.AppointmentId = a.Id
            JOIN dbo.Clients c ON a.ClientId = c.Id
            JOIN dbo.AppointmentDetails ad2 ON a.Id = ad2.AppointmentId
            JOIN dbo.Services s2 ON ad2.ServiceId = s2.Id
            WHERE s.Name = @ServiceName
        ),
        MaxPrice AS (
            SELECT MAX(TotalAppPrice) as MaxPriceVal FROM ServiceAppointments
        )
        INSERT INTO @ResultTable (MessageOrStatus, AppointmentId, AppointmentDate, ClientName)
        SELECT N'Успішно знайдено', AppointmentId, AppointmentDate, ClientName
        FROM ServiceAppointments
        WHERE TotalAppPrice = (SELECT MaxPriceVal FROM MaxPrice);
    END

    RETURN;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_AddServiceToAppointment
    @ServiceName NVARCHAR(100),
    @AppointmentId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ServiceId INT;
    DECLARE @FinalAppointmentId INT = @AppointmentId;

    SELECT TOP 1 @ServiceId = Id 
    FROM dbo.Services 
    WHERE Name = @ServiceName 
    ORDER BY Name ASC;

    IF @ServiceId IS NULL
    BEGIN
        RAISERROR(N'Послугу з такою назвою не знайдено!', 16, 1);
        RETURN;
    END;

    IF @FinalAppointmentId IS NULL
    BEGIN
        SELECT @FinalAppointmentId = ISNULL(MAX(Id), 0) + 1 FROM dbo.Appointments;
        
        INSERT INTO dbo.Appointments (ClientId, MasterId, AppointmentDate, Status)
        VALUES (1, 1, GETDATE(), N'Очікується');
        
        SET @FinalAppointmentId = SCOPE_IDENTITY();
    END;

    INSERT INTO dbo.AppointmentDetails (AppointmentId, ServiceId)
    VALUES (@FinalAppointmentId, @ServiceId);

    PRINT N'Послугу успішно додано до візиту №' + CAST(@FinalAppointmentId AS NVARCHAR(10));
END;
GO

CREATE OR ALTER FUNCTION dbo.udf_GetEverySecondServiceByMaster (@MasterId INT)
RETURNS TABLE
AS
RETURN (
    WITH RankedDetails AS (
        SELECT ad.Id AS DetailId, s.Name AS ServiceName, a.AppointmentDate,
               ROW_NUMBER() OVER (ORDER BY a.AppointmentDate) AS RowNum
        FROM dbo.AppointmentDetails ad
        JOIN dbo.Services s ON ad.ServiceId = s.Id
        JOIN dbo.Appointments a ON ad.AppointmentId = a.Id
        WHERE a.MasterId = @MasterId
    )
    SELECT DetailId, ServiceName, AppointmentDate
    FROM RankedDetails
    WHERE RowNum % 2 = 0
);
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Masters]') AND name = N'Information')
BEGIN
    ALTER TABLE dbo.Masters ADD Information NVARCHAR(250) NULL;
END;
GO

CREATE OR ALTER TRIGGER dbo.tr_CheckMasterServicesCount
ON dbo.AppointmentDetails
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InsertedAppointmentId INT;
    DECLARE @MasterId INT;
    DECLARE @TotalServicesCount INT;

    DECLARE inserted_cursor CURSOR FOR 
    SELECT AppointmentId FROM inserted;

    OPEN inserted_cursor;
    FETCH NEXT FROM inserted_cursor INTO @InsertedAppointmentId;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT @MasterId = MasterId FROM dbo.Appointments WHERE Id = @InsertedAppointmentId;
SELECT @TotalServicesCount = COUNT(DISTINCT ad.ServiceId)
        FROM dbo.AppointmentDetails ad
        JOIN dbo.Appointments a ON ad.AppointmentId = a.Id
        WHERE a.MasterId = @MasterId;

        IF @TotalServicesCount >= 10
        BEGIN
            UPDATE dbo.Masters
            SET Information = N'Майстер надав більше 10 одиниць різних послуг'
            WHERE Id = @MasterId AND (Information IS NULL OR Information NOT LIKE N'%більше 10%');
        END;

        FETCH NEXT FROM inserted_cursor INTO @InsertedAppointmentId;
    END;

    CLOSE inserted_cursor;
    DEALLOCATE inserted_cursor;
END;
GO

IF OBJECT_ID('dbo.ClientsLogs', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ClientsLogs (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ClientId INT NOT NULL,
        LogMessage NVARCHAR(250),
        ModifyDate DATETIME DEFAULT GETDATE()
    );
END;
GO

CREATE OR ALTER TRIGGER dbo.tr_ClientsDeleteLog
ON dbo.Clients
AFTER DELETE
AS
BEGIN
    INSERT INTO dbo.ClientsLogs (ClientId, LogMessage)
    SELECT Id, CONCAT(N'Клієнта «', FullName, N'» було видалено з бази.')
    FROM deleted;
END;
GO
