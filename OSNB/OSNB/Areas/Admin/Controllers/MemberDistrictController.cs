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
    public class MemberDistrictController : Controller
    {
        private AppDbContext _db = new AppDbContext();
        //
        // GET: /Admin/MemberDistrict/

        public ActionResult Index()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetMemberDistricts(DataTableParamModel param)
        {
            var memberDistricts = _db.MemberDistricts.ToList();

            var viewMemberDistricts = memberDistricts.Select(md => new MemberDistrictTableModel() { MemberDistrictId = Convert.ToString(md.Id), DistrictName = md.DistrictName });

            IEnumerable<MemberDistrictTableModel> filteredMemberDistricts;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredMemberDistricts = viewMemberDistricts.Where(md => (md.DistrictName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredMemberDistricts = viewMemberDistricts;
            }

            var viewOdjects = filteredMemberDistricts.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from mdMdl in viewOdjects
                         select new[] { mdMdl.MemberDistrictId, mdMdl.DistrictName, mdMdl.MemberDistrictId };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = memberDistricts.Count(),
                iTotalDisplayRecords = filteredMemberDistricts.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /MemberDistrict/Details/By ID

        public ActionResult Details(int id)
        {
            try
            {
                var memberDistrict = _db.MemberDistricts.Find(id);
                if (memberDistrict != null)
                {
                    var memberDistrictViewModel = new MemberDistrictViewModel { Id = memberDistrict.Id, DistrictName = memberDistrict.DistrictName };

                    return PartialView("_Details", memberDistrictViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberDistrict");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberDistrict");
            }
        }

        //
        // GET: /MemberDistrict/Add

        public ActionResult Add()
        {
            try
            {
                return PartialView("_Add");
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberDistrict");
            }
        }

        //
        // POST: /MemberDistrict/Add

        [HttpPost]
        public ActionResult Add(MemberDistrictViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var memberDistrict = new MemberDistrict { Id = viewModel.Id, DistrictName = viewModel.DistrictName };

                    _db.MemberDistricts.Add(memberDistrict);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to add this member district.");
            }

        }

        //
        // GET: /MemberDistrict/Edit/By ID

        public ActionResult Edit(int id)
        {
            try
            {
                var memberDistrict = _db.MemberDistricts.Find(id);
                if (memberDistrict != null)
                {
                    var memberDistrictViewModel = new MemberDistrictViewModel { Id = memberDistrict.Id, DistrictName = memberDistrict.DistrictName };

                    return PartialView("_Edit", memberDistrictViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberDistrict");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberDistrict");
            }
        }

        //
        // POST: /MemberDistrict/Edit/By ID

        [HttpPost]
        public ActionResult Edit(MemberDistrictViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var memberDistrict = new MemberDistrict { Id = viewModel.Id, DistrictName = viewModel.DistrictName };

                    _db.Entry(memberDistrict).State = EntityState.Modified;
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to edit this member district.");
            }
        }

        //
        // GET: /MemberDistrict/Delete/By ID

        public ActionResult Delete(int id)
        {
            try
            {
                var memberDistrict = _db.MemberDistricts.Find(id);
                if (memberDistrict != null)
                {
                    var memberDistrictViewModel = new MemberDistrictViewModel { Id = memberDistrict.Id, DistrictName = memberDistrict.DistrictName };

                    return PartialView("_Delete", memberDistrictViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberDistrict");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberDistrict");
            }
        }

        //
        // POST: /MemberDistrict/Delete/By ID

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var memberDistrict = _db.MemberDistricts.Find(id);
                if (memberDistrict != null)
                {
                    _db.MemberDistricts.Remove(memberDistrict);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to delete this member district.");
            }
        }

        //public PartialViewResult GetZones(string id)
        //{
        //    string userName = id;

        //    var user = _db.MemberDistricts.Where(x => x.MemberDistrictName == userName).FirstOrDefault();

        //    var roles = user.Roles.ToList();

        //    IEnumerable<RoleTableModel> productTableModels = roles.Count() == 0 ? null : (roles.Select(pro => new RoleTableModel
        //    {
        //        RoleName = pro.RoleName,
        //    }).ToList());


        //    return PartialView("_ZoneList", productTableModels);
        //}
    }
}
