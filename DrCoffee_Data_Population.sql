-- =============================================
-- Dr.Coffee Data Population Script
-- Populates database with all menu items from menu.json
-- =============================================

-- Make sure you've run DrCoffee_Database.sql first!

USE DrCoffeeDB; -- Change this to your database name
GO

-- =============================================
-- Clear existing data (Optional - use with caution!)
-- =============================================
-- Uncomment the following lines if you want to clear existing data first
/*
DELETE FROM OrderItemCustomizations;
DELETE FROM OrderItems;
DELETE FROM Orders;
DELETE FROM ProductFlavors;
DELETE FROM ProductPrices;
DELETE FROM ProductTags;
DELETE FROM Products;
DELETE FROM CustomizationOptions;
DELETE FROM Categories;
GO
*/

-- =============================================
-- Insert All Categories
-- =============================================
SET IDENTITY_INSERT Categories ON;

INSERT INTO Categories (CategoryId, Name, DisplayOrder) VALUES
(1, 'Iced Coffee', 1),
(2, 'Iced Tea', 2),
(3, 'Ice-Espresso', 3),
(4, 'Frappuccino', 4),
(5, 'Energy Drinks', 5),
(6, 'Yogurt & Essence', 6),
(7, 'Milkshakes', 7),
(8, 'Protein Shakes', 8),
(9, 'Smoothies', 9),
(10, 'Fresh Juices', 10),
(11, 'Hot Coffee', 11),
(12, 'Specialty', 12),
(13, 'Tea', 13);

SET IDENTITY_INSERT Categories OFF;
GO

-- =============================================
-- Insert All Customization Options
-- =============================================
SET IDENTITY_INSERT CustomizationOptions ON;

INSERT INTO CustomizationOptions (CustomizationOptionId, OptionId, NameEn, NameAr, Price) VALUES
(1, 'extra_syrup', 'Extra Syrup', 'سيروب إضافي', 1000),
(2, 'almond_milk', 'Almond Milk', 'حليب اللوز', 1000),
(3, 'skimmed_milk', 'Skimmed Milk', 'حليب خالي الدسم', 1000),
(4, 'extra_espresso', 'Extra Espresso Shot', 'شوت إسبريسو إضافي', 1000),
(5, 'add_cream', 'Add Cream', 'إضافة كريمة', 1000);

SET IDENTITY_INSERT CustomizationOptions OFF;
GO

-- =============================================
-- Insert All Products
-- =============================================

-- Iced Coffee Products
INSERT INTO Products (ProductCode, NameEn, NameAr, CategoryId, CaffeineIndex, IsCustomizable) VALUES
('iced_mocha', 'Iced Mocha', 'آيس موكا', 1, 5, 1),
('cookies_iced_latte', 'Cookies Iced Latte', 'كوكيز آيس لاتيه', 1, 4, 1),
('iced_latte', 'Iced Latte', 'آيس لاتيه', 1, 4, 1),
('iced_coffee', 'Iced Coffee', 'آيس كوفي', 1, 5, 1),
('iced_spanish_latte', 'Iced Spanish Latte', 'آيس سبانيش لاتيه', 1, 4, 1),
('iced_berry_latte', 'Iced Berry Latte', 'آيس بيري لاتيه', 1, 4, 1),
('chocolate_iced_latte', 'Chocolate Iced Latte', 'جوكلت آيس لاتيه', 1, 4, 1),
('flavoured_iced_latte', 'Flavoured Iced Latte', 'آيس لاتيه بنكهات', 1, 4, 1),
('iced_cappuccino', 'Iced Cappuccino', 'آيس كابتشينو', 1, 5, 1),
('banana_cinnamon', 'Banana Cinnamon', 'بنانا سينابون', 1, 3, 1);

