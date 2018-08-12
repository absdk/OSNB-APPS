using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSNB.Models
{
    public class MemberZone
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Display(Name = "Zone Name")]
        [MaxLength(100)]
        public string ZoneName { get; set; }

        [Display(Name = "Location X")]
        public string LocationX { get; set; }

        [Display(Name = "Location Y")]
        public string LocationY { get; set; }

        [Display(Name = "Location X1")]
        public string LocationX1 { get; set; }

        [Display(Name = "Location Y1")]
        public string LocationY1 { get; set; }

        [Display(Name = "Location X2")]
        public string LocationX2 { get; set; }

        [Display(Name = "Location Y2")]
        public string LocationY2 { get; set; }

        public int MemberDistrictId { get; set; }
        [ForeignKey("MemberDistrictId")]
        public virtual MemberDistrict MemberDistrict { get; set; }

    }
}