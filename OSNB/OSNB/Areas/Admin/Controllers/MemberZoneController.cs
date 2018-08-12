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
    public class MemberZoneController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        //
        // GET: /Admin/MemberZone/

        public ActionResult Index()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetMemberZones(DataTableParamModel param)
        {
            var memberZones = _db.MemberZones.ToList();

            var viewMemberZones = memberZones.Select(mz => new MemberZoneTableModel() { MemberZoneId = Convert.ToString(mz.Id), ZoneName = mz.ZoneName, LocationX = mz.LocationX, LocationY = mz.LocationY, MemberDistrictId = Convert.ToString(mz.MemberDistrictId), MemberDistrictName = mz.MemberDistrict != null ? mz.MemberDistrict.DistrictName : null });

            IEnumerable<MemberZoneTableModel> filteredMemberZones;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredMemberZones = viewMemberZones.Where(md => (md.ZoneName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredMemberZones = viewMemberZones;
            }

            var viewOdjects = filteredMemberZones.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from mzMdl in viewOdjects
                         select new[] { mzMdl.ZoneName, mzMdl.LocationX, mzMdl.LocationY, mzMdl.MemberDistrictName, mzMdl.MemberZoneId };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = memberZones.Count(),
                iTotalDisplayRecords = filteredMemberZones.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /MemberZone/Details/By ID

        public ActionResult Details(int id)
        {
            try
            {
                var memberZone = _db.MemberZones.Find(id);
                if (memberZone != null)
                {
                    var memberZoneViewModel = new MemberZoneViewModel { Id = memberZone.Id, ZoneName = memberZone.ZoneName, LocationX = memberZone.LocationX, LocationY = memberZone.LocationY, MemberDistrictId = memberZone.MemberDistrictId, MemberDistrictName = memberZone.MemberDistrict != null ? memberZone.MemberDistrict.DistrictName : null };

                    return PartialView("_Details", memberZoneViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberZone");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberZone");
            }
        }

        //
        // GET: /MemberZone/Add

        public ActionResult Add()
        {
            try
            {
                var memberDistricts = SelectListItemExtension.PopulateDropdownList(_db.MemberDistricts.ToList<MemberDistrict>(), "Id", "DistrictName").ToList();

                var memberZoneViewModel = new MemberZoneViewModel { ddlMemberDistricts = memberDistricts };

                return PartialView("_Add", memberZoneViewModel);
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberZone");
            }
        }

        //
        // POST: /MemberZone/Add

        [HttpPost]
        public ActionResult Add(MemberZoneViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var memberZone = new MemberZone { Id = viewModel.Id, ZoneName = viewModel.ZoneName, LocationX = viewModel.LocationX, LocationY = viewModel.LocationY, MemberDistrictId = viewModel.MemberDistrictId };

                    _db.MemberZones.Add(memberZone);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to add this member zone.");
            }

        }

        //
        // GET: /MemberZone/Edit/By ID

        public ActionResult Edit(int id)
        {
            try
            {
                var memberZone = _db.MemberZones.Find(id);
                if (memberZone != null)
                {
                    var memberDistricts = SelectListItemExtension.PopulateDropdownList(_db.MemberDistricts.ToList<MemberDistrict>(), "Id", "DistrictName", isEdit: true, selectedValue: memberZone != null ? memberZone.MemberDistrictId.ToString() : "0").ToList();

                    var memberZoneViewModel = new MemberZoneViewModel { Id = memberZone.Id, ZoneName = memberZone.ZoneName, LocationX = memberZone.LocationX, LocationY = memberZone.LocationY, MemberDistrictId = memberZone.MemberDistrictId, ddlMemberDistricts = memberDistricts };

                    return PartialView("_Edit", memberZoneViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberZone");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberZone");
            }
        }

        //
        // POST: /MemberZone/Edit/By ID

        [HttpPost]
        public ActionResult Edit(MemberZoneViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var memberZone = new MemberZone { Id = viewModel.Id, ZoneName = viewModel.ZoneName, LocationX = viewModel.LocationX, LocationY = viewModel.LocationY, MemberDistrictId = viewModel.MemberDistrictId };

                    _db.Entry(memberZone).State = EntityState.Modified;
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to edit this member zone.");
            }
        }

        //
        // GET: /MemberZone/Delete/By ID

        public ActionResult Delete(int id)
        {
            try
            {
                var memberZone = _db.MemberZones.Find(id);
                if (memberZone != null)
                {
                    var memberZoneViewModel = new MemberZoneViewModel { Id = memberZone.Id, ZoneName = memberZone.ZoneName, LocationX = memberZone.LocationX, LocationY = memberZone.LocationY };

                    return PartialView("_Delete", memberZoneViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberZone");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberZone");
            }
        }

        //
        // POST: /MemberZone/Delete/By ID

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var memberZone = _db.MemberZones.Find(id);
                if (memberZone != null)
                {
                    _db.MemberZones.Remove(memberZone);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to delete this member zone.");
            }
        }

    }
}
