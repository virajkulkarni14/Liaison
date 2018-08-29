using System;
using System.Linq;
using System.Text;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class Regiment : ThreeBar
    {
        public string UniqueName { get; set; }
        public  Regiment(Data.Sql.Edmx.Unit sqlUnit)
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

        public override string GetEquipment()
        {
            return "";
        }

        public override string GetAdminCorps()
        {
            if (this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineLightInfantry
                || this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineArtillery
                ||this.AdminCorps.AdminCorpsId==(int)Helper.Enumerators.AdminCorps.RoyalMarineLogistics)
            {
                return this.AdminCorps.Code;
            }

            throw new Exception();
        }

        public override string GetName()
        {
            StringBuilder sb = new StringBuilder();
            bool isAcceptable = false;
            if (this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineLightInfantry
                || this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineArtillery)
            {
                sb.Append(this.Number.ToOrdinal(this.UseOrdinal) + " ");
                sb.Append(this.UniqueName);
                isAcceptable = true;
            } 
                        else if (this.AdminCorps.AdminCorpsId ==
                                 (int) Helper.Enumerators.AdminCorps.RoyalMarineLogistics)
            {
                sb.Append(this.Number.ToOrdinal(this.UseOrdinal) + " ");
                sb.Append(this.MissionName+ " ");
                sb.Append("Regiment");
                isAcceptable = true;
            }

            if (isAcceptable)
            {
                return sb.ToString();
            }

            throw new Exception();
        }
    }
}