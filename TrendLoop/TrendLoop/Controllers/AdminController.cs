using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TrendLoop.Data.Models;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels.Admin;
using static TrendLoop.Common.ApplicationConstants;

namespace TrendLoop.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IUserService userService;

        public AdminController(UserManager<ApplicationUser> userManager,IUserService userService) : base(userManager)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> UserDashboard()
        {
            var model = new AdminAddUserDashboardViewModel();
            model.Users = await userService.GetAllUsersAsync();

            return this.View(model);
        }

        // User Management Add User action
        [HttpPost]
        public async Task<IActionResult> AddUser(AdminAddUserDashboardViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newUser = new ApplicationUser { Email = model.Email, UserName = model.Email };
                var result = await userManager.CreateAsync(newUser, model.Password);

                ResultMessageViewModel resultMessageModel = new ResultMessageViewModel();
                if (result.Succeeded)
                {
                    // Return success message
                    resultMessageModel.Title = Messages.SuccessfullyAddedUserMessageTitle;
                    resultMessageModel.Messages.Add(string.Format(Messages.SuccessfullyAddedUserMessageBody, model.Email));
                }
                else
                {
                    // Return error message(s)
                    resultMessageModel.Title = Messages.FailedAddingUserMessageTitle;
                    resultMessageModel.Messages.Add(string.Format(Messages.FailedAddingUserMessageBody, model.Email));
                    resultMessageModel.Messages.AddRange(result.Errors.Select(e => e.Description));
                }

                // Store the resultMessageModel in TempData
                TempData["ResultMessage"] = JsonConvert.SerializeObject(resultMessageModel);

                // Redirect to the ResultMessage view
                return RedirectToAction("ResultMessage", "Admin");
            }

            // If Model state is not valid return the model for editing
            return View("UserDashboard", "Admin");
        }

        // User Management Delete User action
        [HttpPost]
        public async Task<IActionResult> DeleteUser(AdminDeleteUserDashboardViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser? userToDelete = await userManager.FindByEmailAsync(model.Email);

                ResultMessageViewModel resultMessageModel = new ResultMessageViewModel();
                if (userToDelete == null)
                {
                    // Return error message(s)
                    resultMessageModel.Title = Messages.UserNotFoundMessageTitle;
                    resultMessageModel.Messages.Add(string.Format(Messages.UserNotFoundMessageBody, model.Email));
                }
                else
                {
                    var result = await userManager.DeleteAsync(userToDelete);

                    if (result.Succeeded)
                    {
                        // Return success message
                        resultMessageModel.Title = Messages.SuccessfullyDeletedUserMessageTitle;
                        resultMessageModel.Messages.Add(string.Format(Messages.SuccessfullyDeletedUserMessageBody, model.Email));
                    }
                    else
                    {
                        // Return error message(s)
                        resultMessageModel.Title = Messages.FailedDeleteUserMessageTitle;
                        resultMessageModel.Messages.Add(string.Format(Messages.FailedDeletingUserMessageBody, model.Email));
                        resultMessageModel.Messages.AddRange(result.Errors.Select(e => e.Description));
                    }
                }

                // Store the resultMessageModel in TempData
                TempData["ResultMessage"] = JsonConvert.SerializeObject(resultMessageModel);

                // Redirect to the ResultMessage view
                return RedirectToAction("ResultMessage", "Admin");
            }

            // If Model state is not valid return the model for editing
            return View("UserDashboard","Admin");
        }

        [HttpGet]
        public IActionResult ResultMessage(ResultMessageViewModel model)
        {
            // Retrieve the resultMessageModel from TempData
            var resultMessageJson = TempData["ResultMessage"] as string;
            if (resultMessageJson != null)
            {
                var resultMessage = JsonConvert.DeserializeObject<ResultMessageViewModel>(resultMessageJson);
                return View(resultMessage);
            }

            // If no message is found, redirect to another page or show an error message
            return RedirectToAction("UserDashboard", "Admin");
        }
    }
}


