using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liaison.BLL.Models.Equipment;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class DetachmentBll : AUnit, IUnit
    {
        public new AdminCorps AdminCorps { get; set; }
        public bool UseOrdinal { get; set; }
        public List<IEquipment> Equipment { get; set; }
        public string GetAdminCorps()
        {
            return this.AdminCorps == null ? string.Empty : this.AdminCorps.Name;
        }
        public DetachmentBll(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.UnitGuid = sqlUnit.UnitGuid;
            this.Number = sqlUnit.Number;
            this.MissionName = sqlUnit.MissionName;
            this.CommandName = sqlUnit.CommandName;
            this.UseOrdinal = sqlUnit.UseOrdinal;
            if (this.CommandName.StartsWith("Det") && (this.CommandName.EndsWith("NAS")))
            {
                this.RankLevel = 10;
                this.RankStar = "|";
            }
            else
            {
                this.RankLevel = sqlUnit.Rank.RankLevel;
                this.RankStar = sqlUnit.Rank.Rank1;
            }

            this.Service = (ServicesBll)sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL)sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.Equipment = sqlUnit.EquipmentOwners.ToEquipmentList();

            this.Mission = new BllMissions(sqlUnit.MissionUnits);
            this.Base = new BLLBase(sqlUnit.Bases.FirstOrDefault());
            this.Indices = sqlUnit.UnitIndexes.OrderBy(x => x.DisplayOrder).Where(x => x.IsDisplayIndex).Select(x => x.IndexCode).ToList();
            this.SortIndex = GetSortIndex(sqlUnit.UnitIndexes);


            if (sqlUnit.AdminCorp != null)
            {
                this.AdminCorps = new AdminCorps(sqlUnit.AdminCorp.Code, sqlUnit.AdminCorp.Name, sqlUnit.AdminCorp.AdminCorpsId);
                // this.AdminCorpsName = sqlUnit.AdminCorp.Name;
                // this.AdminCorpsCode = sqlUnit.AdminCorp.Code;
            }
            var relMain = sqlUnit.RelationshipsFrom.ToList();
            var relt = sqlUnit.RelationshipsTo.ToList();

            relMain.AddRange(relt);
            this.Relationships = new BLLRelationships(sqlUnit.UnitId, relt);

        }

        public string CommandName { get; set; }

        public string GetName()
        {
            // for naval air detachments
            if (this.CommandName.Contains("__"))
            {
                return this.CommandName.Replace("_", "");
            }

            return this.CommandName;
        }

        public string PrintTree()
        {
            throw new NotImplementedException();
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
            return (x.Length > 0 ? x.Substring(0, x.Length - ExtensionMethods.Seperator.Length) : x).Replace("_", "");
        }

        public bool IsTaskForce => false;
    }
}