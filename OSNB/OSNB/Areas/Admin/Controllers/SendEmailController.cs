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
    public class SendEmailController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        //
        // GET: /Admin/SendEmailInfo/

        public ActionResult Index()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetSendEmailInfos(DataTableParamModel param)
        {
            var sendEmailInfos = _db.SendEmailInfos.ToList();

            var viewSendEmailInfos = sendEmailInfos.Select(sei => new SendEmailInfoTableModel()
            {
                SendEmailInfoId = Convert.ToString(sei.Id),
                SenderName = sei.SenderName,
                SenderContactNo = sei.SenderContactNo,
                Subject = sei.Subject,
                Message = sei.Message,
                MemberViewModelId = Convert.ToString(sei.MemberId),
                MemberName = sei.Member
                    != null ? sei.Member.FullName : null,
            });

            IEnumerable<SendEmailInfoTableModel> filteredSendEmailInfos;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredSendEmailInfos = viewSendEmailInfos.Where(m => (m.SenderName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredSendEmailInfos = viewSendEmailInfos;
            }

            var viewOdjects = filteredSendEmailInfos.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from seiMdl in viewOdjects
                         select new[] { seiMdl.SenderName, seiMdl.SenderContactNo, seiMdl.Subject, seiMdl.Message, seiMdl.MemberName, seiMdl.SendEmailInfoId };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = sendEmailInfos.Count(),
                iTotalDisplayRecords = filteredSendEmailInfos.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /SendEmailInfo/Details/By ID

        public ActionResult Details(int id)
        {
            try
            {
                var sendEmailInfo = _db.SendEmailInfos.Find(id);
                if (sendEmailInfo != null)
                {
                    var sendEmailInfoViewModel = new SendEmailInfoViewModel { SendEmailInfoId = sendEmailInfo.Id, SenderName = sendEmailInfo.SenderName, SenderContactNo = sendEmailInfo.SenderContactNo, Subject = sendEmailInfo.Subject, Message = sendEmailInfo.Message, MemberViewModelId = sendEmailInfo.MemberId, MemberName = sendEmailInfo.Member != null ? sendEmailInfo.Member.FullName : null };

                    return PartialView("_Details", sendEmailInfoViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "SendEmail");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "SendEmail");
            }
        }

        ////
        //// GET: /SendEmailInfo/Add

        //public ActionResult Add()
        //{
        //    try
        //    {
        //        return PartialView("_Add");
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHelper.ExceptionMessageFormat(ex, true);
        //        return RedirectToAction("Index", "SendEmail");
        //    }
        //}

        ////
        //// POST: /SendEmailInfo/Add

        //[HttpPost]
        //public ActionResult Add(SendEmailInfoViewModel viewModel)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var sendEmailInfo = new SendEmailInfo { Id = viewModel.SendEmailInfoId, SenderName = viewModel.SenderName, SenderContactNo = viewModel.SenderContactNo, Subject = viewModel.Subject, Message = viewModel.Message, MemberId = viewModel.MemberViewModelId };

        //            _db.SendEmailInfos.Add(sendEmailInfo);
        //            _db.SaveChanges();

        //            return Content(Boolean.TrueString);
        //        }

        //        return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHelper.ExceptionMessageFormat(ex, true);
        //        return Content("Sorry! Unable to add this send email info.");
        //    }

        //}

        ////
        //// GET: /SendEmailInfo/Edit/By ID

        //public ActionResult Edit(int id)
        //{
        //    try
        //    {
        //        var sendEmailInfo = _db.SendEmailInfos.Find(id);
        //        if (sendEmailInfo != null)
        //        {
        //            var sendEmailInfoViewModel = new SendEmailInfoViewModel { SendEmailInfoId = sendEmailInfo.Id, SenderName = sendEmailInfo.SenderName, SenderContactNo = sendEmailInfo.SenderContactNo, Subject = sendEmailInfo.Subject, Message = sendEmailInfo.Message, MemberViewModelId = sendEmailInfo.MemberId, MemberName = sendEmailInfo.Member != null ? sendEmailInfo.Member.FullName : null };

        //            return PartialView("_Edit", sendEmailInfoViewModel);
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "SendEmail");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHelper.ExceptionMessageFormat(ex, true);
        //        return RedirectToAction("Index", "SendEmail");
        //    }
        //}

        ////
        //// POST: /SendEmailInfo/Edit/By ID

        //[HttpPost]
        //public ActionResult Edit(SendEmailInfoViewModel viewModel)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var sendEmailInfo = new SendEmailInfo { Id = viewModel.SendEmailInfoId, SenderName = viewModel.SenderName, SenderContactNo = viewModel.SenderContactNo, Subject = viewModel.Subject, Message = viewModel.Message, MemberId = viewModel.MemberViewModelId };

        //            _db.Entry(sendEmailInfo).State = EntityState.Modified;
        //            _db.SaveChanges();

        //            return Content(Boolean.TrueString);
        //        }

        //        return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHelper.ExceptionMessageFormat(ex, true);
        //        return Content("Sorry! Unable to edit this send email info.");
        //    }
        //}

        ////
        //// GET: /SendEmailInfo/Delete/By ID

        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        var sendEmailInfo = _db.SendEmailInfos.Find(id);
        //        if (sendEmailInfo != null)
        //        {
        //            var sendEmailInfoViewModel = new SendEmailInfoViewModel { SendEmailInfoId = sendEmailInfo.Id, SenderName = sendEmailInfo.SenderName, SenderContactNo = sendEmailInfo.SenderContactNo, Subject = sendEmailInfo.Subject, Message = sendEmailInfo.Message, MemberViewModelId = sendEmailInfo.MemberId, MemberName = sendEmailInfo.Member != null ? sendEmailInfo.Member.FullName : null };

        //            return PartialView("_Delete", sendEmailInfoViewModel);
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "SendEmail");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHelper.ExceptionMessageFormat(ex, true);
        //        return RedirectToAction("Index", "SendEmail");
        //    }
        //}

        ////
        //// POST: /SendEmailInfo/Delete/By ID

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        var sendEmailInfo = _db.SendEmailInfos.Find(id);
        //        if (sendEmailInfo != null)
        //        {
        //            _db.SendEmailInfos.Remove(sendEmailInfo);
        //            _db.SaveChanges();

        //            return Content(Boolean.TrueString);
        //        }

        //        return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHelper.ExceptionMessageFormat(ex, true);
        //        return Content("Sorry! Unable to delete this send email info.");
        //    }
        //}

    }
}
