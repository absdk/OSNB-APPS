using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSNB.Models
{
    public class SendEmailInfo
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Display(Name = "Your Name")]
        [MaxLength(200)]
        public string SenderName { get; set; }

        [Display(Name = "Your Contact No")]
        [MaxLength(200)]
        public string SenderContactNo { get; set; }

        [Display(Name = "Subject")]
        [MaxLength(200)]
        public string Subject { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }

        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }
    }
}