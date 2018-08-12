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
    public class MemberDonateTypeController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        //
        // GET: /Admin/MemberDonateType/

        public ActionResult Index()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetMemberDonateTypes(DataTableParamModel param)
        {
            var memberDonateTypes = _db.MemberDonateTypes.ToList();

            var viewMemberDonateTypes = memberDonateTypes.Select(mdt => new MemberDonateTypeTableModel() { MemberDonateTypeId = Convert.ToString(mdt.Id), DonateTypeName = mdt.DonateTypeName });

            IEnumerable<MemberDonateTypeTableModel> filteredMemberDonateTypes;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredMemberDonateTypes = viewMemberDonateTypes.Where(mbg => (mbg.DonateTypeName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredMemberDonateTypes = viewMemberDonateTypes;
            }

            var viewOdjects = filteredMemberDonateTypes.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from mdtMdl in viewOdjects
                         select new[] { mdtMdl.DonateTypeName, mdtMdl.MemberDonateTypeId };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = memberDonateTypes.Count(),
                iTotalDisplayRecords = filteredMemberDonateTypes.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /MemberDonateType/Details/By ID

        public ActionResult Details(int id)
        {
            try
            {
                var memberDonateType = _db.MemberDonateTypes.Find(id);
                if (memberDonateType != null)
                {
                    var memberDonateTypeViewModel = new MemberDonateTypeViewModel { Id = memberDonateType.Id, DonateTypeName = memberDonateType.DonateTypeName };

                    return PartialView("_Details", memberDonateTypeViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberDonateType");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberDonateType");
            }
        }

        //
        // GET: /MemberDonateType/Add

        public ActionResult Add()
        {
            try
            {
                return PartialView("_Add");
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberDonateType");
            }
        }

        //
        // POST: /MemberDonateType/Add

        [HttpPost]
        public ActionResult Add(MemberDonateTypeViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var memberDonateType = new MemberDonateType { Id = viewModel.Id, DonateTypeName = viewModel.DonateTypeName };

                    _db.MemberDonateTypes.Add(memberDonateType);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to add this member donate type.");
            }

        }

        //
        // GET: /MemberDonateType/Edit/By ID

        public ActionResult Edit(int id)
        {
            try
            {
                var memberDonateType = _db.MemberDonateTypes.Find(id);
                if (memberDonateType != null)
                {
                    var memberDonateTypeViewModel = new MemberDonateTypeViewModel { Id = memberDonateType.Id, DonateTypeName = memberDonateType.DonateTypeName };

                    return PartialView("_Edit", memberDonateTypeViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberDonateType");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberDonateType");
            }
        }

        //
        // POST: /MemberDonateType/Edit/By ID

        [HttpPost]
        public ActionResult Edit(MemberDonateTypeViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var memberDonateType = new MemberDonateType { Id = viewModel.Id, DonateTypeName = viewModel.DonateTypeName };

                    _db.Entry(memberDonateType).State = EntityState.Modified;
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to edit this member donate type.");
            }
        }

        //
        // GET: /MemberDonateType/Delete/By ID

        public ActionResult Delete(int id)
        {
            try
            {
                var memberDonateType = _db.MemberDonateTypes.Find(id);
                if (memberDonateType != null)
                {
                    var memberDonateTypeViewModel = new MemberDonateTypeViewModel { Id = memberDonateType.Id, DonateTypeName = memberDonateType.DonateTypeName };

                    return PartialView("_Delete", memberDonateTypeViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "MemberDonateType");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "MemberDonateType");
            }
        }

        //
        // POST: /MemberDonateType/Delete/By ID

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var memberDonateType = _db.MemberDonateTypes.Find(id);
                if (memberDonateType != null)
                {
                    _db.MemberDonateTypes.Remove(memberDonateType);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to delete this member donate type.");
            }
        }

    }
}
