using System;
using System.Linq;
using System.Text;
using Liaison.Data.Sql.Edmx;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class HCS
    {
        public HCS(string hCS, int? hCSNumber)
        {
            this.Code = hCS;
            if (hCSNumber.HasValue)
            {
                this.Number = hCSNumber.Value;
            }
        }

        public string Code
        {
            get; set;
        }

        public int Number { get; set; }
    }

    public class VesselClass
    {
        public VesselClass(ShipClass shipClass, bool isLeadShip)
        {
            if (shipClass == null) return;

            this.ClassName = shipClass.ClassName;
            this.ClassCode = new HCS(shipClass.ClassCodeHCS, shipClass.ClassCodeNumber);
            this.ModName = shipClass.ModName;
            this.IsLeadShip = isLeadShip;
        }

        public bool IsLeadShip { get; set; }

        public HCS ClassCode { get; set; }

        public string ClassName { get; set; }
        public string ModName { get; set; }

        public string GetClassName()
        {
            StringBuilder sb=new StringBuilder();
            if (this.IsLeadShip) { sb.Append("<u>"); }
            sb.Append(this.ClassName);
            sb.Append(" class");
            if (this.IsLeadShip) { sb.Append("</u>"); }
            if (!string.IsNullOrWhiteSpace(this.ModName)) { sb.Append(", " + this.ModName); }

            return sb.ToString();
        }
    }


    public class Vessel : AUnit, IUnit
    {
        public string GetAdminCorps()
        {
            return "";
        }
        public Vessel(Data.Sql.Edmx.Unit sqlUnit)
        {
            var ship = sqlUnit.Ships.First();

            this.Decommissioned = sqlUnit.Decomissioned ?? false;
            this.UnitId = sqlUnit.UnitId;
            this.UnitGuid = sqlUnit.UnitGuid;
            this.RankLevel = sqlUnit.Rank.RankLevel;
            this.RankStar = sqlUnit.Rank.Rank1;
            this.Service = (ServicesBll)sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL)sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.Prefix = sqlUnit.Ships.First().ShipPrefix.ShipPrefix1;
            this.ShipName = ship.Name;
            this.HCS = new HCS(ship.HCS, ship.HCSNumber);
            this.PennantNumber =new HCS(ship.PennantCode, ship.PennantNumber);

            if (ship.ShipClassMembers.Any())
            {
                if (ship.ShipClassMembers.Count > 1)
                {
                    throw new Exception("Too many ship classes");
                }
                this.ShipClass = new VesselClass(ship.ShipClassMembers.First().ShipClass, ship.ShipClassMembers.First().IsLeadBoat);
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

        public Vessel(Ship ship)
        {
            if (ship.UnitId != null) this.UnitId = ship.UnitId.Value;
            if (ship.Unit != null)
            {
                this.Prefix = ship.ShipPrefix.ShipPrefix1;
                this.ShipName = ship.Name;
                this.HCS = new HCS(ship.HCS, ship.HCSNumber);
                this.PennantNumber = new HCS(ship.PennantCode, ship.PennantNumber);
            }
        }

        public VesselClass ShipClass { get; set; }

        public HCS PennantNumber { get; set; }

        public string ShipName { get; set; }

        public string Prefix { get; set; }
        private HCS HCS { get; set; }
        public string GetName()
        {
          StringBuilder sb = new StringBuilder();
            sb.Append(this.Prefix+" ");
            sb.Append(this.ShipName);
            //sb.Append(" (");
            //sb.Append(this.HCS.Code + " " + this.HCS.Number);
            //sb.Append("/");
            //sb.Append(this.PennantNumber.Code + " " + this.PennantNumber.Number);
            //sb.Append(")");
            return sb.ToString();
        }

        public string PrintTree()
        {
            throw new System.NotImplementedException();
        }
        public int GetRankLevel()
        {
            return RankLevel ?? 0;
        }
        public string GetRankStar()
        {
            return RankStar;
        }

        public string GetIndexes()
        {
            return this.Indices == null ? string.Empty : string.Join(ExtensionMethods.Seperator, this.Indices);
        }

        public string GetEquipment()
        {
            if (this.ShipClass == null)
            {
                return "";
            }

return this.ShipClass.GetClassName();
       
        }

        public bool IsTaskForce { get; }
        public bool IsDecommissioned()
        {
            return Decommissioned;
        }
    }
}