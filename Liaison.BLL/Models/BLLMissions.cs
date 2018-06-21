using System.Collections.Generic;
using Liaison.Data.Sql.Edmx;

namespace Liaison.BLL.Models
{
    public class BLLAdminCorps
    {
        public BLLAdminCorps(AdminCorp admincorps)
        {
            this.AdminCorpsId = admincorps.AdminCorpsId;
            this.Name = admincorps.Name;
            this.Code = admincorps.Code;
           
            this.Lookup = admincorps.Lookup;

        }

        public string Lookup { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int AdminCorpsId { get; set; }
    }

    public class BllMissions : List<BllMission>
    {
        public BllMissions(ICollection<MissionUnit> missionUnits)
        {
            foreach (var mu in missionUnits)
            {
                var mission = new BllMission(mu);
                this.Add(mission);
            }
        }
    }
}