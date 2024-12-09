using System.ComponentModel.DataAnnotations;

namespace TrendLoop.Web.ViewModels.Admin
{
    public class AdminAddUserDashboardViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // TODO change password validation
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public IEnumerable<UserInfoViewModel> Users { get; set; } = new HashSet<UserInfoViewModel>();
    }
}
