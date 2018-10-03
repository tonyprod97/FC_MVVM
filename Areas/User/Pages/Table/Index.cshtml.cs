using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FC_MVVC.Data.Models;
using FC_MVVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FC_MVVC.Areas.User.Pages.Table
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IWeigtLogService _weigtLogManageService;
        private readonly IApplicationUserService _applicationUserService;

        public IndexModel(IMapper mapper, IWeigtLogService weigtLogManageService,
                                    IApplicationUserService applicationUserService)
        {
            _mapper = mapper;
            _weigtLogManageService = weigtLogManageService;
            _applicationUserService = applicationUserService;
        }

        [BindProperty]
        public IEnumerable<FC_MVVC.Data.Models.WeightLog> WeightLogs { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ApplicationUser user = await _applicationUserService.GetUserByName(User.Identity.Name);
            WeightLogs = await _weigtLogManageService.GetAllWeightLogs(user);
            return Page();
        }

        
        public async Task<IActionResult> OnGetDeleteAsync(Guid id)
        {
            var log = await _weigtLogManageService.FindWeightLogById(id);
            WeightLogs.ToList().Remove(log);
            await _weigtLogManageService.Remove(log);
            return Page();
        }
    }
}