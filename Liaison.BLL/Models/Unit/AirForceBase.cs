using System.Linq;
using System.Text;
using Liaison.BLL.Models.Unit.Abstracts;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class AirForceBase : OneStar
    {
        public string TerritorialDesignation { get; set; }
        public AirForceBase (Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.UnitGuid = sqlUnit.UnitGuid;
            this.Number = sqlUnit.Number;
            this.RankLevel = sqlUnit.Rank.RankLevel;
            this.RankStar = sqlUnit.Rank.Rank1;
            this.Service = (ServicesBll) sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL) sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.CommissionedName = sqlUnit.CommandName;
            this.MissionName = sqlUnit.MissionName;
            this.TerritorialDesignation = sqlUnit.TerritorialDesignation;

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

        public string CommissionedName { get; set; }

        //public string AdminCorpsCode { get; set; }

        //public string AdminCorpsName { get; set; }

        public override string GetAdminCorps()
        {
            return this.AdminCorps.DisplayName;
        }

        public override string GetName()
        {
            StringBuilder sb = new StringBuilder("No. ");
            sb.Append(this.Number + " ");

            string basetype = "Air Force Base";

            if (this.ServiceType == ServiceTypeBLL.Reserve)
            {
                sb.Append("(R) ");
                basetype = "Air Reserve Base";
            }
            else if (this.ServiceType == ServiceTypeBLL.Volunteer)
            {
                sb.Append("(V) (" + this.TerritorialDesignation + ") ");
                basetype = "Auxiliary Air Base";
            }

            if (string.IsNullOrWhiteSpace(this.MissionName))
            {
                sb.Append(basetype);

                sb.Append(ResourceStrings.Seperator + this.AdminCorps?.UnitDisplayName);

                if (!string.IsNullOrWhiteSpace(this.CommissionedName))
                {
                    sb.Append(" / " + this.CommissionedName);
                }
            }
            else
            {
                sb.Append(this.MissionName + " Force HQ");
                sb.Append(ResourceStrings.Seperator + this.AdminCorps?.UnitDisplayName);
            }


            return sb.ToString();
        }

        public override int GetRankLevel()
        {
            return RankLevel ?? 0;
        }
    }
}