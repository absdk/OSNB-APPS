using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSNB.ViewModels
{
    public class MemberBloodGroupViewModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Blood Group")]
        [MaxLength(100)]
        public string BloodGroupName { get; set; }
    }
}