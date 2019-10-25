using System.Linq;
using System.Text;
using Liaison.BLL.Models.Unit.Abstracts;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class NumberedAirForce : ThreeStar
    {
        public NumberedAirForce(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.UnitGuid = sqlUnit.UnitGuid;
            this.Number = sqlUnit.Number;
            this.NickName = sqlUnit.NickName;           
            this.MissionName = sqlUnit.MissionName;
            this.RankLevel = sqlUnit.Rank.RankLevel;
            this.RankStar = sqlUnit.Rank.Rank1;
            this.Service = (ServicesBll)sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL)sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            if (sqlUnit.AdminCorp != null)
            {
                this.AdminCorps = new BLLAdminCorps(sqlUnit.AdminCorp, this.UnitId);
            }

            this.Mission = new BllMissions(sqlUnit.MissionUnits);
            this.Base = new BLLBase(sqlUnit.Bases.FirstOrDefault());
            this.Indices = sqlUnit.UnitIndexes.OrderBy(x => x.DisplayOrder).Where(x => x.IsDisplayIndex).Select(x => x.IndexCode).ToList();
            this.SortIndex = GetSortIndex(sqlUnit.UnitIndexes);

            var relMain = sqlUnit.RelationshipsFrom.ToList();
            var relt = sqlUnit.RelationshipsTo.ToList();

            relMain.AddRange(relt);
            this.Relationships = new BLLRelationships(sqlUnit.UnitId, relt);

        }
        public override string GetAdminCorps()
        {
            return this.AdminCorps?.DisplayName;
        }
        public override string GetName()
        {
            if (this.Number == null)
            {
                return this.MissionName;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Number.ToOrdinalAsWord() + " ");
            sb.Append("Air Force");
            if (!string.IsNullOrWhiteSpace(this.MissionName))
            {
                sb.Append(" / " + this.MissionName);
            }

            return sb.ToString();
        }

        public override int GetRankLevel()
        {
            return RankLevel ?? 0;
        }
    }
}