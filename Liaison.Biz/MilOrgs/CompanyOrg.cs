﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liaison.Helper.Enumerators;

namespace Liaison.Biz.MilOrgs
{
    public class CompanyOrg : IMilitaryOrg
    {
        public string GetFullName()
        {
            StringBuilder sb = new StringBuilder();            
            if (Number.HasValue)
            {
                if (UseOrdinal)
                {
                    sb.Append(Helper.Helper.AddOrdinal(Number.Value) + " ");
                }
                else
                {
                    sb.Append(Number.Value + " ");
                }
                sb.Append(Number.ToString());
            }
            else if (!string.IsNullOrEmpty(Name))
            {
                sb.Append(Name);
            }
            if (!string.IsNullOrEmpty(Mission))
            {
                sb.Append(Mission);
            }
            sb.Append(" Coy., ");
            if (!string.IsNullOrEmpty(ParentShortForm))
            {
                sb.Append(ParentShortForm.ToUpper());
            }
            return sb.ToString();
        }

        public int? Number { get; set; }
        public bool UseOrdinal { get; set; }
        public string Name { get; set; }
        public string Mission { get; set; }
        public UnitType UnitTypeId { get; set; }

        public string CurrentOpsRef { get; set; }

        public string CurrentOpsUrl { get; set; }

        public string CurrentOpsLogo { get; set; }

        public ServiceType ServiceTypeIdx { get; set; }

        public Services ServiceId { get; set; }

        public List<BaseOrg> Bases { get; set; }

        public List<HigherHqOrg> HigherHqs { get; set; }

        public List<ShortForm> ShortForm { get; set; }
        public string ParentShortForm { get; set; }
    }
}