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
    public class MemberBloodGroupController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        //
        // GET: /Admin/MemberBloodGroup/

        public ActionResult Index()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetMemberBloodGroups(DataTableParamModel param)
        {
            var memberBloodGroups = _db.MemberBloodGroups.ToList();

            var viewMemberBloodGroups = memberBloodGroups.Select(md => new MemberBloodGroupTableModel() { MemberBloodGroupId = Convert.ToString(md.Id), BloodGroupName = md.BloodGroupName });

            IEnumerable<MemberBloodGroupTableModel> filteredMemberBloodGroups;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredMemberBloodGroups = viewMemberBloodGroups.Where(mbg => (mbg.BloodGroupName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredMemberBloodGroups = viewMemberBloodGroups;
            }

            var viewOdjects = filteredMemberBloodGroups.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from mbgMdl in viewOdjects
                         select new[] { mbgMdl.BloodGroupName, mbgMdl.MemberBloodGroupId };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = memberBloodGroups.Count(),
                iTotalDisplayRecords = filteredMemberBloodGroups.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /MemberBloodGroup/Details/By ID

        public ActionResult Details(int id)
        {
            try
            {
                var memberBloodGroup = _db.MemberBloodGroups.Find(id);
                if (memberBloodGroup != null)
                {
                    var memberBloodGroupViewModel = new MemberBloodGroupViewModel { Id = memberBloodGroup.Id, BloodGroupName = memberBloodGroup.BloodGroupName };

                    return PartialView("_Details", memberBloodGroupViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberBloodGroup");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberBloodGroup");
            }
        }

        //
        // GET: /MemberBloodGroup/Add

        public ActionResult Add()
        {
            try
            {
                return PartialView("_Add");
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberBloodGroup");
            }
        }

        //
        // POST: /MemberBloodGroup/Add

        [HttpPost]
        public ActionResult Add(MemberBloodGroupViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var memberBloodGroup = new MemberBloodGroup { Id = viewModel.Id, BloodGroupName = viewModel.BloodGroupName };

                    _db.MemberBloodGroups.Add(memberBloodGroup);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to add this member blood group.");
            }

        }

        //
        // GET: /MemberBloodGroup/Edit/By ID

        public ActionResult Edit(int id)
        {
            try
            {
                var memberBloodGroup = _db.MemberBloodGroups.Find(id);
                if (memberBloodGroup != null)
                {
                    var memberBloodGroupViewModel = new MemberBloodGroupViewModel { Id = memberBloodGroup.Id, BloodGroupName = memberBloodGroup.BloodGroupName };

                    return PartialView("_Edit", memberBloodGroupViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberBloodGroup");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberBloodGroup");
            }
        }

        //
        // POST: /MemberBloodGroup/Edit/By ID

        [HttpPost]
        public ActionResult Edit(MemberBloodGroupViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var memberBloodGroup = new MemberBloodGroup { Id = viewModel.Id, BloodGroupName = viewModel.BloodGroupName };

                    _db.Entry(memberBloodGroup).State = EntityState.Modified;
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to edit this member blood group.");
            }
        }

        //
        // GET: /MemberBloodGroup/Delete/By ID

        public ActionResult Delete(int id)
        {
            try
            {
                var memberBloodGroup = _db.MemberBloodGroups.Find(id);
                if (memberBloodGroup != null)
                {
                    var memberBloodGroupViewModel = new MemberBloodGroupViewModel { Id = memberBloodGroup.Id, BloodGroupName = memberBloodGroup.BloodGroupName };

                    return PartialView("_Delete", memberBloodGroupViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberBloodGroup");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberBloodGroup");
            }
        }

        //
        // POST: /MemberBloodGroup/Delete/By ID

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var memberBloodGroup = _db.MemberBloodGroups.Find(id);
                if (memberBloodGroup != null)
                {
                    _db.MemberBloodGroups.Remove(memberBloodGroup);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to delete this member blood group.");
            }
        }

    }
}
