using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liaison.BLL.Models.Unit.Abstracts;
using Liaison.BLL.Models.Unit.Interfaces;
using Liaison.Data.Sql.Edmx;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class Facility : AUnit, IUnit
    {
        public Facility(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.UnitGuid = sqlUnit.UnitGuid;
            this.MissionName = sqlUnit.MissionName;
            this.CommandName = sqlUnit.CommandName;
            if (sqlUnit.Ships.Any())
                this.Vessel = new Vessel(sqlUnit.Ships.First());
            this.Service = (ServicesBll)sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL)sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.RankLevel = sqlUnit.Rank.RankLevel;
            this.RankStar = sqlUnit.Rank.Rank1;

            this.Mission = new BllMissions(sqlUnit.MissionUnits);
            this.Base = new BLLBase(sqlUnit.Bases.FirstOrDefault());
            this.Indices = sqlUnit.UnitIndexes.OrderBy(x => x.DisplayOrder).Where(x => x.IsDisplayIndex)
                .Select(x => x.IndexCode).ToList();
            this.Indices.Add(this.Base.AirfieldCode);
            this.SortIndex = GetSortIndex(sqlUnit.UnitIndexes);
            //this.AdminCorps = new BLLAdminCorps(sqlUnit.AdminCorp);

            var relMain = sqlUnit.RelationshipsFrom.ToList();
            var relt = sqlUnit.RelationshipsTo.ToList();

            relMain.AddRange(relt);
            this.Relationships = new BLLRelationships(sqlUnit.UnitId, relt);
        }

        public Vessel Vessel { get; set; }

        public string GetAdminCorps()
        {
            return "";
        }
        public string CommandName { get; set; }

        public string GetName()
        {
            Dictionary<string, string> diction = Liaison.Data.Sql.GetStuff.GetDictionary("FacilityGetName");


            StringBuilder sb = new StringBuilder();
            if (this.Vessel != null)
            {
                sb.Append(this.Vessel.Prefix + " " + this.Vessel.ShipName + " (");
            }

            sb.Append(this.MissionName + " " + this.CommandName);
            if (this.Vessel != null)
            {
                sb.Append(")");
            }

            var returnable = sb.ToString();
            foreach (var thing in diction)
            {
                returnable = returnable.Replace(thing.Key, thing.Value);
            }

            return returnable;
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
            return this.Indices == null ? string.Empty : string.Join(",", this.Indices);
        }


        public string GetEquipment()
        {
            return "";
        }

        public bool IsTaskForce => false;
        public bool IsDecommissioned()
        {
            return Decommissioned;
        }
    }
}