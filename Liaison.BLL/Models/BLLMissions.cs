using System.Collections.Generic;
using System.Linq;
using Liaison.Data.Sql.Edmx;

namespace Liaison.BLL.Models
{
    public class BLLAdminCorps
    {
        public BLLAdminCorps(AdminCorp admincorps, int unitid)
        {
            if (admincorps == null)
            {
                throw new System.Exception("Admin Corps for UnitId " + unitid + " is null.");
            }
            this.AdminCorpsId = admincorps.AdminCorpsId;
            this.ParentAdminCorpsId = admincorps.ParentAdminCorpsId;
            this.Name = admincorps.Name;
            this.SortName = admincorps.SortName;
            this.Code = admincorps.Code;
            this.ParentUnitId = admincorps.ParentUnitId;            
            this.Lookup = admincorps.Lookup;
            this.DisplayName = admincorps.DisplayName;
            this.UnitDisplayName = admincorps.UnitDisplayName;
        }
        public string Name { get; set; }
public string SortName { get; set; }
        public string DisplayName { get; set; }
        public string UnitDisplayName { get; set; }
        public string Code { get; set; }
        public string Lookup { get; set; }
        public int? ParentUnitId { get; set; }

        public int? ParentAdminCorpsId { get; set; }
        public int AdminCorpsId { get; set; }
    }

    public class BllMissions : List<BllMission>
    {
        public BllMissions(ICollection<MissionUnit> missionUnits)
        {
            foreach (var mu in missionUnits.OrderBy(mu=>mu.Mission.SortOrder))
            {
                var mission = new BllMission(mu);
                this.Add(mission);
            }
        }
    }
}