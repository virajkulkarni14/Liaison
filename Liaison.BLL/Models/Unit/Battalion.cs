using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Liaison.BLL.Models.Equipment;
using Liaison.BLL.Models.Objects;
using Liaison.BLL.Models.Unit.Abstracts;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class Battalion : TwoBar
    {
        public string UnitTypeVariant { get; set; }
        public string TerritorialDesignation { get; set; }
        public string UniqueName { get; set; }
		public string CommandName { get; set; }
        public Battalion(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.Number = sqlUnit.Number;
            this.UseOrdinal = sqlUnit.UseOrdinal;
            this.NickName = sqlUnit.NickName;
            this.UniqueName = sqlUnit.UniqueName;
            this.MissionName = sqlUnit.MissionName;
            this.RankLevel = sqlUnit.Rank.RankLevel;
	        this.CommandName = sqlUnit.CommandName;
            this.RankStar = sqlUnit.Rank.Rank1;
            this.Service = (ServicesBll)sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL)sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.Equipment = sqlUnit.EquipmentOwners.ToEquipmentList();
            this.AdminCorps = new BLLAdminCorps(sqlUnit.AdminCorp);
            this.Decommissioned = sqlUnit.Decommissioned ?? false;
            this.TerritorialDesignation = sqlUnit.TerritorialDesignation;
            this.UnitTypeVariant = sqlUnit.UnitTypeVariant;
            this.Language = sqlUnit.Language;

            this.Mission = new BllMissions(sqlUnit.MissionUnits);
            this.Base = new BLLBase(sqlUnit.Bases.FirstOrDefault());
            this.Indices = sqlUnit.UnitIndexes.OrderBy(x => x.DisplayOrder).Where(x => x.IsDisplayIndex).Select(x => x.IndexCode).ToList();
            this.SortIndex = GetSortIndex(sqlUnit.UnitIndexes);

            var relMain = sqlUnit.RelationshipsFrom.ToList();
            var relt = sqlUnit.RelationshipsTo.ToList();

            relMain.AddRange(relt);
            this.Relationships = new BLLRelationships(sqlUnit.UnitId, relt);
        }

        
        public override string GetAdminCorps()
        {
            return this.AdminCorps.DisplayName;
        }

        public override string GetName()
        {
            if (this.Language != null)
            {
                return this.GetNameNotEnglish();
            }
            StringBuilder sb = new StringBuilder();

            if (this.Number != null)
            {
                sb.Append(this.Number.ToOrdinal(this.UseOrdinal) + " ");

                if (this.ServiceType == ServiceTypeBLL.Reserve)
                {
                    sb.Append("(R) ");
                }
                else if (this.ServiceType == ServiceTypeBLL.Volunteer)
                {
                    sb.Append("(V) (" + this.TerritorialDesignation + ") ");
                }
            }

            List<string> missions = Liaison.Data.Sql.GetStuff.GetBattalionNoBracketMissionNames();
            if (string.IsNullOrWhiteSpace(this.UniqueName))
            {
                if (!string.IsNullOrWhiteSpace(this.MissionName))
                {
                    if (this.Number == null)
                    {
                        sb.Append(this.MissionName + " ");
                    }
                    else
                    {
                        if (missions.Contains(this.MissionName)
                            || this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineLogistics
                            || this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarinesReserveLogistics
                            || this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalNavalMedicalService
                            || this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineIntelligence
                            || this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineCommunications
                            || this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarinesMilitaryPolice
                            || this.AdminCorps.ParentAdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineLightInfantry
                            || this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineLightInfantry
                            || this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineCavalry
                            || this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarinesEngineers
                            || this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineCavalryReserve
                            || this.AdminCorps.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarinesEngineersReserve)
                        {
                            sb.Append(this.MissionName);
                            //if (this.MissionName != "Commando")
                            //{
                            sb.Append(" ");
                            //}
                        }
                        else
                        {
                            sb.Append("(" + this.MissionName + ") ");
                        }
                    }
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(this.UniqueName))
                {
                    sb.Append("(" + this.UniqueName + ") ");
                }

                sb.Append(this.MissionName + " ");
            }


	        if (this.AdminCorps?.AdminCorpsId != (int) Helper.Enumerators.AdminCorps.RoyalMarineCommando)
	        {
		        sb.Append("Bn.");
	        }

	        if (!string.IsNullOrWhiteSpace(this.UnitTypeVariant))
            {
                sb.Append(" (" + this.UnitTypeVariant + ")");
            }

            if (this.Number == null)
            {
                if (this.ServiceType == ServiceTypeBLL.Reserve)
                {
                    sb.Append(" (R)");
                }
                else if (this.ServiceType == ServiceTypeBLL.Volunteer)
                {
                    sb.Append(" (V) (" + this.TerritorialDesignation + ")");
                }
            }


            if (!string.IsNullOrWhiteSpace(this.CommandName))
	        {
		        sb.Append(ResourceStrings.Seperator + this.CommandName);
	        }

            sb.Append(ResourceStrings.Seperator +this.AdminCorps?.UnitDisplayName);
	        return sb.ToString().Replace("_", "");
        }

        private string GetNameNotEnglish()
        {
            Type type = Type.GetType("Liaison.BLL.Languages." + this.Language.Replace('-', '_'));

            var instance = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("GetBattalionName");
            if (method != null)
            {
                string a = method.Invoke(instance, new object[] {this}).ToString();

                return a;
            }

            return "";
        }

        public override EquipmentContainer GetEquipment()
        {
            return EquipmentMethods.GetEquipment(this.Equipment);
        }
    }
}