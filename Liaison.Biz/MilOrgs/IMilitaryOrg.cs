using Liaison.Biz.MilOrgs;
using Liaison.Helper.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.Biz
{
    public interface IMilitaryOrg
    {
        string GetFullName();

        int? Number { get; set; }
        bool UseOrdinal { get; set; }
        string Name { get; set; } 
        string Mission { get; set; }
        UnitType UnitTypeId { get; set; }        
        string CurrentOpsRef { get; }
        string CurrentOpsUrl { get; }
        string CurrentOpsLogo { get; }
        ServiceType ServiceTypeIdx { get;  }
        Services ServiceId { get; }
      string USState { get; }
        List<BaseOrg> Bases { get; }
        List<HigherHqOrg> HigherHqs { get; }
        List<ChildOrg> ChildOrgs { get; }
        List<ShortForm> ShortForm { get; }        
    }

  

}
