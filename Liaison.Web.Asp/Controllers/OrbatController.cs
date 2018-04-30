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
        // GET: Orbat
        public ActionResult Index()
        {
            string input = "JFHQ";

            
            var model = LiaisonSql.GetTree(input);


            return View(model);
        }
    }
}