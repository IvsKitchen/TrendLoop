namespace TrendLoop.Common
{
    public static class EntityValidationConstants
    {
        public static class Product
        {
            public const int NameMaxLength = 100;
            public const int DescriptionMaxLength = 500;
            public const string PriceMinValueAsString = "0";
            public const string PriceMaxValueAsString = "79228162514264337593543950335";
            public const string AddedOnDateFormat = "dd MMMM yyyy";
        }

        public static class Brand
        {
            public const int NameMaxLength = 50;
        }

        public static class Material
        {
            public const int NameMaxLength = 50;
        }

        public static class Category
        {
            public const int NameMaxLength = 20;
        }

        public static class Subcategory
        {
            public const int NameMaxLength = 20;
        }

        public static class AttributeType
        {
            public const int NameMaxLength = 20;
        }

        public static class AttributeValue
        {
            public const int ValueMaxLength = 20;
        }
    }
}
