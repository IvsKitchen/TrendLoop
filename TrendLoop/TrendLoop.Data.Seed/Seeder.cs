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
        private static Dictionary<string, string> users = new Dictionary<string, string>()
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


        private static List<string> userAvatars = new List<string>()
        {
            "https://images.unsplash.com/photo-1580489944761-15a19d654956?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NHx8d29tYW4lMjBhdmF0YXJ8ZW58MHx8MHx8fDA%3D",
            "https://images.unsplash.com/photo-1701615004837-40d8573b6652?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjV8fHdvbWFuJTIwYXZhdGFyfGVufDB8fDB8fHwy",
            "https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTR8fHdvbWFuJTIwYXZhdGFyfGVufDB8fDB8fHww",
            "https://images.unsplash.com/photo-1440589473619-3cde28941638?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NjJ8fHdvbWFuJTIwYXZhdGFyfGVufDB8fDB8fHwy",
            "https://images.unsplash.com/photo-1702482527875-e16d07f0d91b?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mjh8fHdvbWFuJTIwYXZhdGFyfGVufDB8fDB8fHww",
            "https://images.unsplash.com/photo-1475180098004-ca77a66827be?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8ODJ8fHdvbWFuJTIwYXZhdGFyfGVufDB8fDB8fHwy",
            "https://images.unsplash.com/photo-1651346158507-a2810590687f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mzl8fHdvbWFuJTIwYXZhdGFyfGVufDB8fDB8fHww",
            "https://images.unsplash.com/photo-1495924979005-79104481a52f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NjN8fHdvbWFuJTIwYXZhdGFyfGVufDB8fDB8fHww",
            "https://images.unsplash.com/photo-1469460340997-2f854421e72f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8ODR8fHdvbWFuJTIwYXZhdGFyfGVufDB8fDB8fHwy",
            "https://images.unsplash.com/photo-1492106087820-71f1a00d2b11?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8ODB8fHdvbWFuJTIwYXZhdGFyfGVufDB8fDB8fHww"
        };

        // Brands
        private static List<Brand> brands = new List<Models.Brand>()
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
            new AttributeType { Id = 1, Name = "Clothing Size"},
            new AttributeType { Id = 2, Name = "Shoes Size"},
            new AttributeType { Id = 3, Name = "Common Size"},
            new AttributeType { Id = 4, Name = "Belt Size"},
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
                "Dresses", new List<string>{ "https://images.unsplash.com/photo-1534875756527-5e8e4392005f?q=80&w=1932&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                             "https://images.unsplash.com/photo-1524504259109-ddd837233694?q=80&w=1728&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                             "https://images.unsplash.com/photo-1496747611176-843222e1e57c?q=80&w=2073&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"}
            },
            {
                "Jeans", new List<string>{ "https://images.unsplash.com/photo-1475178626620-a4d074967452?q=80&w=1886&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                           "https://images.unsplash.com/photo-1718252540617-6ecda2b56b57?q=80&w=1780&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                           "https://images.unsplash.com/photo-1458119516396-015721b6d60a?q=80&w=1908&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"}
            },
            {
                "Shirts", new List<string>{ "https://images.unsplash.com/photo-1621773881532-fe65715b5137?q=80&w=1854&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                            "https://images.unsplash.com/photo-1571044420976-94b71786bae8?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                            "https://images.unsplash.com/photo-1618354691229-88d47f285158?q=80&w=1915&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" }
            },
            {
                "Outerwear", new List<string>{ "https://images.unsplash.com/photo-1591047139829-d91aecb6caea?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8Y29hdCUyMG9uJTIwaGFuZ2VyfGVufDB8fDB8fHwy",
                                               "https://images.unsplash.com/flagged/photo-1554033750-2137b5cfd7ce?q=80&w=1978&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                               "https://images.unsplash.com/photo-1729808784071-346188be62e9?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTI3fHxqYWNrZXR8ZW58MHx8MHx8fDI%3D"}
            },
            {
                "Trousers", new List<string>{ "https://images.unsplash.com/photo-1506629082955-511b1aa562c8?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                              "https://images.unsplash.com/photo-1624378441864-6eda7eac51cb?q=80&w=1888&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                              "https://images.unsplash.com/photo-1552902875-9ac1f9fe0c07?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTIwfHx0cm91c2Vyc3xlbnwwfHwwfHx8Mg%3D%3D"}
            },
            {
                "Costumes", new List<string>{ "https://images.unsplash.com/photo-1661670274599-cca3d1053f2f?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                              "https://images.unsplash.com/photo-1661669475530-16a1ed2455bf?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                              "https://images.unsplash.com/photo-1614786269829-d24616faf56d?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MzV8fGJ1c2luZXNzJTIwY29zdHVtZXxlbnwwfHwwfHx8MA%3D%3D"}
            },
            {
                "Boots", new List<string>{ "https://images.unsplash.com/photo-1494955464529-790512c65305?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                           "https://images.unsplash.com/photo-1542834759-d9f324e7764b?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                           "https://images.unsplash.com/photo-1605733160314-4fc7dac4bb16?q=80&w=1790&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"}
            },
            {
                "Sandals", new List<string>{"https://images.unsplash.com/photo-1521632617300-1e0d873a69a3?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                            "https://images.unsplash.com/photo-1677922336367-908959f1aa9c?q=80&w=1943&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                            "https://images.unsplash.com/photo-1564051806-be616e3bdcec?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"}
            },
            {
                "Heels", new List<string>{"https://images.unsplash.com/photo-1618274158638-41d9f8d9279d?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                          "https://images.unsplash.com/photo-1675237292372-5068aa262f43?q=80&w=1888&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                          "https://images.unsplash.com/photo-1637002932871-13be6fd3feb8?q=80&w=1932&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" }
            },
            {
                "Trainers", new List<string>{"https://images.unsplash.com/photo-1637437757614-6491c8e915b5?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                             "https://images.unsplash.com/photo-1715693754047-4c0b56576495?q=80&w=1776&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                             "https://images.unsplash.com/photo-1719523677291-a395426c1a87?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"}
            },
            {
                "Backpacks", new List<string>{"https://images.unsplash.com/photo-1622560480605-d83c853bc5c3?q=80&w=1888&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                              "https://images.unsplash.com/photo-1657296177449-8854b68537fb?q=80&w=1886&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                              "https://images.unsplash.com/photo-1596273501899-336404ed1701?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"}
            },
            {
                "Shoulder bags", new List<string>{ "https://images.unsplash.com/photo-1584917865442-de89df76afd3?q=80&w=1935&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                                   "https://images.unsplash.com/photo-1624687943971-e86af76d57de?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                                   "https://images.unsplash.com/photo-1594633313593-bab3825d0caf?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"}
            },
            {
                "Travel bags", new List<string>{ "https://images.unsplash.com/photo-1473188588951-666fce8e7c68?q=80&w=1948&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                                 "https://images.unsplash.com/photo-1564982759617-29646f69025c?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                                 "https://images.unsplash.com/photo-1608731267464-c0c889c2ff92?q=80&w=1886&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"}
            },
            {
                "Jewellery", new List<string>{ "https://images.unsplash.com/photo-1631982690223-8aa4be0a2497?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                               "https://images.unsplash.com/photo-1685970731571-72ede0cb26ea?q=80&w=1888&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                               "https://images.unsplash.com/photo-1723522938865-3df1a9399fd5?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"}
            },
            {
                "Sunglasses", new List<string>{ "https://images.unsplash.com/photo-1508296695146-257a814070b4?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8c3VuZ2xhc3Nlc3xlbnwwfHwwfHx8Mg%3D%3D",
                                                "https://images.unsplash.com/photo-1572635196237-14b3f281503f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTR8fHN1bmdsYXNzZXN8ZW58MHx8MHx8fDI%3D",
                                                "https://images.unsplash.com/photo-1502767089025-6572583495f9?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fHN1bmdsYXNzZXN8ZW58MHx8MHx8fDI%3D" }
            },
            {
                "Belts", new List<string>{"https://images.unsplash.com/photo-1666723043169-22e29545675c?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                          "https://images.unsplash.com/photo-1664286074176-5206ee5dc878?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                          "https://images.unsplash.com/photo-1684510334550-0c4fa8aaffd1?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"}
            },
            {
                "Watches", new List<string>{"https://images.unsplash.com/photo-1613177794106-be20802b11d3?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                            "https://images.unsplash.com/photo-1591147810559-9ae8cc24c862?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                                            "https://images.unsplash.com/photo-1506796684999-9fa2770af9c3?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"}
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
                Random random = new Random();

                foreach (KeyValuePair<string, string> emailPasswordPair in users)
                {
                    var user = new ApplicationUser { Email = emailPasswordPair.Key, UserName = emailPasswordPair.Key };
                    user.SellerRating = random.Next(2, 11) * 0.5;
                    // Get index of the key, so that each user has the corresponding avatar index
                    user.AvatarUrl = userAvatars[users.Keys.ToList().IndexOf(emailPasswordPair.Key)];
                    var result = await userManager.CreateAsync(user, emailPasswordPair.Value);

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
                    await dbContext.Brands.AddRangeAsync(brands);
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
            if (!dbContext.Products.Any())
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

                    // TODO: fix naming for plural
                    newProduct.Name = $"{brands.FirstOrDefault(b => b.Id == brandId).Name} {subcategory.Name.Singularize()}";
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
}
