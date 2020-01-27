using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Liaison.BLL.Models
{
    public class NewThing
    {
        public int Number { get; set; }
        public int BaseId { get; set; }
        public string Mission { get; set; }
        public List<NewWing> Things { get; set; }
        public int ServiceType { get; set; }
        public IEnumerable<SelectListItem> ServiceTypeOptions { get; set; }
        public string TerritorialDesignation { get; set; }
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
