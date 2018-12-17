using System.Linq;
using System.Text;
using Liaison.BLL.Models.Unit.Abstracts;
using Liaison.Data.Sql.Edmx;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class AirGroup : TwoStar
    {
        public new AdminCorps AdminCorps { get; set; }
        public string UnitTypeVariant { get; set; }
        public string CommandName { get; set; }
        public AirGroup(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.UnitGuid = sqlUnit.UnitGuid;
            this.Number = sqlUnit.Number;
            this.MissionName = sqlUnit.MissionName;
            this.CommandName = sqlUnit.CommandName;
            this.UnitTypeVariant = sqlUnit.UnitTypeVariant;
            this.UseOrdinal = sqlUnit.UseOrdinal;
            this.RankLevel = sqlUnit.Rank.RankLevel;
            this.RankStar = sqlUnit.Rank.Rank1;
            this.Service = (ServicesBll)sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL)sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.CanHide = sqlUnit.CanHide;

            this.Mission = new BllMissions(sqlUnit.MissionUnits);
            this.Base = new BLLBase(sqlUnit.Bases.FirstOrDefault());
            this.Indices = sqlUnit.UnitIndexes.OrderBy(x => x.DisplayOrder).Where(x => x.IsDisplayIndex)
                .Select(x => x.IndexCode).ToList();
            this.SortIndex = GetSortIndex(sqlUnit.UnitIndexes);          

            if (sqlUnit.AdminCorp != null)
            {
                // this.AdminCorpsName = sqlUnit.AdminCorp.Name;
                // this.AdminCorpsCode = sqlUnit.AdminCorp.Code;
                //this.AdminCorps = new BLLAdminCorps(sqlUnit.AdminCorp);
                this.AdminCorps = new AdminCorps(sqlUnit.AdminCorp.Code, sqlUnit.AdminCorp.Name, sqlUnit.AdminCorp.AdminCorpsId);
            }

            var relMain = sqlUnit.RelationshipsFrom.ToList();
            var relt = sqlUnit.RelationshipsTo.ToList();

            relMain.AddRange(relt);
            this.Relationships = new BLLRelationships(sqlUnit.UnitId, relt);
            //this.Parents = relMain.Where(p => p.RelationshipsTo.UnitId == UnitId);
            //this.Parents2 = relMain.Where(p => p.RelToUnitId == UnitId).ToList();
            //this.Parents1 = relMain.Where(p => p.RelToUnitId == UnitId).ToList().Select(r=>r.RelFromUnitId).ToList();
        }
        public override string GetName()
        {
            StringBuilder sb = new StringBuilder();
            if (this.UseOrdinal)
            {
                sb.Append(this.Number.ToOrdinal(this.UseOrdinal) + " ");
            }
            else if (this.Number != null)
            {
                sb.Append("No. " + this.Number + " ");
            }

            //sb.Append("Naval ");
            sb.Append(this.MissionName + " ");
            if (this.Service == ServicesBll.AirForce)
            {
                if (!string.IsNullOrWhiteSpace(this.CommandName))
                {
                    sb.Append("(" + this.CommandName + ") ");
                }
            }
            else if (this.Service == ServicesBll.Navy)
            {
                if (!string.IsNullOrWhiteSpace(this.CommandName))
                {
                    sb.Append(this.CommandName);
                }
            }
            else if (this.Service == ServicesBll.Marines)
            {
                sb.Append(" Aircraft ");
            }

            if (string.IsNullOrWhiteSpace(this.UnitTypeVariant))
            {
                if (string.IsNullOrWhiteSpace(this.CommandName))
                {
                    sb.Append("Group");
                }
            }
            else
            {
                sb.Append(this.UnitTypeVariant);
            }

            if (this.AdminCorps == null)
            {
                using (var content = new LiaisonEntities())
                {
                    var thisThing = content.Units.First(u => u.UnitId == this.UnitId);

                    var sqlAdminCorps = content.AdminCorps.FirstOrDefault(ac => ac.AdminCorpsId == thisThing.AdminCorpsId); ;
                    if (sqlAdminCorps != null)
                    {
                        this.AdminCorps = new AdminCorps(sqlAdminCorps.Code, sqlAdminCorps.Name,
                            sqlAdminCorps.AdminCorpsId);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(this.AdminCorps?.Code))
            {
                sb.Append(ResourceStrings.Seperator + this.AdminCorps.Code);
            }

            return sb.ToString();
        }

        public override string GetAdminCorps()
        {
            return this.AdminCorps == null ? string.Empty : this.AdminCorps.Name;
        }

        public override int GetRankLevel()
        {
            return RankLevel ?? 0;
        }
    }
}