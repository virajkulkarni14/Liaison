﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liaison.BLL.Models.Equipment;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class AirSquadron : TwoBar, IVolunteerUnit
    {
        public AdminCorps AdminCorps { get; set; }

        public bool UseOrdinal { get; set; }
        public List<IEquipment> Equipment { get; set; }
        public string TerritorialDesignation { get; set; }

        public AirSquadron(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.UnitGuid = sqlUnit.UnitGuid;
            this.Number = sqlUnit.Number;           
            this.TerritorialDesignation = sqlUnit.TerritorialDesignation;
            this.MissionName = sqlUnit.MissionName;
            this.UseOrdinal = sqlUnit.UseOrdinal;
            this.RankLevel = sqlUnit.Rank.RankLevel;
            this.RankStar = sqlUnit.Rank.Rank1;
            this.Service = (ServicesBll)sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL)sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.Equipment = sqlUnit.EquipmentOwners.ToEquipmentList();

            this.Mission = new BLLMissions(sqlUnit.MissionUnits);
            this.Base = new BLLBase(sqlUnit.Bases.FirstOrDefault());
            this.Indices = sqlUnit.UnitIndexes.OrderBy(x=>x.DisplayOrder).Where(x => x.IsDisplayIndex).Select(x => x.IndexCode).ToList();
            this.SortIndex = GetSortIndex(sqlUnit.UnitIndexes);


            if (sqlUnit.AdminCorp != null)
            {
                this.AdminCorps = new AdminCorps(sqlUnit.AdminCorp.Code, sqlUnit.AdminCorp.Name);
                // this.AdminCorpsName = sqlUnit.AdminCorp.Name;
                // this.AdminCorpsCode = sqlUnit.AdminCorp.Code;
            }
            var relMain = sqlUnit.RelationshipsFrom.ToList();
            var relt = sqlUnit.RelationshipsTo.ToList();

            relMain.AddRange(relt);
            this.Relationships = new BLLRelationships(sqlUnit.UnitId, relt);

        }
        public override string GetName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("No. " + this.Number + " ");
            if (this.ServiceType == ServiceTypeBLL.Volunteer)
            {
                sb.Append("(V) ("+this.TerritorialDesignation+") " );
            }
            if (!string.IsNullOrWhiteSpace(this.MissionName))
            {
                sb.Append(this.MissionName + " ");
            }
            if (this.Service == ServicesBll.Navy)
            {
                sb.Append("Naval Air Sqn.");
            }
            else
            {
                sb.Append("Sqn.");
            }
            if (!string.IsNullOrWhiteSpace(this.AdminCorps.Code))
            {
                sb.Append(ExtensionMethods.Seperator + this.AdminCorps.Code);
            }

            return sb.ToString();
        }



        public override string GetEquipment()
        {
            bool showAltName = true;

            StringBuilder sb =new StringBuilder();
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

        public string GetTerritorialDesignation()
        {
            return this.TerritorialDesignation;
        }

        
    }
}