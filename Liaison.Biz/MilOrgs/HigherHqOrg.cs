using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liaison.Helper.Enumerators;

namespace Liaison.Biz.MilOrgs
{
    public class HigherHqOrg
    {
        public string CurrentOpsRef { get; set; }
        public int? DateFrom { get; internal set; }
        public int? DateUntil { get; internal set; }
        public bool IsCurrent { get; internal set; }
        public HigherHqType CommandRelationshipType { get; internal set; }
    }
    public class ShortForm
    {
        public string Text { get; set; }
        public ShortFormType Type { get; set; }
    }
}