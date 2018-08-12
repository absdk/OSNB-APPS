using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSNB.Helpers;
using OSNB.ViewModels.DataTableViewModels;
using OSNB.Models;

namespace OSNB.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        //
        // GET: /Admin/Role/

        public ActionResult Index()
        {
            return View();
        }

        // for display datatable
        public ActionResult GetRoles(DataTableParamModel param)
        {
            var roles = _db.Roles.ToList();

            var viewRoles = roles.Select(rt => new RoleTableModel() { RoleName = rt.RoleName });

            IEnumerable<RoleTableModel> filteredRoles;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredRoles = viewRoles.Where(r => (r.RoleName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredRoles = viewRoles;
            }

            var viewOdjects = filteredRoles.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from rMdl in viewOdjects
                         select new[] { rMdl.RoleName };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = roles.Count(),
                iTotalDisplayRecords = filteredRoles.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }


    }
}
