using System.Linq;
using System.Text;
using Liaison.BLL.Models.Equipment;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class AirWing : ThreeBar
    {              
        public AirWing(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.UnitGuid = sqlUnit.UnitGuid;
            this.Number = sqlUnit.Number;
            this.MissionName = sqlUnit.MissionName;
            this.CommandName = sqlUnit.CommandName;
            this.UseOrdinal = sqlUnit.UseOrdinal;
            this.RankLevel = sqlUnit.Rank.RankLevel;
            this.RankStar = sqlUnit.Rank.Rank1;
            this.Service = (ServicesBll) sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL) sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.CanHide = sqlUnit.CanHide;

            this.Mission = new BLLMissions(sqlUnit.MissionUnits);
            this.Base = new BLLBase(sqlUnit.Bases.FirstOrDefault());
            this.Indices = sqlUnit.UnitIndexes.OrderBy(x => x.DisplayOrder).Where(x => x.IsDisplayIndex)
                .Select(x => x.IndexCode).ToList();
            this.SortIndex = GetSortIndex(sqlUnit.UnitIndexes);
            this.Equipment = sqlUnit.EquipmentOwners.ToEquipmentList();

            if (sqlUnit.AdminCorp != null)
            {
                this.AdminCorpsName = sqlUnit.AdminCorp.Name;
                this.AdminCorpsCode = sqlUnit.AdminCorp.Code;
            }

            var relMain = sqlUnit.RelationshipsFrom.ToList();
            var relt = sqlUnit.RelationshipsTo.ToList();

            relMain.AddRange(relt);
            this.Relationships = new BLLRelationships(sqlUnit.UnitId, relt);
            //this.Parents = relMain.Where(p => p.RelationshipsTo.UnitId == UnitId);
            //this.Parents2 = relMain.Where(p => p.RelToUnitId == UnitId).ToList();
            //this.Parents1 = relMain.Where(p => p.RelToUnitId == UnitId).ToList().Select(r=>r.RelFromUnitId).ToList();
        }

        public string CommandName { get; set; }

        public string AdminCorpsCode { get; set; }

        public string AdminCorpsName { get; set; }
        
        public override string GetName()
        {
            StringBuilder sb = new StringBuilder();
            if (this.UseOrdinal)
            {
                sb.Append(this.Number.ToOrdinal(this.UseOrdinal) + " ");
            }
            else
            {
                sb.Append("No. " + this.Number + " ");
            }

            //sb.Append("Naval ");
            sb.Append(this.MissionName + " ");
            if (!string.IsNullOrWhiteSpace(this.CommandName))
            {
                sb.Append("(" + this.CommandName + ") ");
            }
            sb.Append("Wing");
            if (!string.IsNullOrWhiteSpace(this.AdminCorpsCode))
            {
                sb.Append(ExtensionMethods.Seperator + this.AdminCorpsCode);
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