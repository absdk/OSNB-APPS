using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSNB.Helpers;
using OSNB.Models;
using OSNB.ViewModels;
using OSNB.ViewModels.DataTableViewModels;

namespace OSNB.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        public ActionResult Index()
        {

            var memberZones = _db.MemberZones.ToList().Select(x => new MemberZoneViewModel { Id = x.Id, ZoneName = x.ZoneName });
            var memberBloodGroups = _db.MemberBloodGroups.ToList().Select(x => new MemberBloodGroupViewModel { Id = x.Id, BloodGroupName = x.BloodGroupName });

            var homePageViewModel = new HomePageViewModel { MemberZoneViewModels = memberZones, MemberBloodGroupViewModels = memberBloodGroups };

            return View(homePageViewModel);
        }

        public ActionResult About()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetBloodRequests(DataTableParamModel param)
        {
            var bloodRequests = _db.BloodRequests.ToList();

            var viewBloodRequests = bloodRequests.Select(m => new BloodRequestTableModel() { BloodRequestId = Convert.ToString(m.Id), RequesterName = m.RequesterName, RequesterContactNo = m.RequesterContactNo, RequesterAmount = m.RequesterAmount, PresentLocation = m.PresentLocation, DateOfDonation = Convert.ToString(m.DateOfDonation), AppealMessage = m.AppealMessage, RequiredBloodGroupId = Convert.ToString(m.RequiredBloodGroupId), RequiredBloodGroup = m.MemberBloodGroup != null ? m.MemberBloodGroup.BloodGroupName : null, });

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
                         select new[] { pMdl.RequesterName, pMdl.RequesterContactNo, pMdl.RequesterAmount, pMdl.PresentLocation, pMdl.DateOfDonation, pMdl.AppealMessage, pMdl.RequiredBloodGroupId, pMdl.RequiredBloodGroup, pMdl.BloodRequestId };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = bloodRequests.Count(),
                iTotalDisplayRecords = filteredBloodRequests.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //ZoneInfo
        public ActionResult ZoneInfo(int id)
        {

            var memberZone = _db.MemberZones.Find(id);

            var memberZoneViewModel = new MemberZoneViewModel { Id = memberZone.Id, ZoneName = memberZone.ZoneName };

            return View(memberZoneViewModel);
        }

        //BloodGroupInfo
        public ActionResult BloodGroupInfo(int id)
        {

            var memberBloodGroup = _db.MemberBloodGroups.Find(id);

            var memberBloodGroupViewModel = new MemberBloodGroupViewModel { Id = memberBloodGroup.Id, BloodGroupName = memberBloodGroup.BloodGroupName };

            return View(memberBloodGroupViewModel);
        }

        //ZoneDetails
        public ActionResult ZoneDetails(string id)
        {
            var memberZone = _db.MemberZones.ToList().SingleOrDefault(x => x.ZoneName == id);

            var memberZoneViewModel = new MemberZoneViewModel();

            if (memberZone != null)
            {
                memberZoneViewModel = new MemberZoneViewModel { Id = memberZone.Id, ZoneName = memberZone.ZoneName };

                return View(memberZoneViewModel);
            }
            return View(memberZoneViewModel);
        }

        //Map
        public ActionResult Map()
        {
            return View();
        }
    }
}
