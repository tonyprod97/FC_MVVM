using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace FC_MVVC.Areas.User.Pages.Chart
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IWeigtLogService _weigtLogService;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;

        public IndexModel(IWeigtLogService weigtLogService, IApplicationUserService applicationUserService, IMapper mapper)
        {
            _weigtLogService = weigtLogService;
            _applicationUserService = applicationUserService;
            _mapper = mapper;
        }

        [BindProperty]
        public ChartViewModel Input { get; set; }

        public string Title { get; set; }
        public MeasureType MeasureType { get; set; }
        public IEnumerable<WeightLogViewModel> WeightLogs { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            IEnumerable<FC_MVVC.Data.Models.WeightLog> weightLogs = await _weigtLogService.GetAllWeightLogs(await _applicationUserService.GetUserByName(User.Identity.Name));
            Title = "Data since starting weight";
            ApplicationUser user = await _applicationUserService.GetUserByName(User.Identity.Name);
            MeasureType = user.MeasureType;
            if (MeasureType == MeasureType.lbs)
                weightLogs = weightLogs.Select(log => WeightConverter.ConvertToLbs(log));

            IList<WeightLogViewModel> chartData = _mapper.Map<IEnumerable<WeightLogViewModel>>(weightLogs).ToList();
            WeightLogs = chartData.OrderBy(log => log.LogDate);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            IEnumerable<FC_MVVC.Data.Models.WeightLog> weightLogs = await _weigtLogService.GetWeightLogsSinceDate(await _applicationUserService.GetUserByName(this.User.Identity.Name), Input);
            IList<WeightLogViewModel> chartData = _mapper.Map<IEnumerable<WeightLogViewModel>>(weightLogs).ToList();
            WeightLogs = chartData.OrderBy(log => log.LogDate);

            if (Input.StartingDate != null)
                Title = "Data since: " + Input.StartingDate.Value.ToLocalTime().ToShortDateString();
            return Page();
        }
    }

    public class ChartViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Since: ")]
        public Nullable<DateTime> StartingDate { get; set; }


        [Display(Name = "Number of Logs: ")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive numbers allowed.")]
        public Nullable<int> NumberOfLogs { get; set; }
    }
}