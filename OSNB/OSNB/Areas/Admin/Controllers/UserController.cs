using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OSNB.Helpers;
using OSNB.Models;
using OSNB.ViewModels;
using OSNB.ViewModels.DataTableViewModels;
using System.IO;

namespace OSNB.Areas.Admin.Controllers
{
    public class UserController : Controller
    {

        private AppDbContext _db = new AppDbContext();
        //
        // GET: /Admin/User/

        public ActionResult Index()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetUsers(DataTableParamModel param)
        {
            var users = _db.Users.ToList();

            var viewUsers = users.Select(usr => new UserTableModel() { UserId = Convert.ToString(usr.UserName), UserName = usr.UserName, Email = usr.Email, IsApproved = usr.IsApproved == true ? "Yes" : "No" });

            IEnumerable<UserTableModel> filteredUsers;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredUsers = viewUsers.Where(usr => (usr.UserName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredUsers = viewUsers;
            }

            var viewOdjects = filteredUsers.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from usrMdl in viewOdjects
                         select new[] { usrMdl.UserName, usrMdl.UserName, usrMdl.Email, usrMdl.IsApproved, usrMdl.UserName };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = users.Count(),
                iTotalDisplayRecords = filteredUsers.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /User/Details/By ID

        public ActionResult Details(string id)
        {
            try
            {
                var user = _db.Users.Find(id);
                if (user != null)
                {
                    var roles = user.Roles.ToList().Select(x => new AssignRoleModel { RoleName = x.RoleName });

                    var userViewModel = new UserViewModel { UserName = user.UserName, Email = user.Email, Roles = roles };

                    //return View(userViewModel);
                    return PartialView("_Details", userViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "User");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "User");
            }
        }

        //
        // GET: /User/Add

        public ActionResult Add()
        {
            try
            {
                var roles = _db.Roles.ToList().Select(x => new AssignRoleModel { RoleName = x.RoleName });

                var userViewModel = new UserViewModel { Roles = roles };

                return PartialView("_Add", userViewModel);
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "User");
            }
        }

        //
        // POST: /User/Add

        [HttpPost]
        public ActionResult Add(UserViewModel viewModel, string[] RoleName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CreateUserWithRole(viewModel.UserName, viewModel.Password, viewModel.Email, RoleName);

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to add this user.");
            }

        }

        //
        // GET: /User/Edit/By ID

        public ActionResult Edit(string id)
        {
            try
            {
                var user = _db.Users.Find(id);
                if (user != null)
                {
                    var roles = _db.Roles.ToList().Select(x => new AssignRoleModel { RoleName = x.RoleName });

                    //var editUserViewModel = new EditUserViewModel { UserName = user.UserName, Email = user.Email, OldPassword = user.PasswordHash, NewPassword = user.PasswordHash, ConfirmPassword = user.PasswordHash, ddlRoles = roles };
                    var editUserViewModel = new EditUserViewModel { UserName = user.UserName, Email = user.Email, Roles = roles };

                    return PartialView("_Edit", editUserViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "User");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "User");
            }
        }

        //
        // POST: /User/Edit/By ID

        [HttpPost]
        public ActionResult Edit(EditUserViewModel viewModel, string[] RoleName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UpdateUserWithRole(viewModel, RoleName);

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to edit this user.");
            }
        }

        //
        // GET: /User/Delete/By ID

        public ActionResult Delete(string id)
        {
            try
            {
                var user = _db.Users.Find(id);
                if (user != null)
                {
                    var userViewModel = new UserViewModel { UserName = user.UserName, Email = user.Email };

                    return PartialView("_Delete", userViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "User");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "User");
            }
        }