-- Iced Tea Products
INSERT INTO Products (ProductCode, NameEn, NameAr, CategoryId, CaffeineIndex, IsCustomizable) VALUES
('iced_tea', 'Iced Tea', 'آيس تي', 2, 2, 0),
('hibiscus_iced_tea', 'Hibiscus Iced Tea', 'آيس تي كوجرات', 2, 0, 0),
('iced_tea_flavors', 'Iced Tea Flavors', 'آيس تي بنكهات', 2, 2, 0);

-- Ice-Espresso Products
INSERT INTO Products (ProductCode, NameEn, NameAr, CategoryId, CaffeineIndex, IsCustomizable) VALUES
('ice_espresso_plain', 'Ice-Espresso (Plain)', 'آيس-بريسو (سادة)', 3, 6, 1),
('ice_espresso_flavors', 'Ice-Espresso Flavors', 'آيس-بريسو بنكهات', 3, 6, 1),
('ice_espresso_coconut', 'Ice-Espresso (Coconut)', 'آيس-بريسو (جوز الهند)', 3, 6, 1);

-- Frappuccino Products
INSERT INTO Products (ProductCode, NameEn, NameAr, CategoryId, CaffeineIndex, IsCustomizable) VALUES
('frappuccino', 'Frappuccino', 'فرابتشينو', 4, 4, 1);

-- Energy Drinks
INSERT INTO Products (ProductCode, NameEn, NameAr, CategoryId, CaffeineIndex, IsCustomizable) VALUES
('mexican_energy', 'Mexican Energy', 'مكسيكي', 5, 7, 0),
('cold_lava', 'Cold Lava', 'كولد لافا', 5, 7, 0),
('purple_haze', 'Purple Haze', 'بيربل هيز', 5, 7, 0);

-- Yogurt & Essence
INSERT INTO Products (ProductCode, NameEn, NameAr, CategoryId, CaffeineIndex, IsCustomizable) VALUES
('bloody_dairy', 'Bloody Dairy', 'بلودي ديري', 6, 0, 0),
('berry_essence', 'Berry Essence', 'جوهر التوت', 6, 0, 0),
('joy', 'Joy', 'سعادة', 6, 0, 0);

-- Milkshakes
INSERT INTO Products (ProductCode, NameEn, NameAr, CategoryId, CaffeineIndex, IsCustomizable) VALUES
('brownies_shake', 'Brownies Shake', 'براونيز', 7, 0, 1),
('strawberry_ice_cream', 'Strawberry Ice Cream Shake', 'ستروبيري آيس كريم', 7, 0, 1),
('pistachio_biscuits', 'Pistachio Biscuits Shake', 'فستق بسكويت', 7, 0, 1),
('oreo_kinder_lotus', 'Oreo/Kinder/Lotus Shake', 'أوريو/كيندر/لوتس', 7, 0, 1);

-- Protein Shakes
INSERT INTO Products (ProductCode, NameEn, NameAr, CategoryId, CaffeineIndex, IsCustomizable) VALUES
('protein_shake_banana_strawberry', 'Protein Shake (Banana & Strawberry)', 'بروتين شيك (موز وفراولة)', 8, 0, 0),
('protein_shake_chocolate', 'Protein Shake (Chocolate)', 'بروتين شيك (شوكولاتة)', 8, 0, 0);

-- Smoothies
INSERT INTO Products (ProductCode, NameEn, NameAr, CategoryId, CaffeineIndex, IsCustomizable) VALUES
('smoothie_strawberry', 'Strawberry Smoothie', 'سموذي فراولة', 9, 0, 0),
('smoothie_mango', 'Mango Smoothie', 'سموذي مانجو', 9, 0, 0),
('smoothie_red_mango', 'Red Mango Smoothie', 'سموذي ريد مانجو', 9, 0, 0),
('smoothie_pomegranate', 'Pomegranate Smoothie', 'سموذي رمان', 9, 0, 0);

