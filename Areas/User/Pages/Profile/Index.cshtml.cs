using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FC_MVVC.Data.Models;
using FC_MVVC.Enums;
using FC_MVVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FC_MVVC.Areas.User.Pages.Profile
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;

        public ProfileModel(IApplicationUserService applicationUserService, IMapper mapper)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
        }
        [BindProperty]
        public ProfileViewModel Input { get; set; }

        public string WelcomeMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user =await _applicationUserService.GetUserByName(User.Identity.Name);
            Input = _mapper.Map<ProfileViewModel>(user);

            WelcomeMessage = !string.IsNullOrEmpty(Input.FirstName) && !string.IsNullOrEmpty(Input.LastName) ?
                            "Welcome " + Input.FirstName + " " + Input.LastName : "Welcome " + User.Identity.Name;

            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var email = await _applicationUserService.GetUserEmail(User.Identity.Name);
            await _applicationUserService.UpdateUserProfile(Input, email);

            return RedirectToPage("/Index");
        }
    }

    public class ProfileViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Display(Name = "Public Info")]
        public string PublicInfo { get; set; }
        [Display(Name = "Measure Type")]
        public MeasureType? MeasureType { get; set; }
    }
}