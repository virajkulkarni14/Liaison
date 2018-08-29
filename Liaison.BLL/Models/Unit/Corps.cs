using System.Linq;
using System.Text;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class ExpeditionaryForce : ThreeStar
    {
        public ExpeditionaryForce(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.UnitGuid = sqlUnit.UnitGuid;
            this.Number = sqlUnit.Number;
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

            var relMain = sqlUnit.RelationshipsFrom.ToList();
            var relt = sqlUnit.RelationshipsTo.ToList();

            relMain.AddRange(relt);
            this.Relationships = new BLLRelationships(sqlUnit.UnitId, relt);

        }

        public override string GetName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Number.ToRomanNumerals() + " ");

            sb.Append(this.MissionName);

            return sb.ToString();
        }

        public override int GetRankLevel()
        {
            return RankLevel ?? 0;
        }
    }
    public class Corps :ThreeStar
    {
        public string LegacyMissionName { get; set; }
        public Corps(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.UnitGuid = sqlUnit.UnitGuid;
            this.Number = sqlUnit.Number;
            this.NickName = sqlUnit.NickName;
            this.LegacyMissionName = sqlUnit.LegacyMissionName;
            this.MissionName = sqlUnit.MissionName;
            this.RankLevel = sqlUnit.Rank.RankLevel;
            this.RankStar = sqlUnit.Rank.Rank1;
            this.Service = (ServicesBll)sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL)sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];

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

        public override string GetName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Number.ToRomanNumerals() + " ");
            if (!string.IsNullOrWhiteSpace(this.LegacyMissionName))
            { 
                sb.Append("\"" + this.LegacyMissionName + "\"" + " ");
                sb.Append("Corps");
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

                sb.Append("Corps");
            }

            return sb.ToString();
        }

        public override int GetRankLevel()
        {
            return RankLevel ?? 0;
        }

    }
}