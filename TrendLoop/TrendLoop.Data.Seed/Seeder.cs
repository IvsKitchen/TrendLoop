using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrendLoop.Data.Models;

namespace TrendLoop.Data.Seed
{
    public class Seeder
    {
        private UserManager<ApplicationUser> userManager;
        private TrendLoopDbContext dbContext;

        private static double minPrice = 5;
        private static double maxPrice = 1000;

        // Users
        private Dictionary<string, string> users = new Dictionary<string, string>()
        {
            { "john.doe@mailbox.com", "John123!" },
            { "jane.smith@outlook.net", "Jane456!" },
            { "alex.miller@fastmail.org", "Alex789!" },
            { "lucy.brown@liveemail.com", "Lucy101!" },
            { "michaela.johnson@webmail.net", "Mike2023!" },
            { "emma.wilson@quickmail.com", "Emma312!" },
            { "chris.jackson@expressmail.org", "Chris400!" },
            { "olivia.davis@techmail.com", "Olivia555!" },
            { "david.martin@onlinemail.com", "David678!" },
            { "mia.clark@freshmail.net", "Mia789!" }
        };

        // Brands
        private static List<Brand> brands = new List<Brand>()
        {
            new Brand { Id = 1, Name = "Caro Mio" },
            new Brand { Id = 2, Name = "Celeste di Oro" },
            new Brand { Id = 3, Name = "Ellaro" },
            new Brand { Id = 4, Name = "Lirra" },
            new Brand { Id = 5, Name = "Lunaris" },
            new Brand { Id = 6, Name = "Maison Lennox" },
            new Brand { Id = 7, Name = "Noiré Blanc" },
            new Brand { Id = 8, Name = "Vera Noir" },
            new Brand { Id = 9, Name = "Vivienne Roma" },
            new Brand { Id = 10, Name = "Vossa" },
        };

        // Categories
        private static List<Category> categories = new List<Category>()
        {
            new Category { Id = 1, Name = "Clothing" },
            new Category { Id = 2, Name = "Shoes" },
            new Category { Id = 3, Name = "Bags" },
            new Category { Id = 4, Name = "Accessories" },
        };

        // Attribute Types 
        private static List<AttributeType> attributeTypes = new List<AttributeType>
        {
            new AttributeType { Id = 1, Name = "ClothingSize"},
            new AttributeType { Id = 2, Name = "ShoesSize"},
            new AttributeType { Id = 3, Name = "CommonSize"},
            new AttributeType { Id = 4, Name = "BeltSize"},
            new AttributeType { Id = 5, Name = "Material" },
            new AttributeType { Id = 6, Name = "Color" }
        };

