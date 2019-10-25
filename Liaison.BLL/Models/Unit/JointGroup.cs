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
    public class JointGroup : AUnit, IUnit
    {
        public string UnitTypeVariant { get; set; }
        public new BLLAdminCorps AdminCorps { get; set; }
        public bool UseOrdinal { get; set; }
        public List<IEquipment> Equipment { get; set; }
        public string Letter { get; set; }

        public string GetAdminCorps()
        {
            return this.AdminCorps == null ? string.Empty : this.AdminCorps.DisplayName;
        }

        public string TerritorialDesignation { get; set; }
        private int ThreeBarTab = 10;
        private string ThreeBar = "|||";
        
        public JointGroup(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.UnitGuid = sqlUnit.UnitGuid;
            this.Number = sqlUnit.Number;
            this.MissionName = sqlUnit.MissionName;
            this.CommandName = sqlUnit.CommandName;
            this.UseOrdinal = sqlUnit.UseOrdinal;
            this.Letter = sqlUnit.Letter;
 
                this.RankLevel = ThreeBarTab;
                this.RankStar = ThreeBar;
 
            this.Decommissioned = sqlUnit.Decommissioned ?? false;

            this.Service = (ServicesBll) sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL) sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.Equipment = sqlUnit.EquipmentOwners.ToEquipmentList();
            this.TerritorialDesignation = sqlUnit.TerritorialDesignation;
            this.UnitTypeVariant=sqlUnit.UnitTypeVariant;

            this.Mission = new BllMissions(sqlUnit.MissionUnits);
            this.Base = new BLLBase(sqlUnit.Bases.FirstOrDefault());
            this.Indices = sqlUnit.UnitIndexes.OrderBy(x => x.DisplayOrder).Where(x => x.IsDisplayIndex)
                .Select(x => x.IndexCode).ToList();
            this.SortIndex = GetSortIndex(sqlUnit.UnitIndexes);
            this.UnitTypeVariant = sqlUnit.UnitTypeVariant;

            if (sqlUnit.AdminCorp != null)
            {
                this.AdminCorps = new BLLAdminCorps(sqlUnit.AdminCorp, this.UnitId);
                //this.AdminCorps = new AdminCorps(sqlUnit.AdminCorp.Code, sqlUnit.AdminCorp.Name, sqlUnit.AdminCorp.AdminCorpsId);
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
            StringBuilder sb = new StringBuilder();
            // for HHQD
            if (!string.IsNullOrWhiteSpace(this.MissionName))
            {
                //if (this.UseOrdinal==false)
                //{
                //    sb.Append(ResourceStrings.Det +" ");
                //}
                if (this.Number != null)
                {
                    sb.Append(this.Number.ToOrdinal(this.UseOrdinal) + " ");
                }

                if (this.Letter != null)
                {
                    sb.Append(this.Letter + " ");
                }

                string missionname = this.MissionName;
                if (this.MissionName == ResourceStrings.HQHQ)
                {
                    missionname = "HHQ";
                    sb.Append(missionname + " ");
                    sb.Append(ResourceStrings.Det + " ");
                    if (this.ServiceType == ServiceTypeBLL.Reserve)
                    {
                        sb.Append("(R) ");
                    }
                    else if (ServiceType == ServiceTypeBLL.Volunteer)
                    {
                        sb.Append("(V) ");
                    }
                    if (!string.IsNullOrWhiteSpace(this.TerritorialDesignation))
                    {
                        sb.Append("(" + this.TerritorialDesignation + ") ");
                    }

                }



                if (this.MissionName != ResourceStrings.HQHQ)
                {
                    if (this.ServiceType == ServiceTypeBLL.Reserve)
                    {
                        sb.Append("(R) ");
                    }
                    else if (ServiceType == ServiceTypeBLL.Volunteer)
                    {
                        sb.Append("(V) ");
                    }
                    if (!string.IsNullOrWhiteSpace(this.TerritorialDesignation))
                    {
                        sb.Append("(" + this.TerritorialDesignation + ") ");
                    }
             



                    sb.Append(missionname + " ");
                    sb.Append(ResourceStrings.Group);
                    if (!string.IsNullOrWhiteSpace(this.UnitTypeVariant))
                    {
                        sb.Append(" (" + this.UnitTypeVariant + ")");
                    }

                    sb.Append(", ");
                }

                if (!string.IsNullOrWhiteSpace(this.CommandName))
                {
                    sb.Append(this.CommandName + ", ");
                }

                if (!string.IsNullOrWhiteSpace(this.AdminCorps.UnitDisplayName))
                {
                    sb.Append(this.AdminCorps.UnitDisplayName);
                }

                var returnable = sb.ToString();
                if (returnable.EndsWith(", "))
                {
                    returnable = returnable.Substring(0, returnable.Length - 2);
                }
                

                return returnable;
            }
            //else
            //{
            //    if (this.Number != null)
            //    {
            //        sb.Append(ResourceStrings.Det + " " + this.Number);
            //    }
            //    else if (this.Letter != null)
            //    {
            //        sb.Append(ResourceStrings.Det + " " + this.Letter);
            //    }

            //    if (this.ServiceType == ServiceTypeBLL.Volunteer)
            //    {
            //        sb.Append(" (V) (" + this.TerritorialDesignation + ")");
            //    }

            //    sb.Append(", ");

            //    if (this.CommandName != null)
            //    {
            //        sb.Append(this.CommandName);
            //    }


            //    return sb.ToString();
            //}

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
            return new EquipmentContainer((x.Length > 0 ? x.Substring(0, x.Length - ResourceStrings.Seperator.Length) : x).Replace("_", ""));
        }

        public bool IsTaskForce => false;

        public bool IsDecommissioned()
        {
            return Decommissioned;
        }
    }
}