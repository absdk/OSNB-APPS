using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSNB.ViewModels.DataTableViewModels
{
    public class BloodRequestTableModel
    {
        public string BloodRequestId { get; set; }

        public string RequesterName { get; set; }

        public string RequesterContactNo { get; set; }

        public string RequesterAmount { get; set; }

        public string PresentLocation { get; set; }

        public string DateOfDonation { get; set; }

        public string AppealMessage { get; set; }

        public string RequesterStatus { get; set; }

        public string RequesterStatusMessage { get; set; }

        //Required Blood Group
        public string RequiredBloodGroupId { get; set; }
        public string RequiredBloodGroup { get; set; }
    }
}