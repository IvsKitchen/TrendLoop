namespace TrendLoop.Web.ViewModels.Product
{
    public class AttributeValueInfoViewModel
    {
        public int Id { get; set; }

        public string Value { get; set; } = null!;

        public int AttributeTypeId { get; set; }
    }
}