using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.Biz.MilOrgs
{
    public class BaseOrg
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string CurrentOpsUrl { get; set; }
        public int? DateFrom { get; set; }
        public int? DateUntil { get; set; }
        public bool IsDeployment { get; set; }
        public bool IsCurrent { get; set; }
        public string CurrentOpsBaseRef { get; set; }
        public string ParentBase { get; internal set; }
    }
}
