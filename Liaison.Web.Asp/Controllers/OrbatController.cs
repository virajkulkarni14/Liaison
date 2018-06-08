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

        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveNewAirForceBase(NewThing m)
        {
            var yay = LiaisonSql.CreateAFB(m);

            return rtu
        }

        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Publish(NewThing m)
        {
            string a = "b";
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
                                Code="OPS",
                                Name = "Operations Support",
                                
                            },
                            new NewSqn
                            {
                                Code="OS",
                                Name = "Operations Support",
                                
                            }
                        },
                    },
                    new NewWing
                    {
                        Name = null,
                        NewSqdrns = new List<NewSqn>
                        {
                            new NewSqn
                            {
                                Code="CPT",
                                Name = "Comptroller",
                                
                            },
                            new NewSqn
                            {
                                Code="MD",
                                Name = "Medical",
                                
                            }
                        },
                    },
                    new NewWing
                    {
                        Code="MD",
                        Name = "Medical",
                        NewSqdrns = new List<NewSqn>
                        {
                            new NewSqn
                            {
                                Code="MDO",
                                Name = "Medical Operations",
                                
                            },
                            new NewSqn
                            {
                                Code="MDS",
                                Name = "Medical Support",
                            },                            
                        },
                    },
                    new NewWing
                    {
                        Code="MX",                        
                        Name = "Maintenance",
                        NewSqdrns = new List<NewSqn>
                        {
                            new NewSqn
                            {
                                Code="AMX",
                                Name = "Aircraft Maintenance",
                            },
                            new NewSqn
                            {
                                Code="MX",
                                Name = "Maintenance",
                            },
                            new NewSqn
                            {
                                Code="MUN",
                                Name = "Munitions",
                            },
                            new NewSqn
                            {
                                Code="MO",
                                Name = "Maintenance Operations",
                            },
                        },
                    },
                    new NewWing
                    {
                        Code="MS",
                        
                        Name = "Mission Support",
                        NewSqdrns = new List<NewSqn>
                        {
                            new NewSqn
                            {
                                Name = "Civil Engineer",
                                
                                Code="CE",
                            },
                            new NewSqn
                            {
                                Name = "Communications",
                                
                                Code="COM",
                            },
                            new NewSqn
                            {
                                Name = "Contracting",
                                
                                Code="CON",
                            },
                            new NewSqn
                            {
                                Name = "Force Support",
                                
                                Code="FS",
                            },
                            new NewSqn
                            {
                                Name = "Logistics Readiness",
                                
                                Code="RL",
                            },
                            new NewSqn
                            {
                                Name = "Mission Support",
                                
                                Code="MS",
                            },
                            new NewSqn
                            {
                                Name = "Security Forces",
                                
                                Code="SF",
                            }
                        },
                    }
                }
            };

            return View(nn);
        }
    }

    public class NewThing
    {
        public int Number { get; set; }
        public string Mission { get; set; }
       public  List<NewWing> Things { get; set; }
    }

    public class NewSqn
    {
        public int? Qty { get; set; }
        public bool Create { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class NewWing
    {
        public bool Create { get; set; }
        public string Name { get; set; }
        public List<NewSqn> NewSqdrns { get; set; }
        public String Code { get; set; }
    }
}
