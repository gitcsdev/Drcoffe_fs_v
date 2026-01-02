-- Check CustomizationOptions table structure
-- Run this to verify the table exists and has the correct columns

USE DrCoffeeDB;
GO

-- Check if table exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomizationOptions]') AND type in (N'U'))
BEGIN
    PRINT '✓ CustomizationOptions table exists';
    
    -- Show table structure
    PRINT '';
    PRINT 'Table Structure:';
    SELECT 
        COLUMN_NAME,
        DATA_TYPE,
        CHARACTER_MAXIMUM_LENGTH,
        IS_NULLABLE,
        COLUMN_DEFAULT
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'CustomizationOptions'
    ORDER BY ORDINAL_POSITION;
    
    -- Show row count
    DECLARE @RowCount INT;
    SELECT @RowCount = COUNT(*) FROM [dbo].[CustomizationOptions];
    PRINT '';
    PRINT 'Current row count: ' + CAST(@RowCount AS VARCHAR(10));
    
    -- Show any existing data
    IF @RowCount > 0
    BEGIN
        PRINT '';
        PRINT 'Existing data:';
        SELECT * FROM [dbo].[CustomizationOptions];
    END
    ELSE
    BEGIN
        PRINT '';
        PRINT '⚠ Table is empty - no data found';
    END
END
ELSE
BEGIN
    PRINT '✗ ERROR: CustomizationOptions table does NOT exist!';
    PRINT 'Please run AddCustomizationOptionsTable.sql first.';
END
GO

