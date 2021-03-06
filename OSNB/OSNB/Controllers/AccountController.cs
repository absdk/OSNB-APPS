﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using OSNB.Helpers;
using OSNB.Models;
using OSNB.ViewModels;

namespace OSNB.Controllers
{
    public class AccountController : Controller
    {
        private AppDbContext _db = new AppDbContext();
        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            var memberBloodGroups = SelectListItemExtension.PopulateDropdownList(_db.MemberBloodGroups.ToList<MemberBloodGroup>(), "Id", "BloodGroupName").ToList();
            var memberDistricts = SelectListItemExtension.PopulateDropdownList(_db.MemberDistricts.ToList<MemberDistrict>(), "Id", "DistrictName").ToList();
            var memberZones = new List<SelectListItem>() { new SelectListItem() { Selected = true, Text = "- Select -", Value = "0" } }.ToList();
            var memberHospitals = SelectListItemExtension.PopulateDropdownList(_db.MemberHospitals.ToList<MemberHospital>(), "Id", "HospitalName").ToList();
            var registerViewModel = new RegisterViewModel { ddlMemberBloodGroups = memberBloodGroups, ddlMemberDistricts = memberDistricts, ddlMemberHospitals = memberHospitals, ddlMemberZones = memberZones };

            return View(registerViewModel);
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            var memberBloodGroups = SelectListItemExtension.PopulateDropdownList(_db.MemberBloodGroups.ToList<MemberBloodGroup>(), "Id", "BloodGroupName").ToList();
            var memberDistricts = SelectListItemExtension.PopulateDropdownList(_db.MemberDistricts.ToList<MemberDistrict>(), "Id", "DistrictName").ToList();
            var memberZones = new List<SelectListItem>() { new SelectListItem() { Selected = true, Text = "- Select -", Value = "0" } }.ToList();
            var memberHospitals = SelectListItemExtension.PopulateDropdownList(_db.MemberHospitals.ToList<MemberHospital>(), "Id", "HospitalName").ToList();
            model.ddlMemberBloodGroups = memberBloodGroups;
            model.ddlMemberDistricts = memberDistricts;
            model.ddlMemberZones = memberZones;
            model.ddlMemberHospitals = memberHospitals;

            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    OSNB.Models.User user = _db.Users.Find(model.UserName);
                    OSNB.Models.Role role = _db.Roles.Find("User");
                    user.Roles = new List<Role> { role };

                    _db.Entry(user).State = EntityState.Modified;

                    OSNB.Models.Member member = new OSNB.Models.Member { FirstName = model.UserName, LastName = null, SurName = model.UserName, DateOfBirth = null, Address = null, PhoneNumber = null, MobileNumber = model.ContactNo, ThumbImageUrl = null, SmallImageUrl = null, UserName = model.UserName, MemberBloodGroupId = model.MemberBloodGroupId, MemberDistrictId = model.MemberDistrictId, MemberHospitalId = model.MemberHospitalId, MemberZoneId = model.MemberZoneId };

                    _db.Members.Add(member);
                    _db.SaveChanges();

                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //GetMemberZones
        public ActionResult GetMemberZones(int id)
        {
            var memberDistrict = _db.MemberDistricts.Find(id);
            var memberZones = new List<SelectListItem>() { new SelectListItem() { Selected = true, Text = "- Select -", Value = "0" } }.ToList();
            if (memberDistrict != null)
            {
                var memberZoneList = _db.MemberZones.Where(x => x.MemberDistrictId == memberDistrict.Id).ToList();
                memberZones = SelectListItemExtension.PopulateDropdownList(memberZoneList.ToList<MemberZone>(), "Id", "ZoneName").ToList();

                return PartialView("_ZoneList", memberZones);
            }
            else
            {
                return PartialView("_ZoneList", memberZones);
            }

        }


        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
