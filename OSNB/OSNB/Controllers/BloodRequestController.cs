 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSNB.Models;
using OSNB.ViewModels;
using OSNB.Helpers;
using OSNB.ViewModels.DataTableViewModels;

namespace OSNB.Controllers
{
    public class BloodRequestController : Controller
    {
        private AppDbContext _db = new AppDbContext();
        //
        // GET: /BloodRequest/

        public ActionResult Index()
        {
            return View();
        }

        //GetBloodRequestList
        public ActionResult GetBloodRequestList(DataTableParamModel param)
        {
            var bloodRequests = _db.BloodRequests.ToList();

            var viewBloodRequests = bloodRequests.Select(m => new BloodRequestTableModel() { BloodRequestId = Convert.ToString(m.Id), RequesterName = m.RequesterName, RequesterContactNo = m.RequesterContactNo, RequesterAmount = m.RequesterAmount, PresentLocation = m.PresentLocation, DateOfDonation = Convert.ToString(m.DateOfDonation), AppealMessage = m.AppealMessage, RequesterStatus = m.RequesterStatus, RequesterStatusMessage = m.RequesterStatusMessage, RequiredBloodGroupId = Convert.ToString(m.RequiredBloodGroupId), RequiredBloodGroup = m.MemberBloodGroup != null ? m.MemberBloodGroup.BloodGroupName : null, });

            IEnumerable<BloodRequestTableModel> filteredBloodRequests;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredBloodRequests = viewBloodRequests.Where(m => (m.RequesterName ?? "").Contains(param.sSearch) || (m.RequiredBloodGroup ?? "").Contains(param.sSearch)).ToList();
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

        //ForBlood
        public ActionResult ForBlood()
        {
            try
            {

                var memberBloodGroups = SelectListItemExtension.PopulateDropdownList(_db.MemberBloodGroups.ToList<MemberBloodGroup>(), "Id", "BloodGroupName").ToList();


                var bloodRequestViewModel = new BloodRequestViewModel { ddlMemberBloodGroups = memberBloodGroups };

                return View(bloodRequestViewModel);



            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "BloodRequest");
            }

        }

        [HttpPost]
        public ActionResult ForBlood(BloodRequestViewModel viewModel)
        {

            var memberBloodGroups = SelectListItemExtension.PopulateDropdownList(_db.MemberBloodGroups.ToList<MemberBloodGroup>(), "Id", "BloodGroupName", isEdit: true, selectedValue: viewModel.RequiredBloodGroupId != 0 ? viewModel.RequiredBloodGroupId.ToString() : "0").ToList();


            viewModel.ddlMemberBloodGroups = memberBloodGroups;


            try
            {
                if (ModelState.IsValid)
                {

                    BloodRequest model = new BloodRequest { RequesterName = viewModel.RequesterName, RequesterContactNo = viewModel.RequesterContactNo, RequesterAmount = viewModel.RequesterAmount, PresentLocation = viewModel.PresentLocation, DateOfDonation = Convert.ToDateTime(viewModel.DateOfDonation), AppealMessage = viewModel.AppealMessage, RequiredBloodGroupId = viewModel.RequiredBloodGroupId };

                    _db.BloodRequests.Add(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "BloodRequest");
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
                    return RedirectToAction("Index", "BloodRequest");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "BloodRequest");
            }
        }


    }
}
