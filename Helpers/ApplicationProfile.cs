using AutoMapper;
using FC_MVVC.Areas.User.Pages.Profile;
using FC_MVVC.Areas.User.Pages.WeightLog;
using FC_MVVC.Areas.User.Pages.WeightLog.Models;
using FC_MVVC.Data;
using FC_MVVC.Data.Models;

namespace FC_MVVC.Helpers
{
    public class ApplicationProfile: Profile
    {
        public ApplicationProfile()
        {
            CreateMap<WeightLogViewModel, WeightLog>();
            CreateMap<WeightLog, WeightLogViewModel>();
            CreateMap<ApplicationUser, ProfileViewModel>();
            CreateMap<ProfileViewModel, ApplicationUser>();
        }
    }
}
