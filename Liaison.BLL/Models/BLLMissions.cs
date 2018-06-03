using System.Collections.Generic;
using Liaison.Data.Sql.Edmx;

namespace Liaison.BLL.Models
{
    public class BLLMissions : List<BLLMission>
    {
        public BLLMissions(ICollection<MissionUnit> missionUnits)
        {
            foreach (var mu in missionUnits)
            {
                var mission = new BLLMission(mu);
                this.Add(mission);
            }
        }
    }
}