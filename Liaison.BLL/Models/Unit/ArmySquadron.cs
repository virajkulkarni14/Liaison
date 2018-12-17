using System.Linq;
using System.Text;
using Liaison.BLL.Models.Unit.Abstracts;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class ArmySquadron : OneBar
    {
        public ArmySquadron(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.Number = sqlUnit.Number;
            this.UseOrdinal = sqlUnit.UseOrdinal;
            this.Letter = sqlUnit.Letter;
            this.NickName = sqlUnit.NickName;
            this.UniqueName = sqlUnit.UniqueName;
            this.MissionName = sqlUnit.MissionName;
            this.RankLevel = sqlUnit.Rank.RankLevel;
            this.RankStar = sqlUnit.Rank.Rank1;
            this.Service = (ServicesBll)sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL)sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.AdminCorps = new BLLAdminCorps(sqlUnit.AdminCorp);
            this.Decommissioned = sqlUnit.Decommissioned ?? false;
            this.TerritorialDesignation = sqlUnit.TerritorialDesignation;
	        this.CommandName = sqlUnit.CommandName;

            this.Mission = new BllMissions(sqlUnit.MissionUnits);
            this.Base = new BLLBase(sqlUnit.Bases.FirstOrDefault());
            this.Indices = sqlUnit.UnitIndexes.OrderBy(x => x.DisplayOrder).Where(x => x.IsDisplayIndex).Select(x => x.IndexCode).ToList();
            this.SortIndex = GetSortIndex(sqlUnit.UnitIndexes);

            var relMain = sqlUnit.RelationshipsFrom.ToList();
            var relt = sqlUnit.RelationshipsTo.ToList();

            relMain.AddRange(relt);
            this.Relationships = new BLLRelationships(sqlUnit.UnitId, relt);

        }

        public string Letter { get; set; }

        public string UniqueName { get; set; }

        public bool UseOrdinal { get; set; }
        public override string GetAdminCorps()
        {
            return this.AdminCorps.DisplayName;
        }

        public override string GetName()
        {
            StringBuilder sb = new StringBuilder();

	        bool unitWithId = !(this.Number == null && this.Letter == null);
	        bool ishq = false;

	        if (unitWithId)
	        {
		        if (this.Number != null)
		        {
			        sb.Append(this.Number + " ");
		        }

		        if (this.Letter != null)
		        {
			        sb.Append(this.Letter + " ");
		        }

		        //if (this.LegacyMissionName != null)
		        //{
		        //    sb.Append("(" + this.LegacyMissionName + ") ");
		        //}
	        }
	        else
	        {

		        if (this.MissionName != null)
		        {
			        if (this.MissionName == ResourceStrings.HQHQ)
			        {
				        sb.Append("HHS");
				        ishq = true;
			        }
			        else if (this.MissionName == ResourceStrings.HQS)
			        {
				        sb.Append("HSS");
				        ishq = true;
			        }
			        else
			        {
				        sb.Append(this.MissionName + " ");
				        sb.Append("Sqn.");
				        AUnit.GetServiceType(sb, this.ServiceType, this.TerritorialDesignation, true, true);

				        ishq = true;
			        }
		        }
	        }

	        if (!ishq)
	        {
		        AUnit.GetServiceType(sb, this.ServiceType, this.TerritorialDesignation, true, true);
	        }

	        if (unitWithId)
	        {
		        sb.Append(this.MissionName + " ");
	        }


	        if (!ishq)
	        {
		        sb.Append("Sqn.");
	        }

	        var endstring = !string.IsNullOrWhiteSpace(this.CommandName)
		        ? this.CommandName.Replace("_", "")
		        : this.AdminCorps.UnitDisplayName;

	        if (!string.IsNullOrWhiteSpace(endstring))
	        {
		        sb.Append(", ");

		        sb.Append(endstring);

	        }
	        return sb.ToString();
		}

        public string CommandName { get; set; }

        public string TerritorialDesignation { get; set; }

        public override string GetEquipment()
        {
            return "";
        }
    }
}