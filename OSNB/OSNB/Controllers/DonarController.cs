using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSNB.Models;
using OSNB.Helpers;
using OSNB.ViewModels.DataTableViewModels;
using OSNB.ViewModels;
using System.Data;

namespace OSNB.Controllers
{
    public class DonarController : Controller
    {
        private AppDbContext _db = new AppDbContext();
        //
        // GET: /Donar/

        public ActionResult Index()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetDonarList(DataTableParamModel param)
        {
            var members = _db.Members.ToList();

            var viewMembers = members.Select(m => new MemberTableModel() { MemberId = Convert.ToString(m.Id), FullName = m.FullName, Address = m.Address, MemberBloodGroupName = m.MemberBloodGroup != null ? m.MemberBloodGroup.BloodGroupName : null, DateOfBirth = Convert.ToString(m.DateOfBirth), PhoneNumber = m.PhoneNumber, MobileNumber = m.MobileNumber, UserName = m.UserName, });

            IEnumerable<MemberTableModel> filteredMembers;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredMembers = viewMembers.Where(m => (m.FullName ?? "").Contains(param.sSearch) || (m.LastName ?? "").Contains(param.sSearch) || (m.Address ?? "").Contains(param.sSearch) || (m.MemberBloodGroupName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredMembers = viewMembers;
            }

            var viewOdjects = filteredMembers.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from pMdl in viewOdjects
                         select new[] { pMdl.FullName, pMdl.Address, pMdl.MemberBloodGroupName, pMdl.DateOfBirth, pMdl.PhoneNumber, pMdl.MobileNumber, pMdl.UserName, pMdl.MemberId };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = members.Count(),
                iTotalDisplayRecords = filteredMembers.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        // for display datatable
        public ActionResult GetDonarByBloodGroupList(int id, DataTableParamModel param)
        {
            var members = _db.Members.ToList().Where(x => x.MemberBloodGroupId == id);

            var viewMembers = members.Select(m => new MemberTableModel() { MemberId = Convert.ToString(m.Id), FullName = m.FullName, Address = m.Address, DateOfBirth = Convert.ToString(m.DateOfBirth), PhoneNumber = m.PhoneNumber, MobileNumber = m.MobileNumber, UserName = m.UserName, });

            IEnumerable<MemberTableModel> filteredMembers;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredMembers = viewMembers.Where(m => (m.FullName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredMembers = viewMembers;
            }

            //var viewOdjects = filteredMembers.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var viewOdjects = filteredMembers;

            var result = from pMdl in viewOdjects
                         select new[] { pMdl.FullName, pMdl.Address, pMdl.DateOfBirth, pMdl.PhoneNumber, pMdl.MobileNumber, pMdl.UserName, pMdl.MemberId };

            return Json(new
            {
                //sEcho = param.sEcho,
                iTotalRecords = members.Count(),
                iTotalDisplayRecords = filteredMembers.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        // for display datatable
        public ActionResult GetDonarByZoneList(int id, DataTableParamModel param)
        {
            var members = _db.Members.ToList().Where(x => x.MemberZoneId == id);

            var viewMembers = members.Select(m => new MemberTableModel() { MemberId = Convert.ToString(m.Id), FullName = m.FullName, Address = m.Address, DateOfBirth = Convert.ToString(m.DateOfBirth), PhoneNumber = m.PhoneNumber, MobileNumber = m.MobileNumber, UserName = m.UserName, });

            IEnumerable<MemberTableModel> filteredMembers;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredMembers = viewMembers.Where(m => (m.FullName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredMembers = viewMembers;
            }

            //var viewOdjects = filteredMembers.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var viewOdjects = filteredMembers;

            var result = from pMdl in viewOdjects
                         select new[] { pMdl.FullName, pMdl.Address, pMdl.DateOfBirth, pMdl.PhoneNumber, pMdl.MobileNumber, pMdl.UserName, pMdl.MemberId };

            return Json(new
            {
                //sEcho = param.sEcho,
                iTotalRecords = members.Count(),
                iTotalDisplayRecords = filteredMembers.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //SendEmail
        public ActionResult SendEmail(int id)
        {
            try
            {
                var member = _db.Members.Find(id);
                if (member != null)
                {
                    var sendEmailInfoViewModel = new SendEmailInfoViewModel { MemberViewModelId = member.Id, MemberName = member.FullName };

                    return PartialView("_SendEmail", sendEmailInfoViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "Donar");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "Donar");
            }
        }

        //
        // POST: /Donar/Delete/By ID

        [HttpPost]
        public ActionResult SendEmail(SendEmailInfoViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var sendEmailInfo = new SendEmailInfo { SenderName = viewModel.SenderName, SenderContactNo = viewModel.SenderContactNo, Subject = viewModel.Subject, Message = viewModel.Message, MemberId = viewModel.MemberViewModelId };

                    var member = _db.Members.Find(viewModel.MemberViewModelId);
                    var user = _db.Users.Find(member.UserName);
                    var toEmail = user != null ? user.Email : null;
                    var body = viewModel.Message + "Contact No: " + viewModel.SenderContactNo;

                    SendMailHelper sendMailHelper = new SendMailHelper();
                    bool isResult = sendMailHelper.SendEmail(toEmail, viewModel.Subject, body);

                    if (isResult)
                    {
                        _db.SendEmailInfos.Add(sendEmailInfo);
                        _db.SaveChanges();

                        return Content(Boolean.TrueString);
                    }
                    else
                    {
                        return Content("Sorry! Unable to send email to this member.");
                    }


                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to send email to this member.");
            }
        }

        //
        // GET: /Donar/Details/By ID

        public ActionResult Details(int id)
        {
            try
            {
                var member = _db.Members.Find(id);
                if (member != null)
                {
                    var user = _db.Users.Find(member.UserName);

                    var memberViewModel = new CreateOrEditMemberViewModel { MemberId = member.Id, UserName = user.UserName, UserEmail = user.Email, FirstName = member.FirstName, LastName = member.LastName, SurName = member.SurName, DateOfBirth = member.DateOfBirth, Address = member.Address, PhoneNumber = member.PhoneNumber, MobileNumber = member.MobileNumber, ThumbImageUrl = member.ThumbImageUrl, SmallImageUrl = member.SmallImageUrl, MemberBloodGroupName = member.MemberBloodGroup != null ? member.MemberBloodGroup.BloodGroupName : null, MemberDistrictName = member.MemberDistrict != null ? member.MemberDistrict.DistrictName : null, MemberZoneName = member.MemberZone != null ? member.MemberZone.ZoneName : null, MemberHospitalName = member.MemberHospital != null ? member.MemberHospital.HospitalName : null };

                    return View(memberViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "Donar");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "Donar");
            }
        }

    }
}