        //
        // POST: /User/Delete/By ID

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                User user = _db.Users.Find(id);
                if (user != null)
                {
                    _db.Users.Remove(user);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to delete this user.");
            }
        }

        public PartialViewResult GetRoles(string id)
        {
            string userName = id;

            var user = _db.Users.Where(x => x.UserName == userName).FirstOrDefault();

            var roles = user.Roles.ToList();

            IEnumerable<RoleTableModel> productTableModels = roles.Count() == 0 ? null : (roles.Select(pro => new RoleTableModel
            {
                RoleName = pro.RoleName,
            }).ToList());


            return PartialView("_RoleList", productTableModels);
        }

        public ActionResult AssignRole(string id)
        {
            string userName = id;

            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    var roles = _db.Roles.ToList();

                    UserRoleViewModel userRoleModel = new UserRoleViewModel { UserName = userName };

                    var assignRoles = roles.Count() == 0 ? null : (roles.Select(role => new AssignRoleModel
                    {
                        RoleName = role.RoleName,
                        IsAssigned = role.Users.Where(x => x.UserName == userName).Count() == 0 ? false : true
                    }).ToList());

                    userRoleModel.AssignRoles = assignRoles;

                    return PartialView("_AssignRole", userRoleModel);

                }
                else
                {
                    return RedirectToAction("Index", "User");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "User");
            }

        }

        [HttpPost]
        public ActionResult AssignRole(string userName, string[] roleName)
        {
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    User user = _db.Users.Find(userName);

                    if (user != null)
                    {

                        var roles = new List<Role>();

                        foreach (var item in roleName)
                        {
                            var role = _db.Roles.Find(item);
                            roles.Add(role);
                        }

                        user.Roles = roles;
                        _db.SaveChanges();

                        return Content(Boolean.TrueString);

                    }
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to edit this user.");
            }
        }

        public ActionResult AddMember(string id)
        {
            try
            {
                var user = _db.Users.Find(id);
                if (user != null)
                {
                    var memberBloodGroups = SelectListItemExtension.PopulateDropdownList(_db.MemberBloodGroups.ToList<MemberBloodGroup>(), "Id", "BloodGroupName").ToList();
                    var memberDistricts = SelectListItemExtension.PopulateDropdownList(_db.MemberDistricts.ToList<MemberDistrict>(), "Id", "DistrictName").ToList();
                    var memberZones = SelectListItemExtension.PopulateDropdownList(_db.MemberZones.ToList<MemberZone>(), "Id", "ZoneName").ToList();
                    var memberHospitals = SelectListItemExtension.PopulateDropdownList(_db.MemberHospitals.ToList<MemberHospital>(), "Id", "HospitalName").ToList();
                    var memberStatues = SelectListItemExtension.PopulateDropdownList(_db.MemberStatues.ToList<MemberStatus>(), "Id", "MemberStatusTitle").ToList();

                    var memberViewModel = new CreateOrEditMemberViewModel { UserName = user.UserName, UserEmail = user.Email, ddlMemberBloodGroups = memberBloodGroups, ddlMemberDistricts = memberDistricts, ddlMemberZones = memberZones, ddlMemberHospitals = memberHospitals, ddlMemberStatus = memberStatues };

                    return View(memberViewModel);

                }
                else
                {
                    return RedirectToAction("Index", "User");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "User");
            }

        }

        [HttpPost]
        public ActionResult AddMember(CreateOrEditMemberViewModel viewModel)
        {

            var memberBloodGroups = SelectListItemExtension.PopulateDropdownList(_db.MemberBloodGroups.ToList<MemberBloodGroup>(), "Id", "BloodGroupName", isEdit: true, selectedValue: viewModel.MemberBloodGroupId != 0 ? viewModel.MemberBloodGroupId.ToString() : "0").ToList();
            var memberDistricts = SelectListItemExtension.PopulateDropdownList(_db.MemberDistricts.ToList<MemberDistrict>(), "Id", "DistrictName", isEdit: true, selectedValue: viewModel.MemberDistrictId != 0 ? viewModel.MemberDistrictId.ToString() : "0").ToList();
            var memberZones = SelectListItemExtension.PopulateDropdownList(_db.MemberZones.ToList<MemberZone>(), "Id", "ZoneName", isEdit: true, selectedValue: viewModel.MemberZoneId != 0 ? viewModel.MemberZoneId.ToString() : "0").ToList();
            var memberHospitals = SelectListItemExtension.PopulateDropdownList(_db.MemberHospitals.ToList<MemberHospital>(), "Id", "HospitalName", isEdit: true, selectedValue: viewModel.MemberHospitalId != 0 ? viewModel.MemberHospitalId.ToString() : "0").ToList();
            var memberStatues = SelectListItemExtension.PopulateDropdownList(_db.MemberStatues.ToList<MemberStatus>(), "Id", "MemberStatusTitle").ToList();

            viewModel.ddlMemberBloodGroups = memberBloodGroups;
            viewModel.ddlMemberDistricts = memberDistricts;
            viewModel.ddlMemberZones = memberZones;
            viewModel.ddlMemberHospitals = memberHospitals;
            viewModel.ddlMemberStatus = memberStatues;

            try
            {
                if (ModelState.IsValid)
                {
                    var file = viewModel.ImageFile;

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var physicalPath = Path.Combine(Server.MapPath("~/Images"), fileName);

                        // The files are not actually saved in this demo
                        file.SaveAs(physicalPath);

                        string imageUrl = @"/Images/" + fileName;

                        viewModel.SmallImageUrl = imageUrl;
                        viewModel.ThumbImageUrl = imageUrl;
                    }

                    var memberStatus = _db.MemberStatues.SingleOrDefault(x => x.Id == viewModel.MemberStatusId);
                    var memberStatusList = new List<MemberStatus>();
                    memberStatusList.Add(memberStatus);

                    OSNB.Models.Member model = new OSNB.Models.Member { FirstName = viewModel.FirstName, LastName = viewModel.LastName, SurName = viewModel.SurName, DateOfBirth = viewModel.DateOfBirth, Address = viewModel.Address, PhoneNumber = viewModel.PhoneNumber, MobileNumber = viewModel.MobileNumber, ThumbImageUrl = viewModel.ThumbImageUrl, SmallImageUrl = viewModel.SmallImageUrl, UserName = viewModel.UserName, MemberBloodGroupId = viewModel.MemberBloodGroupId, MemberDistrictId = viewModel.MemberDistrictId == 0 ? 1 : viewModel.MemberDistrictId, MemberZoneId = viewModel.MemberZoneId, MemberHospitalId = viewModel.MemberHospitalId, MemberStatues = memberStatusList };

                    _db.Members.Add(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "Member");
                }

                //return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return View(viewModel);
            }

        }


        private int CreateUserWithRole(string username, string password, string email, string[] rolenames)
        {
            var status = new MembershipCreateStatus();

            Membership.CreateUser(username, password, email);
            if (status == MembershipCreateStatus.Success)
            {
                // Add the role.
                var user = _db.Users.Find(username);

                var roles = new List<Role>();

                foreach (var rolename in rolenames)
                {
                    var role = _db.Roles.Find(rolename);
                    roles.Add(role);
                }

                user.Roles = roles;
            }

            return _db.SaveChanges();
        }

        private int UpdateUserWithRole(EditUserViewModel model, string[] rolenames)
        {
            bool changePasswordSucceeded;

            MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
            changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);

            if (changePasswordSucceeded)
            {
                // Add the role.
                var user = _db.Users.Find(model.UserName);

                var roles = new List<Role>();

                foreach (var rolename in rolenames)
                {
                    var role = _db.Roles.Find(rolename);
                    roles.Add(role);
                }

                user.Roles = roles;
            }

            return _db.SaveChanges();
        }

    }
}
