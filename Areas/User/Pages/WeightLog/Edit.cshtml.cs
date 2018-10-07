using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FC_MVVC.Areas.User.Pages.WeightLog.Models;
using FC_MVVC.Data.Models;
using FC_MVVC.Enums;
using FC_MVVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FC_MVVC.Areas.User.Pages.WeightLog
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;
        private readonly IWeigtLogService _weigtLogService;

        public MeasureType MeasureType { get; private set; }

        [BindProperty]
        public WeightLogViewModel Input { get; set; }

        public EditModel(IApplicationUserService applicationUserService, IWeigtLogService weigtLogService, IMapper mapper)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
            _weigtLogService = weigtLogService;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var log = await _weigtLogService.FindWeightLogById(id);
            Input = _mapper.Map<WeightLogViewModel>(log);
            var user = await GetUser();

            if (user.MeasureType == MeasureType.lbs)
                log = WeightConverter.ConvertToLbs(log);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
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

        public async Task<IActionResult> OnGetDelete(Guid id)
        {
            var log = await _weigtLogService.FindWeightLogById(id);
            await _weigtLogService.Remove(log);

            return RedirectToPage("/Table/Index");
        }

        private async Task<ApplicationUser> GetUser()
        {
            return await _applicationUserService.GetUserByName(User.Identity.Name);
        }
    }
}