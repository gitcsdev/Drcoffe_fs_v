-- Add CustomizationOptions table to DrCoffeeDB
-- Run this script in SQL Server Management Studio

USE DrCoffeeDB;
GO

-- Create CustomizationOptions table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomizationOptions]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[CustomizationOptions] (
        [CustomizationOptionId] INT IDENTITY(1,1) NOT NULL,
        [OptionCode] NVARCHAR(100) NOT NULL,
        [NameEn] NVARCHAR(200) NOT NULL,
        [NameAr] NVARCHAR(200) NOT NULL,
        [Price] DECIMAL(10,2) NOT NULL,
        [IsActive] BIT NOT NULL DEFAULT 1,
        [DisplayOrder] INT NOT NULL DEFAULT 0,
        [CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        [UpdatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT [PK_CustomizationOptions] PRIMARY KEY ([CustomizationOptionId])
    );

    -- Create unique index on OptionCode
    CREATE UNIQUE INDEX [IX_CustomizationOptions_OptionCode] 
    ON [dbo].[CustomizationOptions] ([OptionCode]);

    PRINT 'CustomizationOptions table created successfully.';
END
ELSE
BEGIN
    PRINT 'CustomizationOptions table already exists.';
END
GO

-- Optionally, insert default customization options from menu.json
IF NOT EXISTS (SELECT * FROM [dbo].[CustomizationOptions])
BEGIN
    INSERT INTO [dbo].[CustomizationOptions] ([OptionCode], [NameEn], [NameAr], [Price], [IsActive], [DisplayOrder])
    VALUES
        ('extra_syrup', 'Extra Syrup', 'سيروب إضافي', 1000.00, 1, 1),
        ('almond_milk', 'Almond Milk', 'حليب اللوز', 1000.00, 1, 2),
        ('skimmed_milk', 'Skimmed Milk', 'حليب خالي الدسم', 1000.00, 1, 3),
        ('extra_espresso', 'Extra Espresso Shot', 'شوت إسبريسو إضافي', 1000.00, 1, 4),
        ('add_cream', 'Add Cream', 'إضافة كريمة', 1000.00, 1, 5);

    PRINT 'Default customization options inserted.';
END
ELSE
BEGIN
    PRINT 'Customization options already exist.';
END
GO

