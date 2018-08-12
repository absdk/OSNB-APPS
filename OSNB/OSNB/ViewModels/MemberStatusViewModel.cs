using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSNB.ViewModels
{
    public class MemberStatusViewModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Status Title")]
        [MaxLength(100)]
        public string MemberStatusTitle { get; set; }

        [Display(Name = "Status Description")]
        [MaxLength(200)]
        public string MemberStatusDescription { get; set; }

        [Display(Name = "Status Icon")]
        [MaxLength(200)]
        public string MemberStatusIcon { get; set; }

        public virtual ICollection<MemberViewModel> MemberViewModels { get; set; }
    }
}