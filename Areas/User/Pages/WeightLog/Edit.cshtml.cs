using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FC_MVVC.Areas.User.Pages.WeightLog
{
    [Authorize]
    public class EditModel : PageModel
    {
        public void OnGet()
        {
        }

        public ActionResult OnGetDelete(Guid id)
        {
            return RedirectToPage("./Table/Index");
        }
    }
}