-- Fresh Juices
INSERT INTO Products (ProductCode, NameEn, NameAr, CategoryId, CaffeineIndex, IsCustomizable) VALUES
('fresh_orange_juice', 'Fresh Orange Juice', 'عصير برتقال طازج', 10, 0, 0),
('fresh_lemon_juice', 'Fresh Lemon Juice', 'عصير ليمون طازج', 10, 0, 0),
('fresh_banana_juice', 'Fresh Banana Juice', 'عصير موز طازج', 10, 0, 0),
('fresh_manda_juice', 'Fresh Manda Juice', 'عصير ماندا طازج', 10, 0, 0);

-- Hot Coffee
INSERT INTO Products (ProductCode, NameEn, NameAr, CategoryId, CaffeineIndex, IsCustomizable) VALUES
('espresso_single', 'Espresso (Single)', 'إسبريسو (واحد)', 11, 6, 0),
('espresso_double', 'Espresso (Double)', 'إسبريسو (دبل)', 11, 7, 0),
('espresso_triple', 'Espresso (Triple)', 'إسبريسو (تربل)', 11, 7, 0),
('joco_black', 'Joco Black Coffee', 'جوكو بلاك', 11, 5, 0),
('chocolate_coffee', 'Chocolate Coffee', 'قهوة شوكولاتة', 11, 4, 0),
('turkish_coffee', 'Turkish Coffee', 'قهوة تركية', 11, 5, 0),
('cappuccino', 'Cappuccino', 'كابتشينو', 11, 5, 1),
('nescafe', 'Nescafe', 'نسكافيه', 11, 3, 1),
('spanish_latte_hot', 'Spanish Latte (Hot)', 'سبانيش لاتيه (ساخن)', 11, 4, 1),
('berry_latte_hot', 'Berry Latte (Hot)', 'لاتيه توت (ساخن)', 11, 4, 1),
('flavoured_latte_hot', 'Flavoured Latte (Hot)', 'لاتيه بنكهات (ساخن)', 11, 4, 1);

-- Specialty
INSERT INTO Products (ProductCode, NameEn, NameAr, CategoryId, CaffeineIndex, IsCustomizable) VALUES
('kathban', 'Kathban', 'كثبان', 12, 6, 0),
('sun_ice', 'Sun Ice', 'ثلج الشمس', 12, 4, 0);

-- Tea
INSERT INTO Products (ProductCode, NameEn, NameAr, CategoryId, CaffeineIndex, IsCustomizable) VALUES
('iraqi_tea', 'Iraqi Tea', 'شاي عراقي', 13, 2, 0),
('cinnamon_tea', 'Cinnamon Tea', 'شاي قرفة', 13, 1, 0),
('mint_tea', 'Mint Tea', 'شاي نعناع', 13, 0, 0),
('hibiscus_tea', 'Hibiscus Tea', 'شاي كوجرات', 13, 0, 0),
('milk_tea', 'Milk Tea', 'شاي بالحليب', 13, 2, 0);
GO

-- =============================================
-- Insert Product Tags (Hot/Cold)
-- =============================================

-- Tag all Iced Coffee, Iced Tea, Ice-Espresso, Frappuccino, Energy Drinks, Yogurt & Essence, Milkshakes, Protein Shakes, Smoothies, Fresh Juices as Cold
INSERT INTO ProductTags (ProductId, Tag)
SELECT ProductId, 'Cold' FROM Products WHERE CategoryId IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

-- Tag all Hot Coffee, Specialty, Tea as Hot
INSERT INTO ProductTags (ProductId, Tag)
SELECT ProductId, 'Hot' FROM Products WHERE CategoryId IN (11, 12, 13);
GO

-- =============================================
-- Insert Product Prices
-- =============================================

