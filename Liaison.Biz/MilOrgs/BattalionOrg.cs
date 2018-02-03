using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liaison.Helper.Enumerators;

namespace Liaison.Biz.MilOrgs
{
    public class BattalionOrg : IMilitaryOrg
    {
        public string ParentShortForm { get; set; }
        public string GetFullName()
        {
            StringBuilder sb = new StringBuilder();
            if (Number.HasValue)
            {
                sb.Append(Helper.Helper.GetIntWithUnderscores(Number.Value, true) + " ");
            }
            if (this.ServiceTypeIdx == ServiceType.Reserve)
            {
                sb.Append("(" + Helper.Constants.Initials_Component.Reserve + ") ");
            }
            else if (this.ServiceTypeIdx == ServiceType.Volunteer)
            {
                sb.Append("(" + Helper.Constants.Initials_Component.Volunteer + ") (" + this.USState + ") ");
            }

            sb.Append(Mission + " ");
            sb.Append(Helper.Constants.ShortForm.Battalion + ".");
            if (!string.IsNullOrEmpty(ParentShortForm))
            {
                sb.Append(", "+ParentShortForm);
            }
            return sb.ToString();
        }
        public int? Number { get; set; }
        public bool UseOrdinal { get; set; }
        public string Mission { get; set; }
        public UnitType UnitTypeId { get; set; }
        public string CurrentOpsRef { get; set; }
        public string CurrentOpsUrl { get; set; }
        public string CurrentOpsLogo { get; set; }
        public Services ServiceId { get; set; }
        public List<BaseOrg> Bases { get; set; }
        public List<HigherHqOrg> HigherHqs { get; set; }
        public ServiceType ServiceTypeIdx { get; set; }
        public List<ShortForm> ShortForm { get; set; }
        public string Name { get; set; }
        public List<ChildOrg> ChildOrgs { get; set; }
        public string USState { get; set; }
    }
}

