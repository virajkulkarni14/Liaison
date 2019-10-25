using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using Liaison.BLL.Models.Unit.Abstracts;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class Division:TwoStar
    {
        public string UnitTypeVariant { get; set; }
        public string CommandName { get; set; }
        public string LegacyMissionName { get; set; }
        public string UniqueName { get; set; }
        public string TerritorialDesignation { get; set; }
        public Division(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.Number = sqlUnit.Number;
            this.UseOrdinal = sqlUnit.UseOrdinal;
            this.NickName = sqlUnit.NickName;
            this.LegacyMissionName = sqlUnit.LegacyMissionName;
            this.CommandName = sqlUnit.CommandName;
            this.UniqueName = sqlUnit.UniqueName;
            this.MissionName = sqlUnit.MissionName;
            this.RankLevel = sqlUnit.Rank.RankLevel;
            this.RankStar = sqlUnit.Rank.Rank1;
            this.Service = (ServicesBll)sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL)sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.Decommissioned = sqlUnit.Decommissioned ?? false;
            this.TerritorialDesignation = sqlUnit.TerritorialDesignation;
            this.UnitTypeVariant = sqlUnit.UnitTypeVariant;

            this.Mission = new BllMissions(sqlUnit.MissionUnits);
            this.Base = new BLLBase(sqlUnit.Bases.FirstOrDefault());
            this.Indices = sqlUnit.UnitIndexes.OrderBy(x => x.DisplayOrder).Where(x => x.IsDisplayIndex).Select(x => x.IndexCode).ToList();
            this.SortIndex = GetSortIndex(sqlUnit.UnitIndexes);
            this.AdminCorps = new BLLAdminCorps(sqlUnit.AdminCorp, this.UnitId);

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
            var unitname = "Division";
            if (this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineLogistics)
            {
                unitname = "Group";
            }

            if (this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.DGSpecialForces||
                this.AdminCorps.AdminCorpsId==(int)Helper.Enumerators.AdminCorps.OffCivilAffairs)
            {
                if (this.MissionName != "Commando")
                {
                    unitname = "Command";
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(this.Number.ToOrdinal(this.UseOrdinal)+" ");

            if (string.IsNullOrWhiteSpace(this.LegacyMissionName))
            {
                if (this.ServiceType == ServiceTypeBLL.Reserve)
                {
                    sb.Append("(R) ");
                }
                else if (this.ServiceType == ServiceTypeBLL.Volunteer)
                {
                    sb.Append("(V) ");
                }

                if (!string.IsNullOrWhiteSpace(this.TerritorialDesignation))
                {
                    sb.Append("(" + this.TerritorialDesignation + ") ");
                }

                //if (!string.IsNullOrWhiteSpace(this.UnitTypeVariant))
                //{
                //    sb.Append("(" + this.UnitTypeVariant + ") ");
                //}

                if (!string.IsNullOrWhiteSpace(this.MissionName))
                {
                    sb.Append(this.MissionName + " ");
                }

                if (!string.IsNullOrWhiteSpace(this.UniqueName))
                {
                    sb.Append(" (" + this.UniqueName + ") ");
                }

                sb.Append(unitname);

                if (!string.IsNullOrWhiteSpace(this.UnitTypeVariant))
                {
                    sb.Append(" (" + this.UnitTypeVariant + ")");
                }
            }
            else
            {
                sb.Append(this.LegacyMissionName + " ");
                sb.Append(unitname);
                if (!string.IsNullOrWhiteSpace(this.MissionName))
                {
                    sb.Append(" (" + this.MissionName + ") ");
                }
            }

            var divname =  sb.ToString();

            if (string.IsNullOrWhiteSpace(this.CommandName))
            {
                return divname;
            }
            else
            {
                StringBuilder sb2 = new StringBuilder();
                sb2.Append(this.CommandName);
                sb2.Append(" / ");
                sb2.Append(divname);
                return sb2.ToString();
            }
        }

        public override int GetRankLevel()
        {
            return RankLevel ?? 0;
        }
    }
}