-- Iced Coffee Prices
INSERT INTO ProductPrices (ProductId, Size, Price)
SELECT ProductId, 'medium', 4500 FROM Products WHERE ProductCode = 'iced_mocha'
UNION ALL SELECT ProductId, 'large', 6000 FROM Products WHERE ProductCode = 'iced_mocha'
UNION ALL SELECT ProductId, 'medium', 4000 FROM Products WHERE ProductCode = 'cookies_iced_latte'
UNION ALL SELECT ProductId, 'large', 5500 FROM Products WHERE ProductCode = 'cookies_iced_latte'
UNION ALL SELECT ProductId, 'medium', 3000 FROM Products WHERE ProductCode = 'iced_latte'
UNION ALL SELECT ProductId, 'large', 4000 FROM Products WHERE ProductCode = 'iced_latte'
UNION ALL SELECT ProductId, 'medium', 3000 FROM Products WHERE ProductCode = 'iced_coffee'
UNION ALL SELECT ProductId, 'large', 4000 FROM Products WHERE ProductCode = 'iced_coffee'
UNION ALL SELECT ProductId, 'medium', 4000 FROM Products WHERE ProductCode = 'iced_spanish_latte'
UNION ALL SELECT ProductId, 'large', 5000 FROM Products WHERE ProductCode = 'iced_spanish_latte'
UNION ALL SELECT ProductId, 'medium', 4000 FROM Products WHERE ProductCode = 'iced_berry_latte'
UNION ALL SELECT ProductId, 'large', 5000 FROM Products WHERE ProductCode = 'iced_berry_latte'
UNION ALL SELECT ProductId, 'large', 5000 FROM Products WHERE ProductCode = 'chocolate_iced_latte'
UNION ALL SELECT ProductId, 'medium', 4000 FROM Products WHERE ProductCode = 'flavoured_iced_latte'
UNION ALL SELECT ProductId, 'large', 5500 FROM Products WHERE ProductCode = 'flavoured_iced_latte'
UNION ALL SELECT ProductId, 'large', 5000 FROM Products WHERE ProductCode = 'iced_cappuccino'
UNION ALL SELECT ProductId, 'medium', 4000 FROM Products WHERE ProductCode = 'banana_cinnamon';

-- Iced Tea Prices
INSERT INTO ProductPrices (ProductId, Size, Price)
SELECT ProductId, 'medium', 3000 FROM Products WHERE ProductCode = 'iced_tea'
UNION ALL SELECT ProductId, 'medium', 3000 FROM Products WHERE ProductCode = 'hibiscus_iced_tea'
UNION ALL SELECT ProductId, 'medium', 4000 FROM Products WHERE ProductCode = 'iced_tea_flavors';

-- Ice-Espresso Prices
INSERT INTO ProductPrices (ProductId, Size, Price)
SELECT ProductId, 'medium', 3500 FROM Products WHERE ProductCode = 'ice_espresso_plain'
UNION ALL SELECT ProductId, 'medium', 4500 FROM Products WHERE ProductCode = 'ice_espresso_flavors'
UNION ALL SELECT ProductId, 'medium', 5000 FROM Products WHERE ProductCode = 'ice_espresso_coconut';

-- Frappuccino Prices
INSERT INTO ProductPrices (ProductId, Size, Price)
SELECT ProductId, 'medium', 5000 FROM Products WHERE ProductCode = 'frappuccino';

-- Energy Drinks Prices
INSERT INTO ProductPrices (ProductId, Size, Price)
SELECT ProductId, 'medium', 3000 FROM Products WHERE ProductCode = 'mexican_energy'
UNION ALL SELECT ProductId, 'medium', 4500 FROM Products WHERE ProductCode = 'cold_lava'
UNION ALL SELECT ProductId, 'medium', 6000 FROM Products WHERE ProductCode = 'purple_haze';

-- Yogurt & Essence Prices
INSERT INTO ProductPrices (ProductId, Size, Price)
SELECT ProductId, 'medium', 7000 FROM Products WHERE ProductCode = 'bloody_dairy'
UNION ALL SELECT ProductId, 'medium', 7000 FROM Products WHERE ProductCode = 'berry_essence'
UNION ALL SELECT ProductId, 'medium', 7000 FROM Products WHERE ProductCode = 'joy';

