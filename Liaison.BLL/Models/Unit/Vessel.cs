using System;
using System.Linq;
using System.Text;
using Liaison.BLL.Models.Objects;
using Liaison.BLL.Models.Unit.Abstracts;
using Liaison.BLL.Models.Unit.Interfaces;
using Liaison.Data.Sql.Edmx;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
	public class Vessel : AUnit, IUnit
    {
        public string GetAdminCorps()
        {
            return "";
        }
        public Vessel(Data.Sql.Edmx.Unit sqlUnit)
        {
            Ship ship = null;
            try
            {
                ship = sqlUnit.Ships.First();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
            

            this.Decommissioned = sqlUnit.Decommissioned ?? false;
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
            return this.Indices == null ? string.Empty : string.Join(ResourceStrings.Seperator, this.Indices);
        }

        public EquipmentContainer GetEquipment()
        {
            if (this.ShipClass == null)
            {
                return new EquipmentContainer(string.Empty);
            }

            return new EquipmentContainer(this.ShipClass.GetClassName());
        }

        public bool IsTaskForce { get; }
        public bool IsDecommissioned()
        {
            return Decommissioned;
        }
    }
}