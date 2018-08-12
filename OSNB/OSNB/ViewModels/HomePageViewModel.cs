using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSNB.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<MemberZoneViewModel> MemberZoneViewModels { get; set; }
        public IEnumerable<MemberBloodGroupViewModel> MemberBloodGroupViewModels { get; set; }
    }
}