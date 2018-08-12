using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSNB.Helpers;
using OSNB.Models;
using OSNB.ViewModels;
using OSNB.ViewModels.DataTableViewModels;
using System.Data;

namespace OSNB.Areas.Admin.Controllers
{
    public class AdminBloodRequestController : Controller
    {
        private AppDbContext _db = new AppDbContext();
        //
        // GET: /Admin/BloodRequest/

        public ActionResult Index()
        {
            return View();
        }

        //GetBloodRequestList
        public ActionResult GetBloodRequests(DataTableParamModel param)
        {
            var bloodRequests = _db.BloodRequests.ToList();

            var viewBloodRequests = bloodRequests.Select(m => new BloodRequestTableModel() { BloodRequestId = Convert.ToString(m.Id), RequesterName = m.RequesterName, RequesterContactNo = m.RequesterContactNo, RequesterAmount = m.RequesterAmount, PresentLocation = m.PresentLocation, DateOfDonation = Convert.ToString(m.DateOfDonation), AppealMessage = m.AppealMessage, RequesterStatus = m.RequesterStatus, RequesterStatusMessage = m.RequesterStatusMessage, RequiredBloodGroupId = Convert.ToString(m.RequiredBloodGroupId), RequiredBloodGroup = m.MemberBloodGroup != null ? m.MemberBloodGroup.BloodGroupName : null, });

            IEnumerable<BloodRequestTableModel> filteredBloodRequests;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredBloodRequests = viewBloodRequests.Where(m => (m.RequesterName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredBloodRequests = viewBloodRequests;
            }

            var viewOdjects = filteredBloodRequests.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from pMdl in viewOdjects
                         //select new[] { pMdl.RequesterName, pMdl.RequesterContactNo, pMdl.RequesterAmount, pMdl.PresentLocation, pMdl.DateOfDonation, pMdl.AppealMessage, pMdl.RequiredBloodGroupId, pMdl.RequiredBloodGroup, pMdl.BloodRequestId };
                         select new[] { pMdl.RequesterName, pMdl.RequesterContactNo, pMdl.RequesterAmount, pMdl.PresentLocation, pMdl.DateOfDonation, pMdl.RequiredBloodGroup, pMdl.RequesterStatus, pMdl.BloodRequestId };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = bloodRequests.Count(),
                iTotalDisplayRecords = filteredBloodRequests.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /BloodRequest/Details/By ID

        public ActionResult Details(int id)
        {
            try
            {
                var bloodRequest = _db.BloodRequests.Find(id);
                if (bloodRequest != null)
                {
                    var bloodRequestViewModel = new BloodRequestViewModel { BloodRequestId = bloodRequest.Id, RequesterName = bloodRequest.RequesterName, RequesterContactNo = bloodRequest.RequesterContactNo, RequesterAmount = bloodRequest.RequesterAmount, PresentLocation = bloodRequest.PresentLocation, DateOfDonation = bloodRequest.DateOfDonation, AppealMessage = bloodRequest.AppealMessage };

                    return PartialView("_Details", bloodRequestViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "AdminBloodRequest");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "AdminBloodRequest");
            }
        }

        // GET: /MemberDonateType/Status/By ID

        public ActionResult Status(int id)
        {
            try
            {
                var bloodRequest = _db.BloodRequests.Find(id);
                if (bloodRequest != null)
                {
                    var bloodRequestViewModel = new BloodRequestViewModel { BloodRequestId = bloodRequest.Id, RequesterName = bloodRequest.RequesterName, RequesterContactNo = bloodRequest.RequesterContactNo, RequesterAmount = bloodRequest.RequesterAmount, PresentLocation = bloodRequest.PresentLocation, DateOfDonation = bloodRequest.DateOfDonation, AppealMessage = bloodRequest.AppealMessage, RequesterStatus = bloodRequest.RequesterStatus, RequesterStatusMessage = bloodRequest.RequesterStatusMessage };

                    return PartialView("_Status", bloodRequestViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "AdminBloodRequest");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "AdminBloodRequest");
            }
        }

        //
        // POST: /MemberDonateType/Edit/By ID

        [HttpPost]
        public ActionResult Status(BloodRequestViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = _db.BloodRequests.Find(viewModel.BloodRequestId);

                    model.RequesterStatus = viewModel.RequesterStatus;
                    model.RequesterStatusMessage = viewModel.RequesterStatusMessage;

                    _db.Entry(model).State = EntityState.Modified;
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to update this member donate type.");
            }
        }


    }
}
