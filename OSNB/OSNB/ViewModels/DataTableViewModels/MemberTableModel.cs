using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSNB.ViewModels.DataTableViewModels
{
    public class MemberTableModel
    {
        public string MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string UserName { get; set; }
        public string MemberBloodGroupId { get; set; }
        public string MemberBloodGroupName { get; set; }
        public string MemberDistrictId { get; set; }
        public string MemberDistrictName { get; set; }
        public string MemberZoneId { get; set; }
        public string MemberZoneName { get; set; }
        public string MemberHospitalId { get; set; }
        public string MemberHospitalName { get; set; }
    }
}