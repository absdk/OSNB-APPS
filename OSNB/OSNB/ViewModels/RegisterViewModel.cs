using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OSNB.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Contact No")]
        [MaxLength(200)]
        public string ContactNo { get; set; }

        [Display(Name = "Blood Group")]
        [Range(1, long.MaxValue, ErrorMessage = "Please select one blood group.")]
        public int MemberBloodGroupId { get; set; }
        public string MemberBloodGroupName { get; set; }
        public virtual MemberBloodGroupViewModel MemberBloodGroupViewModel { get; set; }
        public IEnumerable<SelectListItem> ddlMemberBloodGroups { get; set; }

        [Display(Name = "District")]
        [Range(1, long.MaxValue, ErrorMessage = "Please select one district.")]
        public int MemberDistrictId { get; set; }
        public string MemberDistrictName { get; set; }
        public virtual MemberDistrictViewModel MemberDistrictViewModel { get; set; }
        public IEnumerable<SelectListItem> ddlMemberDistricts { get; set; }

        [Display(Name = "Zone")]
        [Range(1, long.MaxValue, ErrorMessage = "Please select one zone.")]
        public int MemberZoneId { get; set; }
        public string MemberZoneName { get; set; }
        public virtual MemberZoneViewModel MemberZoneViewModel { get; set; }
        public IEnumerable<SelectListItem> ddlMemberZones { get; set; }

        [Display(Name = "Hospital")]
        [Range(1, long.MaxValue, ErrorMessage = "Please select one hospital.")]
        public int MemberHospitalId { get; set; }
        public string MemberHospitalName { get; set; }
        public virtual MemberHospitalViewModel MemberHospitalViewModel { get; set; }
        public IEnumerable<SelectListItem> ddlMemberHospitals { get; set; }
    }
}