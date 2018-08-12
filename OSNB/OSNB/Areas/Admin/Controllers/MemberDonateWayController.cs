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
    public class MemberDonateWayController : Controller
    {

        private AppDbContext _db = new AppDbContext();

        //
        // GET: /Admin/MemberDonateWay/

        public ActionResult Index()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetMemberDonateWays(DataTableParamModel param)
        {
            var memberDonateWays = _db.MemberDonateWays.ToList();

            var viewMemberDonateWays = memberDonateWays.Select(mdw => new MemberDonateWayTableModel() { MemberDonateWayId = Convert.ToString(mdw.Id), DonateWayName = mdw.DonateWayName });

            IEnumerable<MemberDonateWayTableModel> filteredMemberDonateWays;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredMemberDonateWays = viewMemberDonateWays.Where(mdw => (mdw.DonateWayName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredMemberDonateWays = viewMemberDonateWays;
            }

            var viewOdjects = filteredMemberDonateWays.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from mdwMdl in viewOdjects
                         select new[] { mdwMdl.DonateWayName, mdwMdl.MemberDonateWayId };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = memberDonateWays.Count(),
                iTotalDisplayRecords = filteredMemberDonateWays.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /MemberDonateWay/Details/By ID

        public ActionResult Details(int id)
        {
            try
            {
                var memberDonateWay = _db.MemberDonateWays.Find(id);
                if (memberDonateWay != null)
                {
                    var memberDonateWayViewModel = new MemberDonateWayViewModel { Id = memberDonateWay.Id, DonateWayName = memberDonateWay.DonateWayName };

                    return PartialView("_Details", memberDonateWayViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberDonateWay");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberDonateWay");
            }
        }

        //
        // GET: /MemberDonateWay/Add

        public ActionResult Add()
        {
            try
            {
                return PartialView("_Add");
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberDonateWay");
            }
        }

        //
        // POST: /MemberDonateWay/Add

        [HttpPost]
        public ActionResult Add(MemberDonateWayViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var memberDonateWay = new MemberDonateWay { Id = viewModel.Id, DonateWayName = viewModel.DonateWayName };

                    _db.MemberDonateWays.Add(memberDonateWay);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to add this member donate way.");
            }

        }

        //
        // GET: /MemberDonateWay/Edit/By ID

        public ActionResult Edit(int id)
        {
            try
            {
                var memberDonateWay = _db.MemberDonateWays.Find(id);
                if (memberDonateWay != null)
                {
                    var memberDonateWayViewModel = new MemberDonateWayViewModel { Id = memberDonateWay.Id, DonateWayName = memberDonateWay.DonateWayName };

                    return PartialView("_Edit", memberDonateWayViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberDonateWay");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberDonateWay");
            }
        }

        //
        // POST: /MemberDonateWay/Edit/By ID

        [HttpPost]
        public ActionResult Edit(MemberDonateWayViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var memberDonateWay = new MemberDonateWay { Id = viewModel.Id, DonateWayName = viewModel.DonateWayName };

                    _db.Entry(memberDonateWay).State = EntityState.Modified;
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to edit this member donate way.");
            }
        }

        //
        // GET: /MemberDonateWay/Delete/By ID

        public ActionResult Delete(int id)
        {
            try
            {
                var memberDonateWay = _db.MemberDonateWays.Find(id);
                if (memberDonateWay != null)
                {
                    var memberDonateWayViewModel = new MemberDonateWayViewModel { Id = memberDonateWay.Id, DonateWayName = memberDonateWay.DonateWayName };

                    return PartialView("_Delete", memberDonateWayViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberDonateWay");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberDonateWay");
            }
        }

        //
        // POST: /MemberDonateWay/Delete/By ID

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var memberDonateWay = _db.MemberDonateWays.Find(id);
                if (memberDonateWay != null)
                {
                    _db.MemberDonateWays.Remove(memberDonateWay);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to delete this member donate way.");
            }
        }
    }
}
