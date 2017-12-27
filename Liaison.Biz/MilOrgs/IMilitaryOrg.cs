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
        string Name { get; }
        int? Number { get; set; }
        bool UseOrdinal { get; set; }
        string Mission { get; set; }
        UnitType UnitTypeId { get; set; }        
        string CurrentOpsRef { get; }
        string CurrentOpsUrl { get; }
        string CurrentOpsLogo { get; }
        ServiceType ServiceTypeIdx { get; set; }
        Services ServiceId { get; }
        List<BaseOrg> Bases { get; }
        List<HigherHqOrg> HigherHqs { get; }
        List<ShortForm> ShortForm { get; }
        
    }

}