-- Milkshakes Prices
INSERT INTO ProductPrices (ProductId, Size, Price)
SELECT ProductId, 'without_cream', 4000 FROM Products WHERE ProductCode = 'brownies_shake'
UNION ALL SELECT ProductId, 'with_cream', 5000 FROM Products WHERE ProductCode = 'brownies_shake'
UNION ALL SELECT ProductId, 'without_cream', 4000 FROM Products WHERE ProductCode = 'strawberry_ice_cream'
UNION ALL SELECT ProductId, 'with_cream', 5000 FROM Products WHERE ProductCode = 'strawberry_ice_cream'
UNION ALL SELECT ProductId, 'without_cream', 5000 FROM Products WHERE ProductCode = 'pistachio_biscuits'
UNION ALL SELECT ProductId, 'with_cream', 6000 FROM Products WHERE ProductCode = 'pistachio_biscuits'
UNION ALL SELECT ProductId, 'medium', 4000 FROM Products WHERE ProductCode = 'oreo_kinder_lotus'
UNION ALL SELECT ProductId, 'large', 5500 FROM Products WHERE ProductCode = 'oreo_kinder_lotus'
UNION ALL SELECT ProductId, 'medium_with_cream', 5000 FROM Products WHERE ProductCode = 'oreo_kinder_lotus'
UNION ALL SELECT ProductId, 'large_with_cream', 6500 FROM Products WHERE ProductCode = 'oreo_kinder_lotus';

-- Protein Shakes Prices
INSERT INTO ProductPrices (ProductId, Size, Price)
SELECT ProductId, 'medium', 5000 FROM Products WHERE ProductCode = 'protein_shake_banana_strawberry'
UNION ALL SELECT ProductId, 'medium', 5000 FROM Products WHERE ProductCode = 'protein_shake_chocolate';

-- Smoothies Prices
INSERT INTO ProductPrices (ProductId, Size, Price)
SELECT ProductId, 'medium', 5000 FROM Products WHERE ProductCode = 'smoothie_strawberry'
UNION ALL SELECT ProductId, 'large', 6500 FROM Products WHERE ProductCode = 'smoothie_strawberry'
UNION ALL SELECT ProductId, 'medium', 5000 FROM Products WHERE ProductCode = 'smoothie_mango'
UNION ALL SELECT ProductId, 'large', 6500 FROM Products WHERE ProductCode = 'smoothie_mango'
UNION ALL SELECT ProductId, 'medium', 5000 FROM Products WHERE ProductCode = 'smoothie_red_mango'
UNION ALL SELECT ProductId, 'large', 6500 FROM Products WHERE ProductCode = 'smoothie_red_mango'
UNION ALL SELECT ProductId, 'medium', 5000 FROM Products WHERE ProductCode = 'smoothie_pomegranate'
UNION ALL SELECT ProductId, 'large', 6500 FROM Products WHERE ProductCode = 'smoothie_pomegranate';

-- Fresh Juices Prices
INSERT INTO ProductPrices (ProductId, Size, Price)
SELECT ProductId, 'medium', 4500 FROM Products WHERE ProductCode = 'fresh_orange_juice'
UNION ALL SELECT ProductId, 'medium', 4000 FROM Products WHERE ProductCode = 'fresh_lemon_juice'
UNION ALL SELECT ProductId, 'medium', 4500 FROM Products WHERE ProductCode = 'fresh_banana_juice'
UNION ALL SELECT ProductId, 'medium', 5000 FROM Products WHERE ProductCode = 'fresh_manda_juice';

