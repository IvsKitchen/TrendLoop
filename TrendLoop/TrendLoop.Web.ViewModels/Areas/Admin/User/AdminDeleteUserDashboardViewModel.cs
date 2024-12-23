﻿using System.ComponentModel.DataAnnotations;

namespace TrendLoop.Web.ViewModels.Areas.Admin.User
{
    public class AdminDeleteUserDashboardViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<UserInfoViewModel> Users { get; set; } = new HashSet<UserInfoViewModel>();
    }
}
