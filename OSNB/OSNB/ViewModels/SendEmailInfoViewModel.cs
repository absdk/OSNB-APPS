using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSNB.ViewModels
{
    public class SendEmailInfoViewModel
    {
        [Required]
        public int SendEmailInfoId { get; set; }

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

        public int MemberViewModelId { get; set; }
        public string MemberName { get; set; }
        public virtual MemberViewModel MemberViewModel { get; set; }
    }
}