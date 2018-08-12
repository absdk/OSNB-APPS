using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSNB.Models
{
    public class MemberStatus
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Display(Name = "Member Status Title")]
        [MaxLength(100)]
        public string MemberStatusTitle { get; set; }

        [Display(Name = "Member Status Description")]
        [MaxLength(200)]
        public string MemberStatusDescription { get; set; }

        [Display(Name = "Member Status Icon")]
        [MaxLength(200)]
        public string MemberStatusIcon { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}