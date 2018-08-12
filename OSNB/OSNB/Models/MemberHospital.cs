using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSNB.Models
{
    public class MemberHospital
    {
        [Key]
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

        public int MemberZoneId { get; set; }
        [ForeignKey("MemberZoneId")]
        public virtual MemberZone MemberZone { get; set; }
    }
}