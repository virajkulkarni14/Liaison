using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Liaison.BLL.Models.Unit;
using Liaison.BLL.Models.Unit.Interfaces;

namespace Liaison.Web.Asp.Models
{
    public class ViewModel
    {
        public IUnit TheTree { get; set; }
        public int Depth { get; set; }
        public string Input { get; set; }
    }
    //public class OrbatModel
    //{
    //    public object FirstUnit { get; set; }
    //}
}