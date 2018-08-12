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
    public class MemberHospitalController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        //
        // GET: /Admin/MemberHospital/

        public ActionResult Index()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetMemberHospitals(DataTableParamModel param)
        {
            var memberHospitals = _db.MemberHospitals.ToList();

            var viewMemberHospitals = memberHospitals.Select(mh => new MemberHospitalTableModel() { MemberHospitalId = Convert.ToString(mh.Id), HospitalName = mh.HospitalName, Address = mh.Address, LocationX = mh.LocationX, LocationY = mh.LocationY, MemberZoneId = Convert.ToString(mh.MemberZoneId), MemberZoneName = mh.MemberZone != null ? mh.MemberZone.ZoneName : null });

            IEnumerable<MemberHospitalTableModel> filteredMemberHospitals;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredMemberHospitals = viewMemberHospitals.Where(mh => (mh.HospitalName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredMemberHospitals = viewMemberHospitals;
            }

            var viewOdjects = filteredMemberHospitals.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from mzMdl in viewOdjects
                         select new[] { mzMdl.HospitalName, mzMdl.Address, mzMdl.LocationX, mzMdl.LocationY, mzMdl.MemberZoneName, mzMdl.MemberHospitalId };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = memberHospitals.Count(),
                iTotalDisplayRecords = filteredMemberHospitals.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /MemberHospital/Details/By ID

        public ActionResult Details(int id)
        {
            try
            {
                var memberHospital = _db.MemberHospitals.Find(id);
                if (memberHospital != null)
                {
                    var memberHospitalViewModel = new MemberHospitalViewModel { Id = memberHospital.Id, HospitalName = memberHospital.HospitalName, LocationX = memberHospital.LocationX, LocationY = memberHospital.LocationY, MemberZoneId = memberHospital.MemberZoneId, MemberZoneName = memberHospital.MemberZone != null ? memberHospital.MemberZone.ZoneName : null };

                    return PartialView("_Details", memberHospitalViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberHospital");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberHospital");
            }
        }

        //
        // GET: /MemberHospital/Add

        public ActionResult Add()
        {
            try
            {
                var memberZones = SelectListItemExtension.PopulateDropdownList(_db.MemberZones.ToList<MemberZone>(), "Id", "ZoneName").ToList();

                var memberHospitalViewModel = new MemberHospitalViewModel { ddlMemberZones = memberZones };

                return PartialView("_Add", memberHospitalViewModel);
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberHospital");
            }
        }

        //
        // POST: /MemberHospital/Add

        [HttpPost]
        public ActionResult Add(MemberHospitalViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var memberHospital = new MemberHospital { Id = viewModel.Id, HospitalName = viewModel.HospitalName, Address = viewModel.Address, LocationX = viewModel.LocationX, LocationY = viewModel.LocationY, MemberZoneId = viewModel.MemberZoneId };

                    _db.MemberHospitals.Add(memberHospital);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to add this member hospital.");
            }

        }

        //
        // GET: /MemberHospital/Edit/By ID

        public ActionResult Edit(int id)
        {
            try
            {
                var memberHospital = _db.MemberHospitals.Find(id);
                if (memberHospital != null)
                {
                    var memberZones = SelectListItemExtension.PopulateDropdownList(_db.MemberZones.ToList<MemberZone>(), "Id", "ZoneName", isEdit: true, selectedValue: memberHospital != null ? memberHospital.MemberZoneId.ToString() : "0").ToList();

                    var memberHospitalViewModel = new MemberHospitalViewModel { Id = memberHospital.Id, HospitalName = memberHospital.HospitalName, Address = memberHospital.Address, LocationX = memberHospital.LocationX, LocationY = memberHospital.LocationY, MemberZoneId = memberHospital.MemberZoneId, ddlMemberZones = memberZones };

                    return PartialView("_Edit", memberHospitalViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberHospital");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberHospital");
            }
        }

        //
        // POST: /MemberHospital/Edit/By ID

        [HttpPost]
        public ActionResult Edit(MemberHospitalViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var memberHospital = new MemberHospital { Id = viewModel.Id, HospitalName = viewModel.HospitalName, Address = viewModel.Address, LocationX = viewModel.LocationX, LocationY = viewModel.LocationY, MemberZoneId = viewModel.MemberZoneId };

                    _db.Entry(memberHospital).State = EntityState.Modified;
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to edit this member hospital.");
            }
        }

        //
        // GET: /MemberHospital/Delete/By ID

        public ActionResult Delete(int id)
        {
            try
            {
                var memberHospital = _db.MemberHospitals.Find(id);
                if (memberHospital != null)
                {
                    var memberHospitalViewModel = new MemberHospitalViewModel { Id = memberHospital.Id, HospitalName = memberHospital.HospitalName, Address = memberHospital.Address, LocationX = memberHospital.LocationX, LocationY = memberHospital.LocationY };

                    return PartialView("_Delete", memberHospitalViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberHospital");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberHospital");
            }
        }

        //
        // POST: /MemberHospital/Delete/By ID

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var memberHospital = _db.MemberHospitals.Find(id);
                if (memberHospital != null)
                {
                    _db.MemberHospitals.Remove(memberHospital);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to delete this member hospital.");
            }
        }

    }
}
