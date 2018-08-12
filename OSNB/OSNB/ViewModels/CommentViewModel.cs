using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSNB.ViewModels
{
    public class CommentViewModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "E-Mail")]
        [MaxLength(150)]
        public string Email { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Comment Date")]
        public DateTime CreateDate { get; set; }

        public int PostId { get; set; }
        public virtual PostViewModel PostViewModel { get; set; }
    }
}