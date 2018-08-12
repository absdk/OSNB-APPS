using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSNB.Helpers;
using OSNB.Models;
using OSNB.ViewModels;
using OSNB.ViewModels.DataTableViewModels;

namespace OSNB.Areas.Admin.Controllers
{
    public class MemberController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        //
        // GET: /Admin/Member/

        public ActionResult Index()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetMembers(DataTableParamModel param)
        {
            var members = _db.Members.ToList();

            var viewMembers = members.Select(m => new MemberTableModel() { MemberId = Convert.ToString(m.Id), FullName = m.FullName, Address = m.Address, DateOfBirth = Convert.ToString(m.DateOfBirth), PhoneNumber = m.PhoneNumber, MobileNumber = m.MobileNumber, UserName = m.UserName, });

            IEnumerable<MemberTableModel> filteredMembers;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredMembers = viewMembers.Where(m => (m.FullName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredMembers = viewMembers;
            }

            var viewOdjects = filteredMembers.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from pMdl in viewOdjects
                         select new[] { pMdl.FullName, pMdl.Address, pMdl.DateOfBirth, pMdl.PhoneNumber, pMdl.MobileNumber, pMdl.UserName, pMdl.MemberId };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = members.Count(),
                iTotalDisplayRecords = filteredMembers.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Member/Details/By ID

        public ActionResult Details(int id)
        {
            try
            {
                var member = _db.Members.Find(id);
                if (member != null)
                {
                    var user = _db.Users.Find(member.UserName);

                    var memberViewModel = new CreateOrEditMemberViewModel { MemberId = member.Id, UserName = user.UserName, UserEmail = user.Email, FirstName = member.FirstName, LastName = member.LastName, SurName = member.SurName, DateOfBirth = member.DateOfBirth, Address = member.Address, PhoneNumber = member.PhoneNumber, MobileNumber = member.MobileNumber, ThumbImageUrl = member.ThumbImageUrl, SmallImageUrl = member.SmallImageUrl, MemberBloodGroupName = member.MemberBloodGroup != null ? member.MemberBloodGroup.BloodGroupName : null, MemberDistrictName = member.MemberDistrict != null ? member.MemberDistrict.DistrictName : null, MemberZoneName = member.MemberZone != null ? member.MemberZone.ZoneName : null, MemberHospitalName = member.MemberHospital != null ? member.MemberHospital.HospitalName : null };

                    return View(memberViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "Member");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "Member");
            }
        }

        //
        // GET: /Member/Edit/By ID

        public ActionResult Edit(int id)
        {
            try
            {
                var member = _db.Members.Find(id);
                if (member != null)
                {
                    var user = _db.Users.Find(member.UserName);

                    var memberBloodGroups = SelectListItemExtension.PopulateDropdownList(_db.MemberBloodGroups.ToList<MemberBloodGroup>(), "Id", "BloodGroupName", isEdit: true, selectedValue: member != null ? member.MemberBloodGroupId.ToString() : "0").ToList();
                    var memberDistricts = SelectListItemExtension.PopulateDropdownList(_db.MemberDistricts.ToList<MemberDistrict>(), "Id", "DistrictName", isEdit: true, selectedValue: member != null ? member.MemberDistrictId.ToString() : "0").ToList();
                    var memberZones = SelectListItemExtension.PopulateDropdownList(_db.MemberZones.ToList<MemberZone>(), "Id", "ZoneName", isEdit: true, selectedValue: member != null ? member.MemberZoneId.ToString() : "0").ToList();
                    var memberHospitals = SelectListItemExtension.PopulateDropdownList(_db.MemberHospitals.ToList<MemberHospital>(), "Id", "HospitalName", isEdit: true, selectedValue: member != null ? member.MemberHospitalId.ToString() : "0").ToList();

                    var memberViewModel = new CreateOrEditMemberViewModel { MemberId = member.Id, UserName = user.UserName, UserEmail = user.Email, FirstName = member.FirstName, LastName = member.LastName, SurName = member.SurName, DateOfBirth = member.DateOfBirth, Address = member.Address, PhoneNumber = member.PhoneNumber, MobileNumber = member.MobileNumber, ThumbImageUrl = member.ThumbImageUrl, SmallImageUrl = member.SmallImageUrl, ddlMemberBloodGroups = memberBloodGroups, ddlMemberDistricts = memberDistricts, ddlMemberZones = memberZones, ddlMemberHospitals = memberHospitals, MemberBloodGroupId = member.MemberBloodGroupId, MemberBloodGroupName = member.MemberBloodGroup != null ? member.MemberBloodGroup.BloodGroupName : null, MemberDistrictId = member.MemberDistrictId, MemberDistrictName = member.MemberDistrict != null ? member.MemberDistrict.DistrictName : null, MemberZoneId = member.MemberZoneId, MemberZoneName = member.MemberZone != null ? member.MemberZone.ZoneName : null, MemberHospitalId = member.MemberHospitalId, MemberHospitalName = member.MemberHospital != null ? member.MemberHospital.HospitalName : null };

                    return View(memberViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "Member");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "Member");
            }
        }

        //
        // POST: /Member/Edit/By ID

        [HttpPost]
        public ActionResult Edit(CreateOrEditMemberViewModel viewModel)
        {
            var memberBloodGroups = SelectListItemExtension.PopulateDropdownList(_db.MemberBloodGroups.ToList<MemberBloodGroup>(), "Id", "BloodGroupName", isEdit: true, selectedValue: viewModel.MemberBloodGroupId != 0 ? viewModel.MemberBloodGroupId.ToString() : "0").ToList();
            var memberDistricts = SelectListItemExtension.PopulateDropdownList(_db.MemberDistricts.ToList<MemberDistrict>(), "Id", "DistrictName", isEdit: true, selectedValue: viewModel.MemberDistrictId != 0 ? viewModel.MemberDistrictId.ToString() : "0").ToList();
            var memberZones = SelectListItemExtension.PopulateDropdownList(_db.MemberZones.ToList<MemberZone>(), "Id", "ZoneName", isEdit: true, selectedValue: viewModel.MemberZoneId != 0 ? viewModel.MemberZoneId.ToString() : "0").ToList();
            var memberHospitals = SelectListItemExtension.PopulateDropdownList(_db.MemberHospitals.ToList<MemberHospital>(), "Id", "HospitalName", isEdit: true, selectedValue: viewModel.MemberHospitalId != 0 ? viewModel.MemberHospitalId.ToString() : "0").ToList();

            viewModel.ddlMemberBloodGroups = memberBloodGroups;
            viewModel.ddlMemberDistricts = memberDistricts;
            viewModel.ddlMemberZones = memberZones;
            viewModel.ddlMemberHospitals = memberHospitals;

            try
            {
                if (ModelState.IsValid)
                {
                    var member = new OSNB.Models.Member { Id = viewModel.MemberId, FirstName = viewModel.FirstName, LastName = viewModel.LastName, SurName = viewModel.SurName, DateOfBirth = viewModel.DateOfBirth, Address = viewModel.Address, PhoneNumber = viewModel.PhoneNumber, MobileNumber = viewModel.MobileNumber, ThumbImageUrl = viewModel.ThumbImageUrl, SmallImageUrl = viewModel.SmallImageUrl, MemberBloodGroupId = viewModel.MemberBloodGroupId, MemberDistrictId = viewModel.MemberDistrictId == 0 ? 1 : viewModel.MemberDistrictId, MemberZoneId = viewModel.MemberZoneId, MemberHospitalId = viewModel.MemberHospitalId, UserName = viewModel.UserName };

                    _db.Entry(member).State = EntityState.Modified;
                    _db.SaveChanges();

                    return RedirectToAction("Index", "Member");
                    //return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to edit this member.");
            }
        }

        //
        // GET: /Member/Delete/By ID

        public ActionResult Delete(int id)
        {
            try
            {
                var member = _db.Members.Find(id);
                if (member != null)
                {
                    var memberViewModel = new MemberViewModel { MemberId = member.Id, FirstName = member.FirstName, LastName = member.LastName, SurName = member.SurName, DateOfBirth = member.DateOfBirth, Address = member.Address, PhoneNumber = member.PhoneNumber, MobileNumber = member.MobileNumber, ThumbImageUrl = member.ThumbImageUrl, SmallImageUrl = member.SmallImageUrl, MemberBloodGroupId = member.MemberBloodGroupId, MemberBloodGroupName = member.MemberBloodGroup != null ? member.MemberBloodGroup.BloodGroupName : null, MemberDistrictId = member.MemberDistrictId, MemberDistrictName = member.MemberDistrict != null ? member.MemberDistrict.DistrictName : null, MemberZoneId = member.MemberZoneId, MemberZoneName = member.MemberZone != null ? member.MemberZone.ZoneName : null, MemberHospitalId = member.MemberHospitalId, MemberHospitalName = member.MemberHospital != null ? member.MemberHospital.HospitalName : null, UserName = member.UserName };

                    return PartialView("_Delete", memberViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "Member");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "Member");
            }
        }

        //
        // POST: /Member/Delete/By ID

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var member = _db.Members.Find(id);
                if (member != null)
                {
                    _db.Members.Remove(member);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to delete this member.");
            }
        }

    }
}
