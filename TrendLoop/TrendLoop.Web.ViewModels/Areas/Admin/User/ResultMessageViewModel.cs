﻿namespace TrendLoop.Web.ViewModels.Areas.Admin.User
{
    public class ResultMessageViewModel
    {
        public string Title { get; set; } = null!;

        public List<string> Messages { get; set; } = new List<string>();
    }
}