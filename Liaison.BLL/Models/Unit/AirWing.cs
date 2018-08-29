using System.Linq;
using System.Text;
using Liaison.BLL.Models.Equipment;
using Liaison.Data.Sql.Edmx;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class AirWing : ThreeBar
    {
        public new AdminCorps AdminCorps { get; set; }
        public string UnitTypeVariant { get; set; }
        public AirWing(Data.Sql.Edmx.Unit sqlUnit)
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
            this.Service = (ServicesBll) sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL) sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.CanHide = sqlUnit.CanHide;
            this.Decommissioned = sqlUnit.Decomissioned ?? false;

            this.Mission = new BllMissions(sqlUnit.MissionUnits);
            this.Base = new BLLBase(sqlUnit.Bases.FirstOrDefault());
            this.Indices = sqlUnit.UnitIndexes.OrderBy(x => x.DisplayOrder).Where(x => x.IsDisplayIndex)
                .Select(x => x.IndexCode).ToList();
            this.SortIndex = GetSortIndex(sqlUnit.UnitIndexes);
            this.Equipment = sqlUnit.EquipmentOwners.ToEquipmentList();

            if (sqlUnit.AdminCorp != null)
            {
                this.AdminCorps = new AdminCorps(sqlUnit.AdminCorp.Code, sqlUnit.AdminCorp.Name, sqlUnit.AdminCorp.AdminCorpsId);
            }

            var relMain = sqlUnit.RelationshipsFrom.ToList();
            var relt = sqlUnit.RelationshipsTo.ToList();

            relMain.AddRange(relt);
            this.Relationships = new BLLRelationships(sqlUnit.UnitId, relt);
        }

        public string CommandName { get; set; }

        public override string GetAdminCorps()
        {
            return this.AdminCorps == null ? string.Empty : this.AdminCorps.Name;
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
            if (!string.IsNullOrWhiteSpace(this.MissionName))
            {
                if (this.AdminCorps.Id == (int) Helper.Enumerators.AdminCorps.RoyalMarinesAirArm)
                {
                    sb.Append("Marine ");
                }
            }

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
                if (string.IsNullOrWhiteSpace(this.MissionName))
                {
                    sb.Append("Marine Air ");
                }
            }

            if (string.IsNullOrWhiteSpace(this.UnitTypeVariant))
            {
                if (string.IsNullOrWhiteSpace(this.CommandName))
                {
                    sb.Append("Wing");
                }
            }
            else
            {
                sb.Append(this.UnitTypeVariant);
            }

            if (this.AdminCorps == null)
            {
                this.AdminCorps = AUnit.GetAdminCorpsHelper(this);
            }

            if (!string.IsNullOrWhiteSpace(this.AdminCorps?.Code))
            {
                sb.Append(ExtensionMethods.Seperator + this.AdminCorps.Code);
            }

            return sb.ToString();
        }




        public override string GetEquipment()
        {
            bool showAltName = true;
            StringBuilder sb = new StringBuilder();
            foreach (var thing in this.Equipment)
            {
                if (thing.GetType() == typeof(BLLAircraft))
                {
                    if (thing is BLLAircraft airc)
                    {
                        sb.Append(airc.PAA + " " + airc.Name + " " + airc.Mark);
                        if (showAltName)
                        {
                            sb.Append(" [" + airc.AltCode + " " + airc.AltName + "]");
                        }
                    }
                }

                sb.Append(ExtensionMethods.Seperator);
            }

            var x = sb.ToString();
            return x.Length > 0 ? x.Substring(0, x.Length - ExtensionMethods.Seperator.Length) : x;
        }
    }
}