-- Hot Coffee Prices
INSERT INTO ProductPrices (ProductId, Size, Price)
SELECT ProductId, 'small', 2000 FROM Products WHERE ProductCode = 'espresso_single'
UNION ALL SELECT ProductId, 'small', 3000 FROM Products WHERE ProductCode = 'espresso_double'
UNION ALL SELECT ProductId, 'small', 4000 FROM Products WHERE ProductCode = 'espresso_triple'
UNION ALL SELECT ProductId, 'small', 2500 FROM Products WHERE ProductCode = 'joco_black'
UNION ALL SELECT ProductId, 'medium', 3500 FROM Products WHERE ProductCode = 'joco_black'
UNION ALL SELECT ProductId, 'large', 4500 FROM Products WHERE ProductCode = 'joco_black'
UNION ALL SELECT ProductId, 'small', 2500 FROM Products WHERE ProductCode = 'chocolate_coffee'
UNION ALL SELECT ProductId, 'medium', 3500 FROM Products WHERE ProductCode = 'chocolate_coffee'
UNION ALL SELECT ProductId, 'large', 4500 FROM Products WHERE ProductCode = 'chocolate_coffee'
UNION ALL SELECT ProductId, 'small', 2000 FROM Products WHERE ProductCode = 'turkish_coffee'
UNION ALL SELECT ProductId, 'medium', 3000 FROM Products WHERE ProductCode = 'turkish_coffee'
UNION ALL SELECT ProductId, 'large', 4000 FROM Products WHERE ProductCode = 'turkish_coffee'
UNION ALL SELECT ProductId, 'medium', 3000 FROM Products WHERE ProductCode = 'cappuccino'
UNION ALL SELECT ProductId, 'large', 4000 FROM Products WHERE ProductCode = 'cappuccino'
UNION ALL SELECT ProductId, 'medium', 3000 FROM Products WHERE ProductCode = 'nescafe'
UNION ALL SELECT ProductId, 'large', 4000 FROM Products WHERE ProductCode = 'nescafe'
UNION ALL SELECT ProductId, 'medium', 4000 FROM Products WHERE ProductCode = 'spanish_latte_hot'
UNION ALL SELECT ProductId, 'large', 5000 FROM Products WHERE ProductCode = 'spanish_latte_hot'
UNION ALL SELECT ProductId, 'medium', 4000 FROM Products WHERE ProductCode = 'berry_latte_hot'
UNION ALL SELECT ProductId, 'large', 5000 FROM Products WHERE ProductCode = 'berry_latte_hot'
UNION ALL SELECT ProductId, 'medium', 4000 FROM Products WHERE ProductCode = 'flavoured_latte_hot'
UNION ALL SELECT ProductId, 'large', 5500 FROM Products WHERE ProductCode = 'flavoured_latte_hot';

-- Specialty Prices
INSERT INTO ProductPrices (ProductId, Size, Price)
SELECT ProductId, 'medium', 8000 FROM Products WHERE ProductCode = 'kathban'
UNION ALL SELECT ProductId, 'medium', 5000 FROM Products WHERE ProductCode = 'sun_ice';

-- Tea Prices
INSERT INTO ProductPrices (ProductId, Size, Price)
SELECT ProductId, 'small', 1000 FROM Products WHERE ProductCode = 'iraqi_tea'
UNION ALL SELECT ProductId, 'small', 1000 FROM Products WHERE ProductCode = 'cinnamon_tea'
UNION ALL SELECT ProductId, 'small', 1500 FROM Products WHERE ProductCode = 'mint_tea'
UNION ALL SELECT ProductId, 'small', 1500 FROM Products WHERE ProductCode = 'hibiscus_tea'
UNION ALL SELECT ProductId, 'small', 2000 FROM Products WHERE ProductCode = 'milk_tea';
GO

-- =============================================
-- Insert Product Flavors
-- =============================================

-- Flavoured Iced Latte
INSERT INTO ProductFlavors (ProductId, FlavorName)
SELECT ProductId, 'Hazelnut' FROM Products WHERE ProductCode = 'flavoured_iced_latte'
UNION ALL SELECT ProductId, 'Caramel' FROM Products WHERE ProductCode = 'flavoured_iced_latte'
UNION ALL SELECT ProductId, 'Vanilla' FROM Products WHERE ProductCode = 'flavoured_iced_latte'
UNION ALL SELECT ProductId, 'Coconut' FROM Products WHERE ProductCode = 'flavoured_iced_latte';

