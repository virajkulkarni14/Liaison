using Liaison.BLL.Translators;
using Liaison.Data.Sql.Edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Liaison.Web.Asp.Controllers
{
    public class OrbatController : Controller
    {
        public ActionResult Index(string input)
        {
            var model = LiaisonSql.GetTree(input);


            return View(model);
        }
    }
}