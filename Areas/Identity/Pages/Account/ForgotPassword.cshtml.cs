using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FC_MVVC.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FC_MVVC.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var code = "";
                var callbackUrl = "";

                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null )
                {
                    callbackUrl = Url.Page(
                    "/Account/Register",
                    pageHandler: null,
                    values: new {  },
                    protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Unknown User Password",
                    $"You are unknown to us please make sure that you have rregistered. You can regiter " +
                    $"here: href={HtmlEncoder.Default.Encode(callbackUrl)}");

                    return RedirectToPage("./Register");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                code = await _userManager.GeneratePasswordResetTokenAsync(user);
                callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Reset Password",
                    $"Please reset your here: href={HtmlEncoder.Default.Encode(callbackUrl)}");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
