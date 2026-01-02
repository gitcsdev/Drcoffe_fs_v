-- Insert default customization options into CustomizationOptions table
-- Run this in SQL Server Management Studio

USE DrCoffeeDB;
GO

-- Check if table exists
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomizationOptions]') AND type in (N'U'))
BEGIN
    PRINT 'ERROR: CustomizationOptions table does not exist!';
    PRINT 'Please run AddCustomizationOptionsTable.sql first.';
    RETURN;
END
GO

-- Check current row count
DECLARE @RowCount INT;
SELECT @RowCount = COUNT(*) FROM [dbo].[CustomizationOptions];
PRINT 'Current row count: ' + CAST(@RowCount AS VARCHAR(10));
GO

-- Insert default customization options (only if they don't exist)
IF NOT EXISTS (SELECT * FROM [dbo].[CustomizationOptions] WHERE [OptionCode] = 'extra_syrup')
BEGIN
    INSERT INTO [dbo].[CustomizationOptions] ([OptionCode], [NameEn], [NameAr], [Price], [IsActive], [DisplayOrder])
    VALUES ('extra_syrup', 'Extra Syrup', 'سيروب إضافي', 1000.00, 1, 1);
    PRINT 'Inserted: extra_syrup';
END
ELSE
BEGIN
    PRINT 'Skipped: extra_syrup (already exists)';
END
GO

IF NOT EXISTS (SELECT * FROM [dbo].[CustomizationOptions] WHERE [OptionCode] = 'almond_milk')
BEGIN
    INSERT INTO [dbo].[CustomizationOptions] ([OptionCode], [NameEn], [NameAr], [Price], [IsActive], [DisplayOrder])
    VALUES ('almond_milk', 'Almond Milk', 'حليب اللوز', 1000.00, 1, 2);
    PRINT 'Inserted: almond_milk';
END
ELSE
BEGIN
    PRINT 'Skipped: almond_milk (already exists)';
END
GO

IF NOT EXISTS (SELECT * FROM [dbo].[CustomizationOptions] WHERE [OptionCode] = 'skimmed_milk')
BEGIN
    INSERT INTO [dbo].[CustomizationOptions] ([OptionCode], [NameEn], [NameAr], [Price], [IsActive], [DisplayOrder])
    VALUES ('skimmed_milk', 'Skimmed Milk', 'حليب خالي الدسم', 1000.00, 1, 3);
    PRINT 'Inserted: skimmed_milk';
END
ELSE
BEGIN
    PRINT 'Skipped: skimmed_milk (already exists)';
END
GO

IF NOT EXISTS (SELECT * FROM [dbo].[CustomizationOptions] WHERE [OptionCode] = 'extra_espresso')
BEGIN
    INSERT INTO [dbo].[CustomizationOptions] ([OptionCode], [NameEn], [NameAr], [Price], [IsActive], [DisplayOrder])
    VALUES ('extra_espresso', 'Extra Espresso Shot', 'شوت إسبريسو إضافي', 1000.00, 1, 4);
    PRINT 'Inserted: extra_espresso';
END
ELSE
BEGIN
    PRINT 'Skipped: extra_espresso (already exists)';
END
GO

IF NOT EXISTS (SELECT * FROM [dbo].[CustomizationOptions] WHERE [OptionCode] = 'add_cream')
BEGIN
    INSERT INTO [dbo].[CustomizationOptions] ([OptionCode], [NameEn], [NameAr], [Price], [IsActive], [DisplayOrder])
    VALUES ('add_cream', 'Add Cream', 'إضافة كريمة', 1000.00, 1, 5);
    PRINT 'Inserted: add_cream';
END
ELSE
BEGIN
    PRINT 'Skipped: add_cream (already exists)';
END
GO

-- Verify the data was inserted
PRINT '';
PRINT 'Final row count:';
SELECT COUNT(*) AS TotalRows FROM [dbo].[CustomizationOptions];
GO

PRINT '';
PRINT 'All customization options:';
SELECT * FROM [dbo].[CustomizationOptions];
GO