        // Attribute Values
        private static List<AttributeValue> attributeValues = new List<AttributeValue>
        {
            // Values for Attribute "ClothingSize"
            new AttributeValue { Id = 1, Value = "XS", AttributeTypeId = 1},
            new AttributeValue { Id = 2, Value = "S", AttributeTypeId = 1},
            new AttributeValue { Id = 3, Value = "M", AttributeTypeId = 1},
            new AttributeValue { Id = 4, Value = "L", AttributeTypeId = 1},
            new AttributeValue { Id = 5, Value = "XL", AttributeTypeId = 1},
            new AttributeValue { Id = 6, Value = "XXL", AttributeTypeId = 1},
            // Values for Attribute "ShoesSize"
            new AttributeValue { Id = 7, Value = "35", AttributeTypeId = 2},
            new AttributeValue { Id = 8, Value = "36", AttributeTypeId = 2},
            new AttributeValue { Id = 9, Value = "37", AttributeTypeId = 2},
            new AttributeValue { Id = 10, Value = "38", AttributeTypeId = 2},
            new AttributeValue { Id = 11, Value = "39", AttributeTypeId = 2},
            new AttributeValue { Id = 12, Value = "40", AttributeTypeId = 2},
            new AttributeValue { Id = 13, Value = "41", AttributeTypeId = 2},
            new AttributeValue { Id = 14, Value = "42", AttributeTypeId = 2},
            new AttributeValue { Id = 15, Value = "43", AttributeTypeId = 2},
            new AttributeValue { Id = 16, Value = "44", AttributeTypeId = 2},
            new AttributeValue { Id = 17, Value = "45", AttributeTypeId = 2},
            // Values for Attribute "CommonSize"
            new AttributeValue { Id = 18, Value = "Extra small", AttributeTypeId = 3},
            new AttributeValue { Id = 19, Value = "Small", AttributeTypeId = 3},
            new AttributeValue { Id = 20, Value = "Medium", AttributeTypeId = 3},
            new AttributeValue { Id = 21, Value = "Large", AttributeTypeId = 3},
            new AttributeValue { Id = 22, Value = "Extra large", AttributeTypeId = 3},
            // Values for Attribute "BeltSize"
            new AttributeValue { Id = 23, Value = "85 cm", AttributeTypeId = 4},
            new AttributeValue { Id = 24, Value = "95 cm", AttributeTypeId = 4},
            new AttributeValue { Id = 25, Value = "100 cm", AttributeTypeId = 4},
            new AttributeValue { Id = 26, Value = "105 cm", AttributeTypeId = 4},
            new AttributeValue { Id = 27, Value = "110 cm", AttributeTypeId = 4},
            // Values for Attribute "Material"
            new AttributeValue { Id = 28, Value = "Cotton", AttributeTypeId = 5},
            new AttributeValue { Id = 29, Value = "Polyester", AttributeTypeId = 5},
            new AttributeValue { Id = 30, Value = "Silk", AttributeTypeId = 5},
            new AttributeValue { Id = 31, Value = "Wool", AttributeTypeId = 5},
            new AttributeValue { Id = 32, Value = "Denim", AttributeTypeId = 5},
            new AttributeValue { Id = 33, Value = "Leather", AttributeTypeId = 5},
            new AttributeValue { Id = 34, Value = "Metal", AttributeTypeId = 5},
            new AttributeValue { Id = 35, Value = "Plastic", AttributeTypeId = 5},
        };

        // Subcategories
        private static List<Subcategory> subcategories = new List<Subcategory>()
        {
            // Clothing subcategories
            new Subcategory { Id = 1, Name = "Dresses", CategoryId = 1 },
            new Subcategory { Id = 2, Name = "Jeans", CategoryId = 1 },
            new Subcategory { Id = 3, Name = "Shirts", CategoryId = 1 },
            new Subcategory { Id = 4, Name = "Outerwear", CategoryId = 1 },
            new Subcategory { Id = 5, Name = "Trousers", CategoryId = 1 },
            new Subcategory { Id = 6, Name = "Costumes", CategoryId = 1},
            
            // Shoes subcategories
            new Subcategory { Id = 7, Name = "Boots", CategoryId = 2 },
            new Subcategory { Id = 8, Name = "Sandals", CategoryId = 2 },
            new Subcategory { Id = 9, Name = "Heels", CategoryId = 2 },
            new Subcategory { Id = 10, Name = "Trainers", CategoryId = 2 },
            
            // Bags subcategories
            new Subcategory { Id = 11, Name = "Backpacks", CategoryId = 3 },
            new Subcategory { Id = 12, Name = "Shoulder bags", CategoryId = 3 },
            new Subcategory { Id = 13, Name = "Travel bags", CategoryId = 3 },
            
            // Accessories subcategories
            new Subcategory { Id = 14, Name = "Belts", CategoryId = 4 },
            new Subcategory { Id = 15, Name = "Jewellery", CategoryId = 4 },
            new Subcategory { Id = 16, Name = "Sunglasses", CategoryId = 4 },
            new Subcategory { Id = 17, Name = "Watches", CategoryId = 4 }
        };

