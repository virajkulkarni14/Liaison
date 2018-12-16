﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liaison.BLL.Models.Equipment;
using Liaison.Data.Sql.Edmx;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class AirSquadron : TwoBar, IVolunteerUnit
    {
      //  public new AdminCorps AdminCorps { get; set; }

        //public bool UseOrdinal { get; set; }
        //public List<IEquipment> Equipment { get; set; }
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
            this.Decommissioned = sqlUnit.Decommissioned ?? false;
	        this.AdminCorps = new BLLAdminCorps(sqlUnit.AdminCorp);

			this.Mission = new BllMissions(sqlUnit.MissionUnits);
            this.Base = new BLLBase(sqlUnit.Bases.FirstOrDefault());
            this.Indices = sqlUnit.UnitIndexes.OrderBy(x=>x.DisplayOrder).Where(x => x.IsDisplayIndex).Select(x => x.IndexCode).ToList();
            this.SortIndex = GetSortIndex(sqlUnit.UnitIndexes);


            //if (sqlUnit.AdminCorp != null)
            //{
            //    this.AdminCorps = new AdminCorps(sqlUnit.AdminCorp.Code, sqlUnit.AdminCorp.Name, sqlUnit.AdminCorp.AdminCorpsId);
            //    // this.AdminCorpsName = sqlUnit.AdminCorp.Name;
            //    // this.AdminCorpsCode = sqlUnit.AdminCorp.Code;
            //}
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
            //if (this.AdminCorps == null)
            //{
            //    this.AdminCorps = AUnit.GetAdminCorpsHelper(this);
            //}

            StringBuilder sb = new StringBuilder();
	        if (this.Service == ServicesBll.AirForce  && this.UseOrdinal)
	        {
		        sb.Append("No. "+this.Number.ToRomanNumerals() + " ");
	        }
	        else
	        {
		        sb.Append("No. " + this.Number + " ");
	        }

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
            else if (this.Service == ServicesBll.Marines)
            {
                sb.Append("Marine Air Sqn.");
            }
            else
            { 
                sb.Append(this.AdminCorps?.AdminCorpsId == (int)Helper.Enumerators.AdminCorps.RAFTraining     ? "Unit" : "Sqn.");//35
			}

            if (!string.IsNullOrWhiteSpace(this.AdminCorps?.Code))
            {
                sb.Append(ResourceStrings.Seperator + this.AdminCorps.UnitDisplayName);
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

                sb.Append(ResourceStrings.Seperator);
            }

            var x = sb.ToString();
            return (x.Length > 0 ? x.Substring(0, x.Length - ResourceStrings.Seperator.Length) : x).Replace("_", "");
        }

        public string GetTerritorialDesignation()
        {
            return this.TerritorialDesignation;
        }

        
    }
}