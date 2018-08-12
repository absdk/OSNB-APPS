using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSNB.Models
{
    public class Member
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Display(Name = "Sur Name")]
        [MaxLength(100)]
        public string SurName { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }

        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Address")]
        [MaxLength(200)]
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Mobile Number")]
        [MaxLength(50)]
        public string MobileNumber { get; set; }

        [Display(Name = "Profile Thumb Image")]
        public string ThumbImageUrl { get; set; }

        [Display(Name = "Profile Small Image")]
        public string SmallImageUrl { get; set; }

        //one to one relationship with user
        public string UserName { get; set; }
        public User User { get; set; }

        public int MemberBloodGroupId { get; set; }
        [ForeignKey("MemberBloodGroupId")]
        public virtual MemberBloodGroup MemberBloodGroup { get; set; }

        public int MemberDistrictId { get; set; }
        [ForeignKey("MemberDistrictId")]
        public virtual MemberDistrict MemberDistrict { get; set; }

        public int MemberZoneId { get; set; }
        [ForeignKey("MemberZoneId")]
        public virtual MemberZone MemberZone { get; set; }

        public int MemberHospitalId { get; set; }
        [ForeignKey("MemberHospitalId")]
        public virtual MemberHospital MemberHospital { get; set; }

        public virtual ICollection<MemberDonate> MemberDonates { get; set; }

        public virtual ICollection<MemberStatus> MemberStatues { get; set; }

    }
}