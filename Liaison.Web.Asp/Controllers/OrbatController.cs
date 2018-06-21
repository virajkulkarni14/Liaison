using Liaison.BLL.Translators;
using Liaison.Data.Sql.Edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Liaison.BLL.Models;
using Liaison.Web.Asp.Models;

namespace Liaison.Web.Asp.Controllers
{
    public class OrbatController : Controller
    {
        public ActionResult Index(string input, int? depth)
        {
            var model = LiaisonSql.GetTree(input, depth);

            var returnable = new ViewModel();
            returnable.TheTree = model;

            return View(returnable);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RefinedIndex(ViewModel vm)
        {
            return RedirectToAction("Index", new {input = vm.Input, depth = vm.Depth});
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveNewAirForceBase(NewThing m)
        {
            var yay = LiaisonSql.CreateAFB(m);

            return View(yay);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Publish(NewThing m)
        {
            
            return null;
        }

        public ActionResult NewAirForceBase()
        {
            NewThing nn = new NewThing
            {
                Things = new List<NewWing>
                {
                    new NewWing
                    {
                        Code = "OPS",
                        Name = "Operations",
                        NewSqdrns = new List<NewSqn>
                        {
                            new NewSqn
                            {
                                Code = "OPS",
                                Name = "Operations Support",

                            },
                            new NewSqn
                            {
                                Code = "OS",
                                Name = "Operations Support",

                            },
                            new NewSqn
                            {
                                Code = "CPT",
                                Name = "Comptroller",

                            },
                            new NewSqn
                            {
                                Code = "MD",
                                Name = "Medical",

                            }
                        },
                    },
                    new NewWing
                    {
                        Code = "MD",
                        Name = "Medical",
                        NewSqdrns = new List<NewSqn>
                        {
                            new NewSqn
                            {
                                Code="AD",
                                Name = "Aeromedical-Dental",
                            },
                            new NewSqn
                            {
                                Code="AMD",
                                Name="Aerospace Medicine"
                            },
                            new NewSqn
                            {
Code="D",
                                Name="Dental"
                            },
                            new NewSqn
                            {
                                Code = "MDO",
                                Name = "Medical Operations",

                            },
                            new NewSqn
                            {
                                Code = "MDS",
                                Name = "Medical Support",
                            },
                        },
                    },
                    new NewWing
                    {
                        Code = "MX",
                        Name = "Maintenance",
                        NewSqdrns = new List<NewSqn>
                        {
                            new NewSqn
                            {
                                Code = "AMX",
                                Name = "Aircraft Maintenance",
                            },
                            new NewSqn
                            {
                                Code="CMX", 
                                Name="Component Maintenance",
                            },
                            new NewSqn
                            {
                                Code ="EMX",
                                Name="Equipment Maintenance"
                            },
                            new NewSqn
                            {
                                Code = "MX",
                                Name = "Maintenance",
                            },
                            new NewSqn
                            {
                                Code = "MUN",
                                Name = "Munitions",
                            },
                            new NewSqn
                            {
                                Code = "MO",
                                Name = "Maintenance Operations",
                            },
                        },
                    },
                    new NewWing
                    {
                        Code = "MS",

                        Name = "Mission Support",
                        NewSqdrns = new List<NewSqn>
                        {
                            new NewSqn
                            {
                                Name = "Civil Engineer",
                                Code = "CE",
                            },
                            new NewSqn
                            {
                                Name = "Communications",
                                Code = "COM",
                            },
                            new NewSqn
                            {
                                Name = "Contracting",
                                Code = "CON",
                            },
                            new NewSqn
                            {
                                Name = "Force Support",
                                Code = "FS",
                            },
                            new NewSqn
                            {
                                Name = "Logistics Readiness",
                                Code = "LR",
                            },
                            new NewSqn
                            {
                                Name = "Mission Support",
                                Code = "MS",
                            },
                            new NewSqn
                            {
                                Name = "Security Forces",
                                Code = "SF",
                            }
                        },
                    }
                }
            };

            return View(nn);
        }

    }
}