        //Attribute types for subcategories
        List<SubcategoryAttributeType> subcategoriesAttributeTypes = new List<SubcategoryAttributeType>
        {
            // Dresses
            new SubcategoryAttributeType { AttributeTypeId = 1, SubcategoryId = 1},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 1},
            // Jeans
            new SubcategoryAttributeType { AttributeTypeId = 1, SubcategoryId = 2},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 2},
            // Shirts
            new SubcategoryAttributeType { AttributeTypeId = 1, SubcategoryId = 3},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 3},
            // Outerwear
            new SubcategoryAttributeType { AttributeTypeId = 1, SubcategoryId = 4},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 4},
            // Trousers
            new SubcategoryAttributeType { AttributeTypeId = 1, SubcategoryId = 5},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 5},
            // Costumes
            new SubcategoryAttributeType { AttributeTypeId = 1, SubcategoryId = 6},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 6},
            // Boots
            new SubcategoryAttributeType { AttributeTypeId = 2, SubcategoryId = 7},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 7},
            // Sandals
            new SubcategoryAttributeType { AttributeTypeId = 2, SubcategoryId = 8},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 8},
            // Heels
            new SubcategoryAttributeType { AttributeTypeId = 2, SubcategoryId = 9},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 9},
            // Trainers
            new SubcategoryAttributeType { AttributeTypeId = 2, SubcategoryId = 10},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 10},
            // Backpacks
            new SubcategoryAttributeType { AttributeTypeId = 3, SubcategoryId = 11},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 11},
            // Shouder bags
            new SubcategoryAttributeType { AttributeTypeId = 3, SubcategoryId = 12},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 12},
            // Travel bags
            new SubcategoryAttributeType { AttributeTypeId = 3, SubcategoryId = 13},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 13},
            // Belts
            new SubcategoryAttributeType { AttributeTypeId = 4, SubcategoryId = 14},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 14},
            // Jewellery
            new SubcategoryAttributeType { AttributeTypeId = 3, SubcategoryId = 15},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 15},
            // Sunglasses
            new SubcategoryAttributeType { AttributeTypeId = 3, SubcategoryId = 16},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 16},
            // Watches
            new SubcategoryAttributeType { AttributeTypeId = 3, SubcategoryId = 17},
            new SubcategoryAttributeType { AttributeTypeId = 5, SubcategoryId = 17}
        };

        private static Dictionary<string, List<string>> imagesBySubcategory = new Dictionary<string, List<string>>
        {
            {
                "Dresses", new List<string>{ "https://unsplash.com/photos/white-sleeveless-dress-near-brown-wooden-door-DnOgzmRYFeg",
                                             "https://unsplash.com/photos/womens-black-sleeveless-dress-4lFzZTnaIcQ",
                                             "https://unsplash.com/photos/woman-in-pink-spaghetti-strap-mini-dress-using-smartphone-caLJTqKFZYY"}
            },
            {
                "Jeans", new List<string>{ "https://unsplash.com/photos/woman-standing-wearing-jeans-and-white-top-zDyJOj8ZXG0",
                                           "https://unsplash.com/photos/a-pair-of-black-jeans-on-a-white-background-muo8Zdkz_4w",
                                           "https://unsplash.com/photos/woman-in-white-crew-neck-t-shirt-and-blue-denim-jeans-holding-black-camera-bEchRxSFYyw"}
            },
            {
                "Shirts", new List<string>{ "https://unsplash.com/photos/white-polo-shirt-hanged-on-brown-wooden-cabinet-ve2dwNxZ5Rg",
                                            "https://unsplash.com/photos/woman-wearing-white-blouse-and-white-pearl-necklace-18ZKVM5-3ic",
                                           "https://unsplash.com/photos/closeup-photo-of-woman-wearing-single-shoulder-dress-toa7kV0WPiM" }
            },
            {
                "Outerwear", new List<string>{ "https://unsplash.com/photos/brown-long-sleeve-shirt-on-white-clothes-hanger-Fg15LdqpWr",
                                               "https://unsplash.com/photos/woman-standing-while-wearing-pink-coat-X2UprmSxIHQ",
                                               "https://unsplash.com/photos/a-mannequin-wearing-a-jacket-with-a-hood-6emxbke6OxA"}
            },
            {
                "Trousers", new List<string>{ "https://unsplash.com/photos/closeup-photo-of-person-hiding-his-right-hand-in-his-pocket-eyFcZLLYvfA",
                                              "https://unsplash.com/photos/blue-denim-jeans-on-white-textile-5NPId7L1_p4",
                                              "https://unsplash.com/photos/woman-in-black-crop-top-and-white-blue-and-pink-floral-pants-XShgRpGVd9w"}
            },
            {
                "Costumes", new List<string>{ "https://unsplash.com/photos/a-person-wearing-sunglasses-I5Apn1MCX4A",
                                              "https://unsplash.com/photos/a-person-wearing-a-hat-and-sunglasses-XTTqlj2xq30",
                                              "https://unsplash.com/photos/a-woman-in-a-gray-jacket-and-black-pants-spTRNa8ErJY"}
            },
            {
                "Boots", new List<string>{ "https://unsplash.com/photos/woman-in-black-chunky-heels-boots-standing-on-gray-floor-4bUF4gWhEXc",
                                           "https://unsplash.com/photos/selective-focus-photography-of-pair-of-brown-leather-work-boots-c317Wf_dydg",
                                           "https://unsplash.com/photos/woman-in-pink-spaghetti-strap-mini-dress-using-smartphone-caLJTqKFZYY"}
            },
            {
                "Sandals", new List<string>{"https://unsplash.com/photos/pair-of-womens-white-kitten-heels-Mjz4YVIy69Y",
                                            "https://unsplash.com/photos/a-pair-of-shoes-sitting-on-top-of-a-table-next-to-a-rope-99ug0dVDTqU",
                                            "https://unsplash.com/photos/black-and-brown-leather-half-shoe-on-white-box-WWSCNfShT5M"}
            },
            {
                "Heels", new List<string>{"https://unsplash.com/photos/black-and-brown-peep-toe-heeled-shoes-BV8YCnXD7Ys",
                                          "https://unsplash.com/photos/a-woman-is-sitting-on-a-bench-with-her-legs-crossed-Jq_918HEDdY",
                                          "https://unsplash.com/photos/a-pair-of-red-high-heeled-shoes-on-a-grey-background-TrUqbmDVnf4" }
            },
            {
                "Trainers", new List<string>{"https://unsplash.com/photos/a-pair-of-running-shoes-on-a-white-background-W_uDEmTq0po",
                                             "https://unsplash.com/photos/a-white-and-red-sneaker-on-a-table-BGKYYeKkzNg",
                                             "https://unsplash.com/photos/a-pair-of-gray-sneakers-with-white-laces-wggDZ5mCF8w"}
            },
            {
                "Backpacks", new List<string>{"https://unsplash.com/photos/brown-leather-backpack-on-white-surface-3o-X8WJOP5E",
                                              "https://unsplash.com/photos/a-hand-holding-a-black-bag-qb3iYBz-RQ0",
                                              "https://unsplash.com/photos/white-and-black-leather-handbag-VDow9qDDMKc"}
            },
            {
                "Shoulder bags", new List<string>{ "https://unsplash.com/photos/red-leather-handbag-on-white-table-oCXVxwTFwqE",
                                                   "https://unsplash.com/photos/brown-leather-handbag-on-white-table-XwjrPFW7xw0",
                                                   "https://unsplash.com/photos/woman-in-white-lace-tank-top-carrying-brown-leather-sling-bag-TLoMyRdcQOE"}
            },
            {
                "Travel bags", new List<string>{ "https://unsplash.com/photos/brown-leather-satchel-bag-on-gray-concrete-surface-near-green-plant-at-daytime-pFLNV4gkXsc",
                                                 "https://unsplash.com/photos/red-supreme-bag-V_vgvY8ZSsk",
                                                 "https://unsplash.com/photos/person-holding-brown-leather-sling-bag-NqZ2vFqChaw"}
            },
            {
                "Jewellery", new List<string>{ "https://unsplash.com/photos/brown-leather-backpack-on-white-surface-3o-X8WJOP5E",
                                               "https://unsplash.com/photos/a-necklace-with-white-flowers-on-a-gold-chain-rvF5EglUXWg",
                                               "https://unsplash.com/photos/a-womans-hand-holding-onto-a-bracelet-n93BVI_sPZ4"}
            },
            {
                "Sunglasses", new List<string>{ "https://unsplash.com/photos/brown-leather-backpack-on-white-surface-3o-X8WJOP5E",
                                                "https://unsplash.com/photos/a-necklace-with-white-flowers-on-a-gold-chain-rvF5EglUXWg",
                                                "https://unsplash.com/photos/a-womans-hand-holding-onto-a-bracelet-n93BVI_sPZ4" }
            },
            {
                "Belts", new List<string>{"https://unsplash.com/photos/a-pair-of-sunglasses-on-a-table-tRu15nFN3Pw",
                                          "https://unsplash.com/photos/a-leather-belt-on-a-black-surface-eNEa7Gsfzzs",
                                          "https://unsplash.com/photos/a-close-up-of-a-leather-belt-on-a-bench-Mf194wsFKZI"}
            },
            {
                "Watches", new List<string>{"https://unsplash.com/photos/gold-link-bracelet-black-and-gold-square-watch-FoMMuiXO4FE",
                                            "https://unsplash.com/photos/person-wearing-silver-aluminum-case-apple-watch-with-white-sport-band-YWnFeV-MvRE",
                                            "https://unsplash.com/photos/person-wearing-round-silver-colored-analog-watch-at-119-XcUJUO2AqVA"}
            }
        };

        private static List<string> productDescriptions = new List<string>
        {
            "Stylish and comfortable, this versatile piece adds a touch of elegance to any outfit.",
            "Chic and practical, this product offers spacious compartments for all your essentials.",
            "Sleek, modern with stylish look",
            "Dazzling piece with a unique design, perfect for adding sparkle to any occasion.",
            "Classic and durable, with high-quality material, perfect for everyday wear and comfort.",
            "Elegant and timeless, this accessory completes any look with sophistication and style.",
            "Compact and trendy, this fashion product is perfect for both day-to-night versatility and convenience.",
            "Lightweight and durable, offers both comfort and style.",
            "Crafted with care, this piece adds a subtle touch of luxury to your everyday look.",
            "Versatile and functional, this piece is ideal for both casual and formal occasions."
        };

        public Seeder(UserManager<ApplicationUser> userManager, TrendLoopDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public async Task SeedDb(int productsCount)
        {
            await CreateUsers();
            await CreateBrands();
            await CreateCategories();
            await CreateAttributeTypes();
            await CreateAttributeValues();
            await CreateSubcategories();
            await CreateSubcategoriesAttributeTypes();
            await GenerateProducts(productsCount);
        }

        private async Task CreateUsers()
        {
            if (!dbContext.Users.Any())
            {
                foreach (var emailPassWordPair in users)
                {

                    var user = new ApplicationUser { Email = emailPassWordPair.Key, UserName = emailPassWordPair.Key };
                    var result = await userManager.CreateAsync(user, emailPassWordPair.Value);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"Error: {error.Description}");
                        }
                    }
                }
            }
        }

        private async Task CreateBrands()
        {
            if (!dbContext.Brands.Any())
            {
                using var transaction = await dbContext.Database.BeginTransactionAsync();
                try
                {
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Brands ON");
                    await dbContext.Brands.AddRangeAsync(brands); // Insert explicitly set Ids
                    await dbContext.SaveChangesAsync();
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Brands OFF");
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        private async Task CreateCategories()
        {
            if (!dbContext.Categories.Any())
            {
                using var transaction = await dbContext.Database.BeginTransactionAsync();
                try
                {
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Categories ON");
                    await dbContext.Categories.AddRangeAsync(categories);
                    await dbContext.SaveChangesAsync();
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Categories OFF");
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        private async Task CreateAttributeTypes()
        {
            if (!dbContext.AttributeTypes.Any())
            {
                using var transaction = await dbContext.Database.BeginTransactionAsync();
                try
                {
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT AttributeTypes ON");
                    await dbContext.AttributeTypes.AddRangeAsync(attributeTypes);
                    await dbContext.SaveChangesAsync();
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT AttributeTypes OFF");
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        private async Task CreateAttributeValues()
        {
            if (!dbContext.AttributeValues.Any())
            {
                using var transaction = await dbContext.Database.BeginTransactionAsync();
                try
                {
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT AttributeValues ON");
                    await dbContext.AttributeValues.AddRangeAsync(attributeValues);
                    await dbContext.SaveChangesAsync();
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT AttributeValues OFF");
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        private async Task CreateSubcategories()
        {
            if (!dbContext.Subcategories.Any())
            {
                using var transaction = await dbContext.Database.BeginTransactionAsync();
                try
                {
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Subcategories ON");
                    await dbContext.Subcategories.AddRangeAsync(subcategories);
                    await dbContext.SaveChangesAsync();
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Subcategories OFF");
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        private async Task CreateSubcategoriesAttributeTypes()
        {
            if (!dbContext.SubcategoriesAttributeTypes.Any())
            {
                using var transaction = await dbContext.Database.BeginTransactionAsync();
                try
                {
                    // NOTE: we do not need to set IDENTITY_INSERT ON, since it is a mapping table with composite PK
                    await dbContext.SubcategoriesAttributeTypes.AddRangeAsync(subcategoriesAttributeTypes);
                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        private async Task GenerateProducts(int productsCount)
        {
            Random random = new Random();
            List<Product> newProducts = new List<Product>();

            for (int i = 0; i <= productsCount; i++)
            {
                // create empty product
                Product newProduct = new Product();
                // select random brand
                int brandId = brands[random.Next(0, brands.Count)].Id;
                // select random category Id
                int categoryId = categories[random.Next(0, categories.Count)].Id;
                // find the corresponding category
                Category category = await dbContext.Categories
                    .Include(c => c.Subcategories)
                    .ThenInclude(sc => sc.SubcategoryAttributeTypes)
                    .ThenInclude(scat => scat.AttributeType)
                    .ThenInclude(av => av.AttributeValues)
                    .FirstOrDefaultAsync(c => c.Id == categoryId);

                // from category select random subcategory
                Subcategory subcategory = category.Subcategories.ElementAt(random.Next(category.Subcategories.Count()));

                // create attribute values for the product, depending on attribute types of its subcategory
                List<ProductAttributeValue> productAttributeValues = new List<ProductAttributeValue>();
                foreach (SubcategoryAttributeType attributeTypeForSubcategory in subcategory.SubcategoryAttributeTypes)
                {
                    // create empty product-attribute value object
                    ProductAttributeValue productAttributeValue = new ProductAttributeValue();
                   
                    // assign product
                    productAttributeValue.Product = newProduct;
                    productAttributeValue.ProductId = newProduct.Id;

                    // assign attribute value id for product-attribute value object
                    List<AttributeValue> attributeValuesForAttributeType = attributeTypeForSubcategory.AttributeType.AttributeValues.ToList();
                    AttributeValue attributeValueForProduct = attributeValuesForAttributeType[random.Next(0, attributeValuesForAttributeType.Count)];
                    
                    productAttributeValue.AttributeValue = attributeValueForProduct;
                    productAttributeValue.AttributeValueId = attributeValueForProduct.Id;
                    
                    // add to collection
                    productAttributeValues.Add(productAttributeValue);
                }
                // extract the string value representation of the Size attribute type, stored in the corresponding product-attribute value object
                string productSize = productAttributeValues
                    .FirstOrDefault(pav => pav.AttributeValue.AttributeType.Name.ToLower().Contains("size"))
                    .AttributeValue.Value;
                
                newProduct.Name = $"{brands.FirstOrDefault(b => b.Id == brandId).Name} {subcategory.Name.Singularize()} {productSize} Size";
                newProduct.AddedOn = DateTime.Now;
                newProduct.BrandId = brandId;
                newProduct.CategoryId = category.Id;
                newProduct.SubcategoryId = subcategory.Id;
                newProduct.ImageUrl = imagesBySubcategory[subcategory.Name][random.Next(0, imagesBySubcategory[subcategory.Name].Count)];
                newProduct.Price = Math.Round((decimal)(random.NextDouble() * (maxPrice - minPrice) + minPrice), 2);
                newProduct.Description = productDescriptions[random.Next(0, productDescriptions.Count)];
                newProduct.Seller = await userManager.FindByEmailAsync(users.Keys.ToList()[random.Next(0, users.Keys.Count)]);

                // assign product-attribute values for product
                newProduct.ProductAttributeValues = productAttributeValues;

                newProducts.Add(newProduct);
            }

            await dbContext.AddRangeAsync(newProducts);
            await dbContext.SaveChangesAsync();
        }
    }

}
