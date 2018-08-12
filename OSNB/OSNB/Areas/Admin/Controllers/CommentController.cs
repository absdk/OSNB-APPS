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
    public class CommentController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        //
        // GET: /Admin/Comment/

        public ActionResult Index()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetComments(DataTableParamModel param)
        {
            var comments = _db.Comments.ToList();

            var viewComments = comments.Select(ct => new CommentTableModel() { CommentId = Convert.ToString(ct.Id), Name = ct.Name, Email = ct.Email, Description = ct.Description, CreateDate = Convert.ToString(ct.CreateDate) });

            IEnumerable<CommentTableModel> filteredComments;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredComments = viewComments.Where(c => (c.Name ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredComments = viewComments;
            }

            var viewOdjects = filteredComments.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from cMdl in viewOdjects
                         select new[] { cMdl.Name, cMdl.Email, cMdl.Description, cMdl.CreateDate, cMdl.CommentId };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = comments.Count(),
                iTotalDisplayRecords = filteredComments.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Comment/Details/By ID

        public ActionResult Details(int id)
        {
            try
            {
                var comment = _db.Comments.Find(id);
                if (comment != null)
                {
                    var commentViewModel = new CommentViewModel { Id = comment.Id, Name = comment.Name, Email = comment.Email, Description = comment.Description, CreateDate = comment.CreateDate, PostId = comment.PostId };

                    return PartialView("_Details", commentViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "Comment");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "Comment");
            }
        }

        //
        // GET: /Comment/Add

        public ActionResult Add()
        {
            try
            {
                return PartialView("_Add");
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "Comment");
            }
        }

        //
        // POST: /Comment/Add

        [HttpPost]
        public ActionResult Add(CommentViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var comment = new Comment { Id = viewModel.Id, Name = viewModel.Name, Email = viewModel.Email, Description = viewModel.Description, CreateDate = DateTime.Now, PostId = viewModel.PostId };

                    _db.Comments.Add(comment);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to add this comment.");
            }

        }

        //
        // GET: /Comment/Edit/By ID

        public ActionResult Edit(int id)
        {
            try
            {
                var comment = _db.Comments.Find(id);
                if (comment != null)
                {
                    var commentViewModel = new CommentViewModel { Id = comment.Id, Name = comment.Name, Email = comment.Email, Description = comment.Description, CreateDate = comment.CreateDate, PostId = comment.PostId };

                    return PartialView("_Edit", commentViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "Comment");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "Comment");
            }
        }

        //
        // POST: /Comment/Edit/By ID

        [HttpPost]
        public ActionResult Edit(CommentViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var comment = new Comment { Id = viewModel.Id, Name = viewModel.Name, Email = viewModel.Email, Description = viewModel.Description };

                    _db.Entry(comment).State = EntityState.Modified;
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to edit this comment.");
            }
        }

        //
        // GET: /Comment/Delete/By ID

        public ActionResult Delete(int id)
        {
            try
            {
                var comment = _db.Comments.Find(id);
                if (comment != null)
                {
                    var commentViewModel = new CommentViewModel { Id = comment.Id, Name = comment.Name, Email = comment.Email, Description = comment.Description, CreateDate = comment.CreateDate, PostId = comment.PostId };

                    return PartialView("_Delete", commentViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "Comment");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "Comment");
            }
        }

        //
        // POST: /Comment/Delete/By ID

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var comment = _db.Comments.Find(id);
                if (comment != null)
                {
                    _db.Comments.Remove(comment);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to delete this comment.");
            }
        }

    }
}
