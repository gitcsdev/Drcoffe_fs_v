using DrCoffee_BackEnd.Data;
using DrCoffee_BackEnd.Models;

namespace DrCoffee_BackEnd.Services;

public static class DataSeeder
{
    public static async Task SeedMenuDataAsync(ApplicationDbContext context)
    {
        // Check if data already exists
        if (context.Products.Any())
        {
            return; // Data already seeded
        }

        // Define categories with display order
        var categories = new Dictionary<string, int>
        {
            { "Iced Coffee", 1 },
            { "Iced Tea", 2 },
            { "Ice-Espresso", 3 },
            { "Frappuccino", 4 },
            { "Energy Drinks", 5 },
            { "Yogurt & Essence", 6 },
            { "Milkshakes", 7 },
            { "Protein Shakes", 8 },
            { "Smoothies", 9 },
            { "Fresh Juices", 10 },
            { "Hot Coffee", 11 },
            { "Specialty", 12 },
            { "Tea", 13 }
        };

        // Create categories
        var categoryDict = new Dictionary<string, Category>();
        foreach (var (name, order) in categories)
        {
            var category = new Category
            {
                Name = name,
                DisplayOrder = order,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            context.Categories.Add(category);
            categoryDict[name] = category;
        }
        await context.SaveChangesAsync();

        // Seed Customization Options
        var customizationOptions = new[]
        {
            new { Id = "extra_syrup", NameEn = "Extra Syrup", NameAr = "سيروب إضافي", Price = 1000m },
            new { Id = "almond_milk", NameEn = "Almond Milk", NameAr = "حليب اللوز", Price = 1000m },
            new { Id = "skimmed_milk", NameEn = "Skimmed Milk", NameAr = "حليب خالي الدسم", Price = 1000m },
            new { Id = "extra_espresso", NameEn = "Extra Espresso Shot", NameAr = "شوت إسبريسو إضافي", Price = 1000m },
            new { Id = "add_cream", NameEn = "Add Cream", NameAr = "إضافة كريمة", Price = 1000m }
        };

        // Note: CustomizationOptions table exists but we're focusing on Products for now
        // You can add CustomizationOptions seeding if needed

        // Define all products from menu.json
        var products = new[]
        {
            // Iced Coffee
            new { Code = "iced_mocha", NameEn = "Iced Mocha", NameAr = "آيس موكا", Category = "Iced Coffee", Caffeine = 5, Customizable = true, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 4500 }, { "large", 6000 } } },
            new { Code = "cookies_iced_latte", NameEn = "Cookies Iced Latte", NameAr = "كوكيز آيس لاتيه", Category = "Iced Coffee", Caffeine = 4, Customizable = true, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 4000 }, { "large", 5500 } } },
            new { Code = "iced_latte", NameEn = "Iced Latte", NameAr = "آيس لاتيه", Category = "Iced Coffee", Caffeine = 4, Customizable = true, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 3000 }, { "large", 4000 } } },
            new { Code = "iced_coffee", NameEn = "Iced Coffee", NameAr = "آيس كوفي", Category = "Iced Coffee", Caffeine = 5, Customizable = true, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 3000 }, { "large", 4000 } } },
            new { Code = "iced_spanish_latte", NameEn = "Iced Spanish Latte", NameAr = "آيس سبانيش لاتيه", Category = "Iced Coffee", Caffeine = 4, Customizable = true, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 4000 }, { "large", 5000 } } },
            new { Code = "iced_berry_latte", NameEn = "Iced Berry Latte", NameAr = "آيس بيري لاتيه", Category = "Iced Coffee", Caffeine = 4, Customizable = true, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 4000 }, { "large", 5000 } } },
            new { Code = "chocolate_iced_latte", NameEn = "Chocolate Iced Latte", NameAr = "جوكلت آيس لاتيه", Category = "Iced Coffee", Caffeine = 4, Customizable = true, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "large", 5000 } } },
            new { Code = "flavoured_iced_latte", NameEn = "Flavoured Iced Latte", NameAr = "آيس لاتيه بنكهات", Category = "Iced Coffee", Caffeine = 4, Customizable = true, Tags = new[] { "Cold" }, Flavors = new[] { "Hazelnut", "Caramel", "Vanilla", "Coconut" }, Prices = new Dictionary<string, decimal> { { "medium", 4000 }, { "large", 5500 } } },
            new { Code = "iced_cappuccino", NameEn = "Iced Cappuccino", NameAr = "آيس كابتشينو", Category = "Iced Coffee", Caffeine = 5, Customizable = true, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "large", 5000 } } },
            new { Code = "banana_cinnamon", NameEn = "Banana Cinnamon", NameAr = "بنانا سينابون", Category = "Iced Coffee", Caffeine = 3, Customizable = true, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 4000 } } },
            
            // Iced Tea
            new { Code = "iced_tea", NameEn = "Iced Tea", NameAr = "آيس تي", Category = "Iced Tea", Caffeine = 2, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 3000 } } },
            new { Code = "hibiscus_iced_tea", NameEn = "Hibiscus Iced Tea", NameAr = "آيس تي كوجرات", Category = "Iced Tea", Caffeine = 0, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 3000 } } },
            new { Code = "iced_tea_flavors", NameEn = "Iced Tea Flavors", NameAr = "آيس تي بنكهات", Category = "Iced Tea", Caffeine = 2, Customizable = false, Tags = new[] { "Cold" }, Flavors = new[] { "Mango", "Peach", "Strawberry" }, Prices = new Dictionary<string, decimal> { { "medium", 4000 } } },
            
            // Ice-Espresso
            new { Code = "ice_espresso_plain", NameEn = "Ice-Espresso (Plain)", NameAr = "آيس-بريسو (سادة)", Category = "Ice-Espresso", Caffeine = 6, Customizable = true, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 3500 } } },
            new { Code = "ice_espresso_flavors", NameEn = "Ice-Espresso Flavors", NameAr = "آيس-بريسو بنكهات", Category = "Ice-Espresso", Caffeine = 6, Customizable = true, Tags = new[] { "Cold" }, Flavors = new[] { "Hazelnut", "Berry", "Vanilla", "Caramel", "Cookies" }, Prices = new Dictionary<string, decimal> { { "medium", 4500 } } },
            new { Code = "ice_espresso_coconut", NameEn = "Ice-Espresso (Coconut)", NameAr = "آيس-بريسو (جوز الهند)", Category = "Ice-Espresso", Caffeine = 6, Customizable = true, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 5000 } } },
            
            // Frappuccino
            new { Code = "frappuccino", NameEn = "Frappuccino", NameAr = "فرابتشينو", Category = "Frappuccino", Caffeine = 4, Customizable = true, Tags = new[] { "Cold" }, Flavors = new[] { "Vanilla", "Coconut", "Hazelnut", "Caramel" }, Prices = new Dictionary<string, decimal> { { "medium", 5000 } } },
            
            // Energy Drinks
            new { Code = "mexican_energy", NameEn = "Mexican Energy", NameAr = "مكسيكي", Category = "Energy Drinks", Caffeine = 7, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 3000 } } },
            new { Code = "cold_lava", NameEn = "Cold Lava", NameAr = "كولد لافا", Category = "Energy Drinks", Caffeine = 7, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 4500 } } },
            new { Code = "purple_haze", NameEn = "Purple Haze", NameAr = "بيربل هيز", Category = "Energy Drinks", Caffeine = 7, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 6000 } } },
            
            // Yogurt & Essence
            new { Code = "bloody_dairy", NameEn = "Bloody Dairy", NameAr = "بلودي ديري", Category = "Yogurt & Essence", Caffeine = 0, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 7000 } } },
            new { Code = "berry_essence", NameEn = "Berry Essence", NameAr = "جوهر التوت", Category = "Yogurt & Essence", Caffeine = 0, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 7000 } } },
            new { Code = "joy", NameEn = "Joy", NameAr = "سعادة", Category = "Yogurt & Essence", Caffeine = 0, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 7000 } } },
            
            // Milkshakes
            new { Code = "brownies_shake", NameEn = "Brownies Shake", NameAr = "براونيز", Category = "Milkshakes", Caffeine = 0, Customizable = true, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "without_cream", 4000 }, { "with_cream", 5000 } } },
            new { Code = "strawberry_ice_cream", NameEn = "Strawberry Ice Cream Shake", NameAr = "ستروبيري آيس كريم", Category = "Milkshakes", Caffeine = 0, Customizable = true, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "without_cream", 4000 }, { "with_cream", 5000 } } },
            new { Code = "pistachio_biscuits", NameEn = "Pistachio Biscuits Shake", NameAr = "فستق بسكويت", Category = "Milkshakes", Caffeine = 0, Customizable = true, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "without_cream", 5000 }, { "with_cream", 6000 } } },
            new { Code = "oreo_kinder_lotus", NameEn = "Oreo/Kinder/Lotus Shake", NameAr = "أوريو/كيندر/لوتس", Category = "Milkshakes", Caffeine = 0, Customizable = true, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 4000 }, { "large", 5500 }, { "medium_with_cream", 5000 }, { "large_with_cream", 6500 } } },
            
            // Protein Shakes
            new { Code = "protein_shake_banana_strawberry", NameEn = "Protein Shake (Banana & Strawberry)", NameAr = "بروتين شيك (موز وفراولة)", Category = "Protein Shakes", Caffeine = 0, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 5000 } } },
            new { Code = "protein_shake_chocolate", NameEn = "Protein Shake (Chocolate)", NameAr = "بروتين شيك (شوكولاتة)", Category = "Protein Shakes", Caffeine = 0, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 5000 } } },
            
            // Smoothies
            new { Code = "smoothie_strawberry", NameEn = "Strawberry Smoothie", NameAr = "سموذي فراولة", Category = "Smoothies", Caffeine = 0, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 5000 }, { "large", 6500 } } },
            new { Code = "smoothie_mango", NameEn = "Mango Smoothie", NameAr = "سموذي مانجو", Category = "Smoothies", Caffeine = 0, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 5000 }, { "large", 6500 } } },
            new { Code = "smoothie_red_mango", NameEn = "Red Mango Smoothie", NameAr = "سموذي ريد مانجو", Category = "Smoothies", Caffeine = 0, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 5000 }, { "large", 6500 } } },
            new { Code = "smoothie_pomegranate", NameEn = "Pomegranate Smoothie", NameAr = "سموذي رمان", Category = "Smoothies", Caffeine = 0, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 5000 }, { "large", 6500 } } },
            
            // Fresh Juices
            new { Code = "fresh_orange_juice", NameEn = "Fresh Orange Juice", NameAr = "عصير برتقال طازج", Category = "Fresh Juices", Caffeine = 0, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 4500 } } },
            new { Code = "fresh_lemon_juice", NameEn = "Fresh Lemon Juice", NameAr = "عصير ليمون طازج", Category = "Fresh Juices", Caffeine = 0, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 4000 } } },
            new { Code = "fresh_banana_juice", NameEn = "Fresh Banana Juice", NameAr = "عصير موز طازج", Category = "Fresh Juices", Caffeine = 0, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 4500 } } },
            new { Code = "fresh_manda_juice", NameEn = "Fresh Manda Juice", NameAr = "عصير ماندا طازج", Category = "Fresh Juices", Caffeine = 0, Customizable = false, Tags = new[] { "Cold" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 5000 } } },
            
            // Hot Coffee
            new { Code = "espresso_single", NameEn = "Espresso (Single)", NameAr = "إسبريسو (واحد)", Category = "Hot Coffee", Caffeine = 6, Customizable = false, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "small", 2000 } } },
            new { Code = "espresso_double", NameEn = "Espresso (Double)", NameAr = "إسبريسو (دبل)", Category = "Hot Coffee", Caffeine = 7, Customizable = false, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "small", 3000 } } },
            new { Code = "espresso_triple", NameEn = "Espresso (Triple)", NameAr = "إسبريسو (تربل)", Category = "Hot Coffee", Caffeine = 7, Customizable = false, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "small", 4000 } } },
            new { Code = "joco_black", NameEn = "Joco Black Coffee", NameAr = "جوكو بلاك", Category = "Hot Coffee", Caffeine = 5, Customizable = false, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "small", 2500 }, { "medium", 3500 }, { "large", 4500 } } },
            new { Code = "chocolate_coffee", NameEn = "Chocolate Coffee", NameAr = "قهوة شوكولاتة", Category = "Hot Coffee", Caffeine = 4, Customizable = false, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "small", 2500 }, { "medium", 3500 }, { "large", 4500 } } },
            new { Code = "turkish_coffee", NameEn = "Turkish Coffee", NameAr = "قهوة تركية", Category = "Hot Coffee", Caffeine = 5, Customizable = false, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "small", 2000 }, { "medium", 3000 }, { "large", 4000 } } },
            new { Code = "cappuccino", NameEn = "Cappuccino", NameAr = "كابتشينو", Category = "Hot Coffee", Caffeine = 5, Customizable = true, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 3000 }, { "large", 4000 } } },
            new { Code = "nescafe", NameEn = "Nescafe", NameAr = "نسكافيه", Category = "Hot Coffee", Caffeine = 3, Customizable = true, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 3000 }, { "large", 4000 } } },
            new { Code = "spanish_latte_hot", NameEn = "Spanish Latte (Hot)", NameAr = "سبانيش لاتيه (ساخن)", Category = "Hot Coffee", Caffeine = 4, Customizable = true, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 4000 }, { "large", 5000 } } },
            new { Code = "berry_latte_hot", NameEn = "Berry Latte (Hot)", NameAr = "لاتيه توت (ساخن)", Category = "Hot Coffee", Caffeine = 4, Customizable = true, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 4000 }, { "large", 5000 } } },
            new { Code = "flavoured_latte_hot", NameEn = "Flavoured Latte (Hot)", NameAr = "لاتيه بنكهات (ساخن)", Category = "Hot Coffee", Caffeine = 4, Customizable = true, Tags = new[] { "Hot" }, Flavors = new[] { "Hazelnut", "Caramel", "Vanilla", "Coconut" }, Prices = new Dictionary<string, decimal> { { "medium", 4000 }, { "large", 5500 } } },
            
            // Specialty
            new { Code = "kathban", NameEn = "Kathban", NameAr = "كثبان", Category = "Specialty", Caffeine = 6, Customizable = false, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 8000 } } },
            new { Code = "sun_ice", NameEn = "Sun Ice", NameAr = "ثلج الشمس", Category = "Specialty", Caffeine = 4, Customizable = false, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "medium", 5000 } } },
            
            // Tea
            new { Code = "iraqi_tea", NameEn = "Iraqi Tea", NameAr = "شاي عراقي", Category = "Tea", Caffeine = 2, Customizable = false, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "small", 1000 } } },
            new { Code = "cinnamon_tea", NameEn = "Cinnamon Tea", NameAr = "شاي قرفة", Category = "Tea", Caffeine = 1, Customizable = false, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "small", 1000 } } },
            new { Code = "mint_tea", NameEn = "Mint Tea", NameAr = "شاي نعناع", Category = "Tea", Caffeine = 0, Customizable = false, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "small", 1500 } } },
            new { Code = "hibiscus_tea", NameEn = "Hibiscus Tea", NameAr = "شاي كوجرات", Category = "Tea", Caffeine = 0, Customizable = false, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "small", 1500 } } },
            new { Code = "milk_tea", NameEn = "Milk Tea", NameAr = "شاي بالحليب", Category = "Tea", Caffeine = 2, Customizable = false, Tags = new[] { "Hot" }, Flavors = Array.Empty<string>(), Prices = new Dictionary<string, decimal> { { "small", 2000 } } }
        };

        // Create products
        foreach (var productData in products)
        {
            var category = categoryDict[productData.Category];
            var product = new Product
            {
                ProductCode = productData.Code,
                NameEn = productData.NameEn,
                NameAr = productData.NameAr,
                CategoryId = category.CategoryId,
                CaffeineIndex = productData.Caffeine,
                IsCustomizable = productData.Customizable,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.Products.Add(product);
            await context.SaveChangesAsync(); // Save to get ProductId

            // Add prices
            foreach (var (size, price) in productData.Prices)
            {
                var productPrice = new ProductPrice
                {
                    ProductId = product.ProductId,
                    Size = size,
                    Price = price,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                context.ProductPrices.Add(productPrice);
            }

            // Add tags
            foreach (var tag in productData.Tags)
            {
                var productTag = new ProductTag
                {
                    ProductId = product.ProductId,
                    Tag = tag
                };
                context.ProductTags.Add(productTag);
            }

            // Add flavors
            foreach (var flavor in productData.Flavors)
            {
                var productFlavor = new ProductFlavor
                {
                    ProductId = product.ProductId,
                    FlavorName = flavor
                };
                context.ProductFlavors.Add(productFlavor);
            }

            await context.SaveChangesAsync();
        }
    }
}


