﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSNB.Models
{
    public class Role
    {
        [Key]
        [Display(Name = "Role Name")]
        public virtual string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}