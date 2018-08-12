using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSNB.Models
{
    public class MemberDonate
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Display(Name = "Donate Date")]
        public DateTime StartDonateDate { get; set; }

        [Display(Name = "Donate Date Over")]
        public DateTime EndDonateDate { get; set; }

        [Display(Name = "Recipient Address")]
        [MaxLength(250)]
        public string RecipientAddress { get; set; }

        [Display(Name = "Description")]
        [MaxLength(250)]
        public string Description { get; set; }

        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }

        public int MemberDonateWayId { get; set; }
        [ForeignKey("MemberDonateWayId")]
        public virtual MemberDonateWay MemberDonateWay { get; set; }

        public int MemberDonateTypeId { get; set; }
        [ForeignKey("MemberDonateTypeId")]
        public virtual MemberDonateType MemberDonateType { get; set; }

    }
}