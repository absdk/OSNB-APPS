using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSNB.ViewModels
{
    public class MemberDonateWayViewModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Donate Way Name")]
        [MaxLength(200)]
        public string DonateWayName { get; set; }
    }
}