using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSNB.ViewModels.DataTableViewModels
{
    public class MemberHospitalTableModel
    {
        public string MemberHospitalId { get; set; }
        public string HospitalName { get; set; }
        public string Address { get; set; }
        public string LocationX { get; set; }
        public string LocationY { get; set; }
        public string MemberZoneId { get; set; }
        public string MemberZoneName { get; set; }
    }
}