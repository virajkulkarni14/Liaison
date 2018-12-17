using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liaison.BLL.Models.Unit.Abstracts;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class Battalion : TwoBar
    {
        public string UnitTypeVariant { get; set; }
        public string TerritorialDesignation { get; set; }
        public string UniqueName { get; set; }
        public Battalion(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.Number = sqlUnit.Number;
            this.UseOrdinal = sqlUnit.UseOrdinal;
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
            this.UnitTypeVariant = sqlUnit.UnitTypeVariant;

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
            return this.AdminCorps.DisplayName;
        }

        public override string GetName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Number.ToOrdinal(this.UseOrdinal) + " ");

            if (this.ServiceType == ServiceTypeBLL.Reserve)
            {
                sb.Append("(R) ");
            }
            else if (this.ServiceType == ServiceTypeBLL.Volunteer)
            {
                sb.Append("(V) (" + this.TerritorialDesignation + ") ");
            }

            List<string> missions = new List<string>() {"Civil Affairs", "Psychological Operations", "Commando"};
            if (string.IsNullOrWhiteSpace(this.UniqueName))
            {
                if (!string.IsNullOrWhiteSpace(this.MissionName))
                {
                    if (this.Number == null)
                    {
                        sb.Append(this.MissionName + " ");
                    }
                    else
                    {
                        if (missions.Contains(this.MissionName))
                        {
	                        
                            sb.Append(this.MissionName);
	                        if (this.MissionName != "Commando")
	                        {
		                        sb.Append(" ");
	                        }
						}
                        else
                        {
                            sb.Append("(" + this.MissionName + ") ");
                        }
                    }
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(this.UniqueName))
                {
                    sb.Append("(" + this.UniqueName + ") ");
                }

                sb.Append(this.MissionName + " ");
            }


	        if (this.AdminCorps?.AdminCorpsId != (int) Helper.Enumerators.AdminCorps.RoyalMarineCommando)
	        {
		        sb.Append("Bn.");
	        }

	        if (!string.IsNullOrWhiteSpace(this.UnitTypeVariant))
            {
                sb.Append(" (" + this.UnitTypeVariant + ")");
            }



            sb.Append(ResourceStrings.Seperator +this.AdminCorps?.UnitDisplayName);
            return sb.ToString();
        }

        public override string GetEquipment()
        {
            return "";
        }
    }
}