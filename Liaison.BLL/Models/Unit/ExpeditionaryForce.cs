using System.Linq;
using System.Text;
using Liaison.BLL.Models.Unit.Abstracts;
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
			this.AdminCorps = new BLLAdminCorps(sqlUnit.AdminCorp, this.UnitId);
			this.Decommissioned = sqlUnit.Decommissioned ?? false;

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

	    public override string GetAdminCorps()
	    {
			return this.AdminCorps.DisplayName;
		}

	    public override int GetRankLevel()
		{
			return RankLevel ?? 0;
		}
	}
}