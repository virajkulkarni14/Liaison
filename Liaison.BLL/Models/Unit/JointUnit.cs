using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liaison.BLL.Models.Equipment;
using Liaison.BLL.Models.Objects;
using Liaison.BLL.Models.Unit.Abstracts;
using Liaison.BLL.Models.Unit.Interfaces;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class JointUnit : AUnit,IUnit
    {
        public new BLLAdminCorps AdminCorps { get; set; }
        public bool UseOrdinal { get; set; }
        public List<IEquipment> Equipment { get; set; }
        public string Letter { get; set; }
        public string CommandName { get; set; }
        public string TerritorialDesignation { get; set; }

        private int TwoBarTab = 11;
        private int OneBarTab = 12;
        //private string OneBar = "|";


        public JointUnit(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.UnitGuid = sqlUnit.UnitGuid;
            this.Number = sqlUnit.Number;
            this.TerritorialDesignation = sqlUnit.TerritorialDesignation;
            this.MissionName = sqlUnit.MissionName;
            this.UseOrdinal = sqlUnit.UseOrdinal;
            this.RankLevel = sqlUnit.Rank.RankLevel;
            this.RankStar = sqlUnit.Rank.Rank1;
            this.Service = (ServicesBll) sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL) sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.AdminCorps = new BLLAdminCorps(sqlUnit.AdminCorp, this.UnitId);
            this.Equipment = sqlUnit.EquipmentOwners.ToEquipmentList();
            this.Letter = sqlUnit.Letter;
            this.CommandName = sqlUnit.CommandName;

            this.Mission = new BllMissions(sqlUnit.MissionUnits);
            this.Base = new BLLBase(sqlUnit.Bases.FirstOrDefault());
            this.Indices = sqlUnit.UnitIndexes.OrderBy(x => x.DisplayOrder).Where(x => x.IsDisplayIndex)
                .Select(x => x.IndexCode).ToList();
            this.SortIndex = GetSortIndex(sqlUnit.UnitIndexes);

            var relMain = sqlUnit.RelationshipsFrom.ToList();
            //var relt = doRelTos ? sqlUnit.RelationshipsTo.ToList() : new List<Relationship>();             
            var relt = sqlUnit.RelationshipsTo.ToList();

            relMain.AddRange(relt);
            this.Relationships = new BLLRelationships(sqlUnit.UnitId, relt);
        }

        public string GetName()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(this.MissionName))
            {
                sb.Append(this.MissionName);
            }

            if (!string.IsNullOrWhiteSpace(this.CommandName))
            {
                sb.Append(this.CommandName);
            }

            return sb.ToString();
        }


        public string PrintTree()
        {
            throw new NotImplementedException();
        }

        public int GetRankLevel()
        {
            if (this.RankLevel == 11)
            {
                return TwoBarTab;
            }
            return OneBarTab;
        }

        public string GetRankStar()
        {
            return RankStar;
        }

        public string GetIndexes()
        {
            return this.Indices == null ? string.Empty : string.Join(",", this.Indices);
        }

        public EquipmentContainer GetEquipment()
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

                sb.Append(ResourceStrings.Seperator);
            }

            var x = sb.ToString();
            return new EquipmentContainer( (x.Length > 0 ? x.Substring(0, x.Length - ResourceStrings.Seperator.Length) : x).Replace("_", ""));
        }

        public bool IsTaskForce => false;
        public bool IsDecommissioned()
        {
            return Decommissioned;
        }


        public string GetAdminCorps()
        {
            return this.AdminCorps == null ? string.Empty : this.AdminCorps.DisplayName;
        }
    }
}