-- Iced Tea Flavors
INSERT INTO ProductFlavors (ProductId, FlavorName)
SELECT ProductId, 'Mango' FROM Products WHERE ProductCode = 'iced_tea_flavors'
UNION ALL SELECT ProductId, 'Peach' FROM Products WHERE ProductCode = 'iced_tea_flavors'
UNION ALL SELECT ProductId, 'Strawberry' FROM Products WHERE ProductCode = 'iced_tea_flavors';

-- Ice-Espresso Flavors
INSERT INTO ProductFlavors (ProductId, FlavorName)
SELECT ProductId, 'Hazelnut' FROM Products WHERE ProductCode = 'ice_espresso_flavors'
UNION ALL SELECT ProductId, 'Berry' FROM Products WHERE ProductCode = 'ice_espresso_flavors'
UNION ALL SELECT ProductId, 'Vanilla' FROM Products WHERE ProductCode = 'ice_espresso_flavors'
UNION ALL SELECT ProductId, 'Caramel' FROM Products WHERE ProductCode = 'ice_espresso_flavors'
UNION ALL SELECT ProductId, 'Cookies' FROM Products WHERE ProductCode = 'ice_espresso_flavors';

-- Frappuccino Flavors
INSERT INTO ProductFlavors (ProductId, FlavorName)
SELECT ProductId, 'Vanilla' FROM Products WHERE ProductCode = 'frappuccino'
UNION ALL SELECT ProductId, 'Coconut' FROM Products WHERE ProductCode = 'frappuccino'
UNION ALL SELECT ProductId, 'Hazelnut' FROM Products WHERE ProductCode = 'frappuccino'
UNION ALL SELECT ProductId, 'Caramel' FROM Products WHERE ProductCode = 'frappuccino';

-- Flavoured Latte (Hot)
INSERT INTO ProductFlavors (ProductId, FlavorName)
SELECT ProductId, 'Hazelnut' FROM Products WHERE ProductCode = 'flavoured_latte_hot'
UNION ALL SELECT ProductId, 'Caramel' FROM Products WHERE ProductCode = 'flavoured_latte_hot'
UNION ALL SELECT ProductId, 'Vanilla' FROM Products WHERE ProductCode = 'flavoured_latte_hot'
UNION ALL SELECT ProductId, 'Coconut' FROM Products WHERE ProductCode = 'flavoured_latte_hot';
GO

-- =============================================
-- Verification Queries
-- =============================================

-- Check total products
SELECT COUNT(*) AS TotalProducts FROM Products;
SELECT COUNT(*) AS TotalPrices FROM ProductPrices;
SELECT COUNT(*) AS TotalFlavors FROM ProductFlavors;
SELECT COUNT(*) AS TotalTags FROM ProductTags;

-- View all products with their details
SELECT 
    p.ProductCode,
    p.NameEn,
    c.Name AS Category,
    p.CaffeineIndex,
    p.IsCustomizable,
    COUNT(pp.Size) AS AvailableSizes,
    COUNT(pf.FlavorName) AS AvailableFlavors
FROM Products p
INNER JOIN Categories c ON p.CategoryId = c.CategoryId
LEFT JOIN ProductPrices pp ON p.ProductId = pp.ProductId
LEFT JOIN ProductFlavors pf ON p.ProductId = pf.ProductId
GROUP BY p.ProductCode, p.NameEn, c.Name, p.CaffeineIndex, p.IsCustomizable
ORDER BY c.Name, p.NameEn;
GO

PRINT 'Data population completed successfully!';
PRINT 'Total Products: ' + CAST((SELECT COUNT(*) FROM Products) AS NVARCHAR);
PRINT 'Total Prices: ' + CAST((SELECT COUNT(*) FROM ProductPrices) AS NVARCHAR);
PRINT 'Total Flavors: ' + CAST((SELECT COUNT(*) FROM ProductFlavors) AS NVARCHAR);
GO



