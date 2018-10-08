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
        public IEnumerable<WeightLogTableViewModel> WeightLogs { get; private set; }
        [BindProperty]
        public MeasureType MeasureType { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ApplicationUser user = await _applicationUserService.GetUserByName(User.Identity.Name);
            var logs = await _weigtLogManageService.GetAllWeightLogs(user);
            MeasureType = user.MeasureType;

            if (MeasureType == MeasureType.lbs)
                logs = logs.Select( log => log = WeightConverter.ConvertToLbs(log));

            WeightLogs = _mapper.Map<IEnumerable<WeightLogTableViewModel>>(logs);

            return Page();
        }

     
    }
    public class WeightLogTableViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime LogDate { get; set; }

        [Required]
        [Range(0.0, 1000.0)]
        public float WeightValue { get; set; }
    }
}