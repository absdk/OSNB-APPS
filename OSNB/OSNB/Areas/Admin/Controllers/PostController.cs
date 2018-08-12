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
    public class PostController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        //
        // GET: /Admin/Post/

        public ActionResult Index()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetPosts(DataTableParamModel param)
        {
            var posts = _db.Posts.ToList();

            var viewPosts = posts.Select(pt => new PostTableModel() { PostId = Convert.ToString(pt.Id), Title = pt.Title, Content = pt.Content, CreateDate = Convert.ToString(pt.CreateDate), UserName = pt.UserName, });

            IEnumerable<PostTableModel> filteredPosts;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredPosts = viewPosts.Where(m => (m.Title ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredPosts = viewPosts;
            }

            var viewOdjects = filteredPosts.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from pMdl in viewOdjects
                         select new[] { pMdl.Title, pMdl.Content, pMdl.CreateDate, pMdl.UserName, pMdl.PostId };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = posts.Count(),
                iTotalDisplayRecords = filteredPosts.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Post/Details/By ID

        public ActionResult Details(int id)
        {
            try
            {
                var post = _db.Posts.Find(id);
                if (post != null)
                {
                    var postViewModel = new PostViewModel { Id = post.Id, Title = post.Title, Content = post.Content, CreateDate = post.CreateDate, UserName = post.UserName };

                    return PartialView("_Details", postViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "Post");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "Post");
            }
        }

        //
        // GET: /Post/Add

        public ActionResult Add()
        {
            try
            {
                return PartialView("_Add");
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "Post");
            }
        }

        //
        // POST: /Post/Add

        [HttpPost]
        public ActionResult Add(PostViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var post = new Post { Id = viewModel.Id, Title = viewModel.Title, Content = viewModel.Content, CreateDate = DateTime.Now, UserName = viewModel.UserName };

                    _db.Posts.Add(post);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to add this post.");
            }

        }

        //
        // GET: /Post/Edit/By ID

        public ActionResult Edit(int id)
        {
            try
            {
                var post = _db.Posts.Find(id);
                if (post != null)
                {
                    var postViewModel = new PostViewModel { Id = post.Id, Title = post.Title, Content = post.Content, CreateDate = post.CreateDate, UserName = post.UserName };

                    return PartialView("_Edit", postViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "Post");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "Post");
            }
        }

        //
        // POST: /Post/Edit/By ID

        [HttpPost]
        public ActionResult Edit(PostViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var post = new Post { Id = viewModel.Id, Title = viewModel.Title, Content = viewModel.Content, CreateDate = viewModel.CreateDate};

                    _db.Entry(post).State = EntityState.Modified;
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to edit this post.");
            }
        }

        //
        // GET: /Post/Delete/By ID

        public ActionResult Delete(int id)
        {
            try
            {
                var post = _db.Posts.Find(id);
                if (post != null)
                {
                    var postViewModel = new PostViewModel { Id = post.Id, Title = post.Title, Content = post.Content, CreateDate = post.CreateDate, UserName = post.UserName };

                    return PartialView("_Delete", postViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "Post");
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return RedirectToAction("Index", "Post");
            }
        }

        //
        // POST: /Post/Delete/By ID

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var post = _db.Posts.Find(id);
                if (post != null)
                {
                    _db.Posts.Remove(post);
                    _db.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                return Content(ExceptionHelper.ModelStateErrorFormat(ModelState));
            }
            catch (Exception ex)
            {
                ExceptionHelper.ExceptionMessageFormat(ex, true);
                return Content("Sorry! Unable to delete this post.");
            }
        }
    }
}
