using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FC_MVVC.Areas.User.Pages.WeightLog.Models;
using FC_MVVC.Data;
using FC_MVVC.Data.Models;
using FC_MVVC.Enums;
using FC_MVVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FC_MVVC.Areas.User.Pages.WeightLog
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;
        private readonly IWeigtLogService _weigtLogService;

        public MeasureType MeasureType { get; private set; }

        [BindProperty]
        public WeightLogViewModel Input { get; set; }

        public CreateModel(IApplicationUserService applicationUserService, IWeigtLogService weigtLogService ,IMapper mapper)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
            _weigtLogService = weigtLogService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Input = new WeightLogViewModel()
            {
                LogDate = DateTime.Now
            };
            ApplicationUser user = await GetUser();
            MeasureType = user.MeasureType;

            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                var newLog = _mapper.Map<FC_MVVC.Data.Models.WeightLog>(Input);

                ApplicationUser user = await GetUser();
                if (user.MeasureType == MeasureType.lbs)
                    newLog = WeightConverter.ConvertToKg(newLog);

                newLog.User = user;

                await _weigtLogService.Add(newLog);

                return RedirectToPage("/Table/Index");
            }
            return Page();
        }

        private async Task<ApplicationUser> GetUser()
        {
            return await _applicationUserService.GetUserByName(this.User.Identity.Name);
        }
    }
}
