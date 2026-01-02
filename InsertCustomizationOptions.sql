-- Insert default customization options into CustomizationOptions table
-- Run this in SQL Server Management Studio

USE DrCoffeeDB;
GO

-- Insert default customization options
INSERT INTO [dbo].[CustomizationOptions] ([OptionCode], [NameEn], [NameAr], [Price], [IsActive], [DisplayOrder])
VALUES
    ('extra_syrup', 'Extra Syrup', 'سيروب إضافي', 1000.00, 1, 1),
    ('almond_milk', 'Almond Milk', 'حليب اللوز', 1000.00, 1, 2),
    ('skimmed_milk', 'Skimmed Milk', 'حليب خالي الدسم', 1000.00, 1, 3),
    ('extra_espresso', 'Extra Espresso Shot', 'شوت إسبريسو إضافي', 1000.00, 1, 4),
    ('add_cream', 'Add Cream', 'إضافة كريمة', 1000.00, 1, 5);

PRINT 'Customization options inserted successfully.';
GO

-- Verify the data was inserted
SELECT * FROM [dbo].[CustomizationOptions];
GO

