using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSNB.ViewModels
{
    public class MemberHospitalViewModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Hospital Name")]
        [MaxLength(100)]
        public string HospitalName { get; set; }

        [Display(Name = "Address")]
        [MaxLength(200)]
        public string Address { get; set; }

        [Display(Name = "Location X")]
        public string LocationX { get; set; }

        [Display(Name = "Location Y")]
        public string LocationY { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Please select one zone.")]
        [Display(Name = "Zone Name")]
        public int MemberZoneId { get; set; }

        public string MemberZoneName { get; set; }

        public IEnumerable<SelectListItem> ddlMemberZones { get; set; }
    }
}