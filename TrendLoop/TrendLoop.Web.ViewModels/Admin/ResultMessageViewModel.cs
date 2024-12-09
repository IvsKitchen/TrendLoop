namespace TrendLoop.Web.ViewModels.Admin
{
    public class ResultMessageViewModel
    {
        public string Title { get; set; } = null!;

        public List<string> Messages { get; set; } = new List<string>();
    }
}
