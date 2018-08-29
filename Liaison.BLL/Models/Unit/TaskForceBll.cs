using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liaison.BLL.Models.Equipment;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class TaskForceBll : AUnit, IUnit
    {
        public string GetAdminCorps()
        {
            return "";
        }
        public List<IEquipment> Equipment { get; set; }
        public int? TaskGroup { get; set; }
        public int? TaskElement { get; set; }
        public int? TaskUnit { get; set; }
        public int? TaskForceNo { get; set; }
        public string TaskForceName { get; set; }

        public TaskForceBll(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.TaskForceName = sqlUnit.TaskForce.TaskForceName;
            this.TaskForceNo = sqlUnit.TaskForce.TaskForceNo;
            this.TaskGroup = sqlUnit.TaskForce.TaskGroup;
            this.TaskUnit = sqlUnit.TaskForce.TaskUnit;
            this.TaskElement = sqlUnit.TaskForce.TaskElement;

            this.MissionName = sqlUnit.MissionName;
            this.RankLevel = sqlUnit.Rank.RankLevel;
            this.RankStar = sqlUnit.Rank.Rank1;
            this.Service = (ServicesBll)sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL)sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.Equipment = sqlUnit.EquipmentOwners.ToEquipmentList();
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


        public string GetName()
        {
            string mission = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.MissionName))
            {
                mission = " (" + this.MissionName + ")";
            }

            if (!string.IsNullOrWhiteSpace(this.TaskForceName))
            {
                return "Task Force " + this.TaskForceName + mission;
            }

            if (TaskUnit != null)
            {
                return "Task Unit " + this.TaskForceNo + "." + this.TaskGroup + "." + this.TaskElement + "." +
                       this.TaskUnit + mission;
            }

            if (TaskElement != null)
            {
                return "Task Element " + this.TaskForceNo + "." + this.TaskGroup + "." + this.TaskElement + mission;
            }

            if (TaskGroup != null)
            {
                return "Task Group " + this.TaskForceNo + "." + this.TaskGroup + mission;
            }

            if (TaskForceNo != null)
            {
                return "Task Force " + this.TaskForceNo + mission;
            }

            throw new Exception("no task force name");
        }

        public string PrintTree()
        {
            throw new NotImplementedException();
        }

        public int GetRankLevel()
        {
            if (RankLevel == null)
            {
                var named = this.Relationships.FirstOrDefault(rt => rt.RelType.RelationshipTypeId == 8);
                if (named != null)
                {
                    return named.To.GetRankLevel();
                }
            }
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

        public bool IsTaskForce => true;
        public bool IsDecommissioned()
        {
            return Decommissioned;
        }
    }
}