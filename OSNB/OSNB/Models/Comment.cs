using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSNB.Models
{
    public class Comment
    {
        [Key]
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
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
    }
}