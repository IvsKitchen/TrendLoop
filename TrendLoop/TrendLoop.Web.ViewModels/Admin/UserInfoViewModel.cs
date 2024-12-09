using System.ComponentModel.DataAnnotations;

namespace TrendLoop.Web.ViewModels.Admin

{
    public class UserInfoViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string Username { get; set; }

        public double SellerRating { get; set; }
    }
}
