using System.Linq;
using System.Text;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class Division:TwoStar
    {
        public string LegacyMissionName { get; set; }
        public Division(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.Number = sqlUnit.Number;
            this.UseOrdinal = sqlUnit.UseOrdinal;
            this.NickName = sqlUnit.NickName;
            this.LegacyMissionName = sqlUnit.LegacyMissionName;
            this.MissionName = sqlUnit.MissionName;
            this.RankLevel = sqlUnit.Rank.RankLevel;
            this.RankStar = sqlUnit.Rank.Rank1;
            this.Service = (ServicesBll)sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL)sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.Decommissioned = sqlUnit.Decomissioned ?? false;

            this.Mission = new BllMissions(sqlUnit.MissionUnits);
            this.Base = new BLLBase(sqlUnit.Bases.FirstOrDefault());
            this.Indices = sqlUnit.UnitIndexes.OrderBy(x => x.DisplayOrder).Where(x => x.IsDisplayIndex).Select(x => x.IndexCode).ToList();
            this.SortIndex = GetSortIndex(sqlUnit.UnitIndexes);
            this.AdminCorps = new BLLAdminCorps(sqlUnit.AdminCorp);

            var relMain = sqlUnit.RelationshipsFrom.ToList();
            var relt = sqlUnit.RelationshipsTo.ToList();

            relMain.AddRange(relt);
            this.Relationships = new BLLRelationships(sqlUnit.UnitId, relt);

        }

        public override string GetAdminCorps()
        {
            return this.AdminCorps.Name;
        }
        public override string GetName()
        {
            var unitname = "Division";
            if (this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineLogistics)
            {
                unitname = "Group";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(this.Number.ToOrdinal(this.UseOrdinal)+" ");

            if (!string.IsNullOrWhiteSpace(this.LegacyMissionName))
            {
                sb.Append(this.LegacyMissionName + " ");
                sb.Append(unitname);
                if (!string.IsNullOrWhiteSpace(this.MissionName))
                {
                    sb.Append(" (" + this.MissionName + ") ");
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(this.MissionName))
                {
                    sb.Append(this.MissionName + " ");
                }

                sb.Append(unitname);
            }

            return sb.ToString();
        }

        public override int GetRankLevel()
        {
            return RankLevel ?? 0;
        }
    }
}