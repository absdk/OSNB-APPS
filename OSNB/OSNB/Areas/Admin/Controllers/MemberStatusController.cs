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
    public class MemberStatusController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        //
        // GET: /Admin/MemberStatus/

        public ActionResult Index()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetMemberStatues(DataTableParamModel param)
        {
            var memberStatues = _db.MemberStatues.ToList();

            var viewMemberStatues = memberStatues.Select(ms => new MemberStatusTableModel() { MemberStatusId = Convert.ToString(ms.Id), MemberStatusTitle = ms.MemberStatusTitle, MemberStatusDescription = ms.MemberStatusDescription, MemberStatusIcon = ms.MemberStatusIcon });

            IEnumerable<MemberStatusTableModel> filteredMemberStatues;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredMemberStatues = viewMemberStatues.Where(ms => (ms.MemberStatusTitle ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredMemberStatues = viewMemberStatues;
            }

            var viewOdjects = filteredMemberStatues.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from mzMdl in viewOdjects
                         select new[] { mzMdl.MemberStatusTitle, mzMdl.MemberStatusDescription, mzMdl.MemberStatusIcon, mzMdl.MemberStatusId };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = memberStatues.Count(),
                iTotalDisplayRecords = filteredMemberStatues.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /MemberStatus/Details/By ID

        public ActionResult Details(int id)
        {
            try
            {
                var memberStatus = _db.MemberStatues.Find(id);
                if (memberStatus != null)
                {
                    var memberStatusViewModel = new MemberStatusViewModel { Id = memberStatus.Id, MemberStatusTitle = memberStatus.MemberStatusTitle, MemberStatusDescription = memberStatus.MemberStatusDescription, MemberStatusIcon = memberStatus.MemberStatusIcon };

                    return PartialView("_Details", memberStatusViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberStatus");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberStatus");
            }
        }

        //
        // GET: /MemberStatus/Add

        public ActionResult Add()
        {
            try
            {
                return PartialView("_Add");
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberStatus");
            }
        }

        //
        // POST: /MemberStatus/Add

        [HttpPost]
        public ActionResult Add(MemberStatusViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var memberStatus = new MemberStatus { Id = viewModel.Id, MemberStatusTitle = viewModel.MemberStatusTitle, MemberStatusDescription = viewModel.MemberStatusDescription, MemberStatusIcon = viewModel.MemberStatusIcon };

                    _db.MemberStatues.Add(memberStatus);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to add this member status.");
            }

        }

        //
        // GET: /MemberStatus/Edit/By ID

        public ActionResult Edit(int id)
        {
            try
            {
                var memberStatus = _db.MemberStatues.Find(id);
                if (memberStatus != null)
                {
                    var memberStatusViewModel = new MemberStatusViewModel { Id = memberStatus.Id, MemberStatusTitle = memberStatus.MemberStatusTitle, MemberStatusDescription = memberStatus.MemberStatusDescription, MemberStatusIcon = memberStatus.MemberStatusIcon };

                    return PartialView("_Edit", memberStatusViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberStatus");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberStatus");
            }
        }

        //
        // POST: /MemberStatus/Edit/By ID

        [HttpPost]
        public ActionResult Edit(MemberStatusViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var memberStatus = new MemberStatus { Id = viewModel.Id, MemberStatusTitle = viewModel.MemberStatusTitle, MemberStatusDescription = viewModel.MemberStatusDescription, MemberStatusIcon = viewModel.MemberStatusIcon };

                    _db.Entry(memberStatus).State = EntityState.Modified;
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to edit this member status.");
            }
        }

        //
        // GET: /MemberStatus/Delete/By ID

        public ActionResult Delete(int id)
        {
            try
            {
                var memberStatus = _db.MemberStatues.Find(id);
                if (memberStatus != null)
                {
                    var memberStatusViewModel = new MemberStatusViewModel { Id = memberStatus.Id, MemberStatusTitle = memberStatus.MemberStatusTitle, MemberStatusDescription = memberStatus.MemberStatusDescription, MemberStatusIcon = memberStatus.MemberStatusIcon };

                    return PartialView("_Delete", memberStatusViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberStatus");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberStatus");
            }
        }

        //
        // POST: /MemberStatus/Delete/By ID

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var memberStatus = _db.MemberStatues.Find(id);
                if (memberStatus != null)
                {
                    _db.MemberStatues.Remove(memberStatus);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to delete this member status.");
            }
        }
    }
}
