using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSNB.ViewModels
{
    public class PostViewModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Post Title")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Display(Name = "Post Content")]
        public string Content { get; set; }

        [Display(Name = "Post Date")]
        public DateTime CreateDate { get; set; }

        //one to one relationship with user
        public string UserName { get; set; }
        public UserViewModel UserViewModel { get; set; }
    }
}