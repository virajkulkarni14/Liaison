using Liaison.Data.Sql.Edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Liaison.BLL.Models;
using Liaison.BLL.Models.Unit;
using Liaison.BLL.Models.Unit.Interfaces;
using Unit = Liaison.Data.Sql.Edmx.Unit;

namespace Liaison.BLL.Translators
{
    public class LiaisonSql
    {
        public static List<string> GetSortOrder()
        {
            List<string> order;
            using (var le = new LiaisonEntities())
            {
                order = Data.Sql.GetStuff.GetSortOrder(le);
            }

            return order;
        }


        public static IUnit GetTree(string input, int? depth, bool showAll)
        {
            _depthRequired = depth == 0 ? null : depth;
            _showAll = showAll;
            
            IUnit convUnit;
            using (var context = new LiaisonEntities())
            {
                HttpContext.Current.Session["BattalionCorps"] = Data.Sql.GetStuff.GetConfigSetting("BattalionCorps");
                HttpContext.Current.Session["RegimentCorps"] = Data.Sql.GetStuff.GetConfigSetting("RegimentCorps");
                HttpContext.Current.Session["ArmySquadronCorps"] =
                    Data.Sql.GetStuff.GetConfigSetting("ArmySquadronCorps");
                HttpContext.Current.Session["CompanyCorps"] = Data.Sql.GetStuff.GetConfigSetting("CompanyCorps");
                HttpContext.Current.Session["JointGroupMissions"] =
                    Data.Sql.GetStuff.GetJointGroupMissionNames();
                HttpContext.Current.Session["Detachment|||UnitIds"] =
                    Data.Sql.GetStuff.GetConfigSetting("Detachment|||UnitIds");
                HttpContext.Current.Session["Detachment|UnitIds"] =
                    Data.Sql.GetStuff.GetConfigSetting("Detachment|UnitIds");
                HttpContext.Current.Session["BrigadeCommandsUnitIds"] =
                    Data.Sql.GetStuff.GetConfigSetting("BrigadeCommandsUnitIds");
                HttpContext.Current.Session["BatteryCorps"] = Data.Sql.GetStuff.GetConfigSetting("BatteryCorps");

                HttpContext.Current.Session["TroopCorps"] = Data.Sql.GetStuff.GetConfigSetting("TroopCorps");
                HttpContext.Current.Session["PlatoonCorps"] = Data.Sql.GetStuff.GetConfigSetting("PlatoonCorps");

                if (Int32.TryParse(input, out int iInput))
                {

                    Unit sqlUnit = Liaison.Data.Sql.GetStuff.GetItWithContext(context, iInput);

                    convUnit = ConvertUnit(sqlUnit);

                }
                else
                {
                    Unit sqlUnit = Liaison.Data.Sql.GetStuff.GetItWithContext(context, input);

                    convUnit = ConvertUnit(sqlUnit);
                }
            }

            return convUnit;
        }

        private static  int? _depthRequired = null;
        private static bool _showAll = false;

        public static IUnit ConvertUnit(Unit sqlUnit) //, bool includeParent)
        {
            List<int> battalionCorps = HttpContext.Current.Session["BattalionCorps"] as List<int>;
            List<int> regimentCorps = HttpContext.Current.Session["RegimentCorps"] as List<int>;
            List<int> armysquadronCorps = HttpContext.Current.Session["ArmySquadronCorps"] as List<int>;
            List<int> companyCorps =            HttpContext.Current.Session["CompanyCorps"] as List<int>;
            List<int> brigadeCmds = HttpContext.Current.Session["BrigadeCommandsUnitIds"] as List<int>;
            List<int> batteryCorps = HttpContext.Current.Session["BatteryCorps"] as List<int>;
            List<int> troopCorps = HttpContext.Current.Session["TroopCorps"] as List<int>;
            List<int> platoonCorps = HttpContext.Current.Session["PlatoonCorps"] as List<int>;

            var thisUnitRankLevel = sqlUnit.Rank.RankLevel;
            if (_depthRequired == null)
            {
                _depthRequired =100;
            }

            if (thisUnitRankLevel > _depthRequired)
            {
                return new DefaultUnit();
            }

            if (!_showAll && sqlUnit.CanHide)
            {
                return new DefaultUnit();
            }

            if (sqlUnit.RankSymbol == "?")
            {
                var detachment = new DetachmentBll(sqlUnit);
                return detachment.GetRankLevel() > _depthRequired ? (IUnit) new DefaultUnit() : detachment;
            }

            var cont = thisUnitRankLevel <= _depthRequired;

            if (sqlUnit.MissionName != null && sqlUnit.MissionName.Contains("Directorate"))
            {
                return new Directorate(sqlUnit, cont);
            }

            if (sqlUnit.RankSymbol == " ")
            {
                // Commissioned
                return new Command(sqlUnit, cont);
            }
            if (sqlUnit.RankSymbol == "'")
            {
                // Ministry
                return new Command(sqlUnit, cont);
            }
            if (sqlUnit.RankSymbol == "-")
            {
                // Department
                return new Command(sqlUnit, cont);
            }
            if (sqlUnit.RankSymbol == "!")
            {
                return new Command(sqlUnit, cont); //, includeParent);
            }

            if (sqlUnit.RankSymbol == "\"")
            {
                return new Command(sqlUnit, cont); //, includeParent);
            }

	        if (sqlUnit.RankSymbol == "#")
	        {
		        //try
		        //{
			        return new Command(sqlUnit, cont); //, includeParent);
		        //}
		        //catch (Exception x)
		        //{
			       // string a = "b";
		        //}
	        }


	        if (sqlUnit.RankSymbol == "$")
            {
                return new Command(sqlUnit, cont); //, includeParent);
            }

            if (sqlUnit.RankSymbol == "%")
            {
                if (sqlUnit.Number != null)
                {
                    return new ArmyGroup(sqlUnit); //, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Joint ||
                    sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Army ||
                    sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce ||
                    sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
                {
                    return new Command(sqlUnit, cont); //, includeParent);
                }
            }
            else if (sqlUnit.RankSymbol == "&")
            {
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Joint)
                {
                    return new Command(sqlUnit, cont); //, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Army)
                {
                    return new FieldArmy(sqlUnit); //, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce)
                {
                    return new TacticalAirForce(sqlUnit); //, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
                {
                    if (sqlUnit.Number == null)
                    {
                        return new Command(sqlUnit, cont); //, includeParent);
                    }
                    else
                    {
                        return new NamedFleet(sqlUnit); //, includeParent);
                    }
                }
            }
            else if (sqlUnit.RankSymbol == "(")
            {
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Joint)
                {
                    return new Command(sqlUnit, cont); //, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Army)
                {
                    if (sqlUnit.Number == null)
                    {
                        return new Command(sqlUnit, cont); //, includeParent);
                    }

                    return new Corps(sqlUnit); //, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce)
                {
                    if (sqlUnit.Number == null)
                    {
                        return new Command(sqlUnit, cont); //, includeParent);
                    }

                    return new NumberedAirForce(sqlUnit);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
                {
                    if (sqlUnit.Number == null)
                    {
                        return new Command(sqlUnit, cont); //, includeParent);
                    }

                    return new NumberedFleet(sqlUnit); //, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Marines)
                {
                    if (sqlUnit.Number == null)
                    {
                        return new Command(sqlUnit, cont); //, includeParent);
                    }
                    return new ExpeditionaryForce(sqlUnit);                    
                }
            }
            else if (sqlUnit.RankSymbol == ")")
            {
	            if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Army)
	            {
		            if (sqlUnit.Number == null)
		            {
			            return new Command(sqlUnit, cont);
		            }

		            return new Division(sqlUnit); //, includeParent);
	            }

	            if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce)
	            {
	                if (sqlUnit.Number == null)
		            {
			            return new Command(sqlUnit, cont);
		            }

	                return new AirGroup(sqlUnit);
	            }

	            if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
	            {
		            if (sqlUnit.Number == null)
		            {
			            return new Command(sqlUnit, cont); //, includeParent);
		            }
		            else
		            {
			            return new NavalGroup(sqlUnit);
		            }
	            }

	            if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Marines)
	            {
	                if (sqlUnit.AdminCorp == null)
	                {
	                    return new Command(sqlUnit, cont);
	                }
		            if (sqlUnit.AdminCorp.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineAviation)
		            {
			            return new AirGroup(sqlUnit);
		            }

		            if (sqlUnit.AdminCorp.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineLandForces)
		            {
			            return new Division(sqlUnit);
		            }

		            if (sqlUnit.AdminCorp.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineLogistics)
		            {
			            return new Division(sqlUnit);
		            }
	            }

	            if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Joint)
	            {
		            return new Command(sqlUnit, cont);
	            }
            }
            else if (sqlUnit.RankSymbol == "*")
            {
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Joint)
                {
                    return brigadeCmds.Contains(sqlUnit.UnitId)
                        ? (IUnit) new Brigade(sqlUnit)
                        : new Command(sqlUnit, cont);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce)
                {
                    if (!string.IsNullOrWhiteSpace(sqlUnit?.CommandName))
                    {
                        if (sqlUnit.CommandName.Contains("College") || sqlUnit.CommandName.Contains("Centre"))
                        {
                            return new AirForceEstablishment(sqlUnit);
                        }
                    }

                    return new AirForceBase(sqlUnit); //, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Army)
                {
                    if (!string.IsNullOrWhiteSpace(sqlUnit.CommandName))
                    {
                        return new Command(sqlUnit, cont);
                    }
                    return new Brigade(sqlUnit); //, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
                {
                    if (sqlUnit.MissionName!=null && sqlUnit.MissionName.Contains("Strike"))
                    {
                        return new NavalGroup(sqlUnit);
                    }
                    return new Flotilla(sqlUnit);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Marines)
                {
                    if (sqlUnit.AdminCorp?.AdminCorpsId == (int)Helper.Enumerators.AdminCorps.MAGTF)
                    {
                        return new Brigade(sqlUnit);
                    }
                    if (sqlUnit.AdminCorp?.AdminCorpsId == (int)Helper.Enumerators.AdminCorps.MarineHeadquartersGroups)
                    {
                        return new Command(sqlUnit, cont);
                    }
                }
            }
            else if (sqlUnit.RankSymbol == "/")
            {
                List<string> jointGroupMissions = HttpContext.Current.Session["JointGroupMissions"] as List<string>;
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Joint)
                {
                    return new JointGroup(sqlUnit);
                }
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Army)
                {
                    if (jointGroupMissions!=null && jointGroupMissions.Contains(sqlUnit.MissionName))
                    {
                        return new JointGroup(sqlUnit);
                    }

                    if (sqlUnit.MissionName == "Army Garrison")
                    {
                        return new Facility(sqlUnit);
                    }
                    if (!string.IsNullOrWhiteSpace(sqlUnit.CommandName))
                    {
                        return new Command(sqlUnit, cont);
                    }
                    return new Regiment(sqlUnit);
                }
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
                {
                    var f = sqlUnit.CommandName;
                    if (sqlUnit.Ships != null && sqlUnit.Ships.Any())
                    {
                        if (sqlUnit.Ships.Any(b => b.IsBase))
                        {
                            return new Facility(sqlUnit);
                        }
                        return new Vessel(sqlUnit);
                    }

                    if (sqlUnit.AdminCorp?.Code == "FAA" || sqlUnit.AdminCorp?.Code == "NAv")
                    {
                        return new AirWing(sqlUnit); //, includeParent);
                    }

                    List<string> missionNames = Liaison.Data.Sql.GetStuff.GetNavalSquadronMissionNames();
                    if (missionNames.Contains(sqlUnit.MissionName))
                    //if (sqlUnit.MissionName == "Destroyer" || sqlUnit.MissionName == "Submarine")
                    {
                        return new NavalSquadron(sqlUnit);
                    }
                    if (jointGroupMissions != null && jointGroupMissions.Contains(sqlUnit.MissionName))
                    {
                        return new JointGroup(sqlUnit);
                    }


                    return new Facility(sqlUnit);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce)
                {
                    return new AirWing(sqlUnit);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Marines)
                {

                    if (sqlUnit.AdminCorpsId==(int)Helper.Enumerators.AdminCorps.RoyalMarineAviation ||
                        sqlUnit.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarinesAirArm ||
                        sqlUnit.AdminCorp.ParentAdminCorpsId == (int)Helper.Enumerators.AdminCorps.RoyalMarinesAirArm ||
                        sqlUnit.AdminCorpsId == (int)Helper.Enumerators.AdminCorps.RoyalIndianMarinesAirArm)
                    {
                        return new AirWing(sqlUnit);
                    }
                    if (sqlUnit.AdminCorpsId == (int)Helper.Enumerators.AdminCorps.RoyalMarineLogistics ||
                        sqlUnit.AdminCorp.ParentAdminCorpsId == (int)Helper.Enumerators.AdminCorps.RoyalMarineLogistics ||
                        sqlUnit.AdminCorp.ParentAdminCorpsId == (int)Helper.Enumerators.AdminCorps.RoyalMarinesReserveLogistics||
                        sqlUnit.AdminCorp.ParentAdminCorpsId == (int)Helper.Enumerators.AdminCorps.RoyalMarineArtillery ||
                        sqlUnit.AdminCorp.ParentAdminCorpsId == (int)Helper.Enumerators.AdminCorps.RoyalMarineLightInfantry
                    )
                    {
                        return new Regiment(sqlUnit);
                    }

                    if (sqlUnit.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.MarineHeadquartersGroups ||
                        sqlUnit.AdminCorpsId == (int)Helper.Enumerators.AdminCorps.CorpsOfRoyalMarines ||
                        sqlUnit.AdminCorpsId==(int)Helper.Enumerators.AdminCorps.RoyalMarinesReserve)
                    {
                        return new Command(sqlUnit, false);
                    }

                    if (sqlUnit.AdminCorp.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.MAGTF)
                    {
                        return new ExpeditionaryUnit(sqlUnit);
                    }
                    if (sqlUnit.AdminCorp.AdminCorpsId == (int)Helper.Enumerators.AdminCorps.RMRCivilAffairs)
                    {
                        return new JointGroup(sqlUnit);
                    }

                    throw new Exception("Marine Corps not processed");
                }
            }
            else if (sqlUnit.RankSymbol == "@")
            {
                if (sqlUnit.MissionName !=null && sqlUnit.MissionName.Contains("Office"))
                {
                    return new Directorate(sqlUnit, cont);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Joint)
                {
                    if (sqlUnit.MissionName != null)
                    {
						if (sqlUnit.UnitTypeVariant=="Sqn")
						{
							sqlUnit.UnitTypeVariant = null;
							return new AirSquadron(sqlUnit);
						}
                        return new JointUnit(sqlUnit);
                    }
                    if (sqlUnit.CommandName.StartsWith("Joint") && sqlUnit.CommandName.Contains("Component"))
                    {
                        return new JointUnit(sqlUnit);
                    }
                    return new AirSquadron(sqlUnit);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Marines)
                {
                    if (sqlUnit.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarinesAirArmAirDefence)
                    {
                        return new Battalion(sqlUnit);
                    }
                    if (sqlUnit.AdminCorpsId == (int)Helper.Enumerators.AdminCorps.RoyalMarineAviation||
                        sqlUnit.AdminCorp.ParentAdminCorpsId == (int)Helper.Enumerators.AdminCorps.RoyalMarinesAirArm ||
                        sqlUnit.AdminCorp.ParentAdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineAviation)
                    {
                        return new AirSquadron(sqlUnit);
                    }

                    if (battalionCorps != null && battalionCorps.Contains(sqlUnit.AdminCorpsId.Value))
                    //if (sqlUnit.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineCommando
                    //  || sqlUnit.AdminCorpsId== (int)Helper.Enumerators.AdminCorps.RRM10)
                    {
		                return new Battalion(sqlUnit);
	                }

                    throw new Exception("Other type of marine @");
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce)
                {
                    return new AirSquadron(sqlUnit);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
                {                    
                    if (sqlUnit.AdminCorp?.ParentAdminCorpsId == (int) Helper.Enumerators.AdminCorps.NavalAviation
                        ||sqlUnit.AdminCorp?.ParentAdminCorpsId==(int)Helper.Enumerators.AdminCorps.FleetArmArm)
                    {
                        return new AirSquadron(sqlUnit);
                    }

                    if (sqlUnit.AdminCorp?.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalNavalMedicalService)
                    {
                        return new Battalion(sqlUnit);
                    }
                    List<string> missionNames = Liaison.Data.Sql.GetStuff.GetNavalSquadronMissionNames();
                    if (missionNames.Contains(sqlUnit.MissionName))                        
                    {
                        if (sqlUnit.AdminCorpsId == (int)Helper.Enumerators.AdminCorps.NavalSpecialWarfare)
                        {
                            return new NavalSquadron(sqlUnit);
                        }
                        return new NavalSquadronDivision(sqlUnit);
                    }

                    List<string> facilityMissionNames = Data.Sql.GetStuff.GetFacilityMissionNames();
                    //if (sqlUnit.Ships.Any(b => b.IsBase))
                    if (facilityMissionNames.Contains(sqlUnit.MissionName))
                    {
                        return new Facility(sqlUnit);
                    }

                    if (sqlUnit.Ships.Any())
                    {
                        return new Vessel(sqlUnit);
                    }
                    return new Facility(sqlUnit);

                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Army)
                {
                    if (sqlUnit.MissionName == "Army Garrison")
                    {
                        return new Facility(sqlUnit);
                    }
                    if (sqlUnit.AdminCorpsId.HasValue)
                    {
                        if (battalionCorps != null && battalionCorps.Contains(sqlUnit.AdminCorpsId.Value))
                        {
                            return new Battalion(sqlUnit);
                        }

                        if (regimentCorps != null && regimentCorps.Contains(sqlUnit.AdminCorpsId.Value))
                        {
                            return new Regiment(sqlUnit);
                        }
                    }
                }
            }
            else if (sqlUnit.RankSymbol == "|")
            {
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Joint)
                {
                    return new JointUnit(sqlUnit);
                }
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce)
                {
                    return new Flight(sqlUnit);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
                {
                    if (sqlUnit.AdminCorp?.AdminCorpsId == (int)Helper.Enumerators.AdminCorps.RoyalNavalMedicalService)
                    {
                        return new Company(sqlUnit);
                    }


                    if (sqlUnit.Ships != null && sqlUnit.Ships.Any())
                    {
                        return new Vessel(sqlUnit);
                    }

                    return new Facility(sqlUnit);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Army)
                {
                    if (armysquadronCorps.Contains(sqlUnit.AdminCorpsId.Value))
                    {
                        if (sqlUnit.AdminCorpsId.Value == (int) Helper.Enumerators.AdminCorps.ArmyAirCorps)
                        {
                            if (sqlUnit.Number == null)
                            { 
                                return new ArmySquadron(sqlUnit);
                            }
                            return new AirSquadron(sqlUnit);
                        }

                        return new ArmySquadron(sqlUnit);
                    }

                    if (companyCorps.Contains(sqlUnit.AdminCorpsId.Value))
                    {
                        return new Company(sqlUnit);
                    }

                    if (batteryCorps.Contains(sqlUnit.AdminCorpsId.Value))
                    {
                        return new Battery(sqlUnit);
                    }
                }

	            if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Marines)
	            {
	                if (batteryCorps.Contains(sqlUnit.AdminCorpsId.Value))
	                {
	                    return new Battery(sqlUnit);
	                }
                    //if (companyCorps.Contains(sqlUnit.AdminCorpsId.Value) ||
	                // (sqlUnit.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineCommando))
	                //{
	                    return new Company(sqlUnit);
	                //}

	            }
            }
            else if (sqlUnit.RankSymbol == "¦")
            {
                if (platoonCorps.Contains(sqlUnit.AdminCorpsId.Value))
                {
                    if (sqlUnit.MissionName != null && sqlUnit.MissionName.EndsWith("Team"))
                    {
                        return new Team(sqlUnit);
                    }

                    return new Platoon(sqlUnit);
                }

                if (troopCorps.Contains(sqlUnit.AdminCorpsId.Value))
                {
                    return new Troop(sqlUnit);
                }

                if (sqlUnit.AdminCorpsId.Value == (int) Helper.Enumerators.AdminCorps.ArmyAirCorps)
                {
                    return new Flight(sqlUnit);
                }
            }
            else if (sqlUnit.RankSymbol == "^")
            {
                return new TaskForceBll(sqlUnit); //, includeParent);
            }
            else if (sqlUnit.RankSymbol == "?")
            {
                return new DetachmentBll(sqlUnit); //, includeParent);
            }

            throw new Exception("not found; Unit Id: "+sqlUnit.UnitId + "; Rank Symbol " + sqlUnit.RankSymbol +
                                "; Service: " +
                                (Helper.Enumerators.ServicesBll) sqlUnit.ServiceIdx + "(" + sqlUnit.ServiceIdx + ")");
        }

        


        public static NewThing CreateAFB(NewThing newThing)
        {
            if (newThing.Number == 0)
            {
                throw new Exception("Put a number");
            }
            using (var le = new LiaisonEntities())
            {
                var unitindices = Liaison.Data.Sql.GetStuff.GetDictionary("AirForceUnitIndices");
                foreach (var x in newThing.Things.Where(c=>c.Create))
                {
                    var wingAdmin = le.AdminCorps.First(a => a.Lookup == x.Code) ?? throw new ArgumentNullException("le.AdminCorps.Where(a => a.Code == x.Code).First()");

                    var unit = new Unit();
                    unit.Number = newThing.Number;
                    unit.UseOrdinal = false;
                    unit.MissionName = x.Name;
                    unit.ServiceIdx = (int) Liaison.Helper.Enumerators.ServicesBll.AirForce;
                    unit.ServiceTypeIdx = (int) Helper.Enumerators.ServiceTypeBLL.Active;
                    unit.RankSymbol = "/" ;
                    unit.UnitGuid = Guid.NewGuid();
                    unit.CanHide = newThing.Mission != "Operations";
                    unit.AdminCorpsId = wingAdmin.AdminCorpsId;
                    le.Units.Add(unit);
                    le.SaveChanges();

                    var unitIndex = new UnitIndex();

                    string under = Helper.Helper.GetIntWithUnderscores(newThing.Number, false);
                    string ic;
                    if (newThing.Mission == "Operations")
                    {
                        ic = "AIR/" + under;
                    }
                    else
                    {
                        ic = "AIR/" + x.Code + under;
                    }

                    unitIndex.IndexCode = ic;
                    unitIndex.UnitId = unit.UnitId;
                    unitIndex.IsSortIndex = true;
                    unitIndex.IsDisplayIndex = true;
                    unitIndex.IsAlt = false;
                    unitIndex.DisplayOrder = 10;
                    le.UnitIndexes.Add(unitIndex);
                    le.SaveChanges();

                    unitIndex=new UnitIndex();
                    unitIndex.IndexCode = under + " " + x.Code + "W/RAF";
                    unitIndex.UnitId = unit.UnitId;
                    unitIndex.IsSortIndex = false;
                    unitIndex.IsDisplayIndex = true;
                    unitIndex.IsAlt = false;
                    unitIndex.DisplayOrder = 20;
                    le.UnitIndexes.Add(unitIndex);
                    le.SaveChanges();

                    unitIndex = new UnitIndex();
                    unitIndex.IndexCode = under+ " "+GetLongCode(unitindices, x.Code + "W") + ", RAF";
                    unitIndex.UnitId = unit.UnitId;
                    unitIndex.IsSortIndex = false;
                    unitIndex.IsDisplayIndex = true;
                    unitIndex.IsAlt = false;
                    unitIndex.DisplayOrder = 30;
                    le.UnitIndexes.Add(unitIndex);
                    le.SaveChanges();

                    unitIndex = new UnitIndex();
                    unitIndex.IndexCode = "~USAF " + x.Code + "G " + under;
                    unitIndex.UnitId = unit.UnitId;
                    unitIndex.IsSortIndex = false;
                    unitIndex.IsDisplayIndex = true;
                    unitIndex.IsAlt = false;
                    unitIndex.DisplayOrder = 50;
                    le.UnitIndexes.Add(unitIndex);
                    le.SaveChanges();

                    var tennant = new Tennant();
                    tennant.BaseId = newThing.BaseId;
                    tennant.UnitId = unit.UnitId;
                    tennant.IsHost = false;
                    le.Tennants.Add(tennant);
                    le.SaveChanges();

                    foreach (var y in x.NewSqdrns.Where(c => c.Create))
                    {
                        var sqnAdmin =  le.AdminCorps.First(a => a.Lookup == y.Code) ?? throw new ArgumentNullException("le.AdminCorps.Where(a => a.Code == x.Code).First()");

                        var sqn = new Unit();
                        sqn.Number = newThing.Number;
                        sqn.UseOrdinal = false;
                        sqn.MissionName = y.Name;
                        sqn.ServiceIdx = (int)Liaison.Helper.Enumerators.ServicesBll.AirForce;
                        sqn.ServiceTypeIdx = (int)Helper.Enumerators.ServiceTypeBLL.Active;
                        sqn.RankSymbol = "@";
                        sqn.UnitGuid = Guid.NewGuid();
                        sqn.CanHide = newThing.Mission != "Operations";
                        sqn.AdminCorpsId = sqnAdmin.AdminCorpsId;
                        le.Units.Add(sqn);
                        le.SaveChanges();

                        var uiSqn = new UnitIndex();                        
                        uiSqn.IndexCode = "AIR@" + y.Code + under;
                        uiSqn.UnitId = sqn.UnitId;
                        uiSqn.IsSortIndex = true;
                        uiSqn.IsDisplayIndex = true;
                        uiSqn.IsAlt = false;
                        uiSqn.DisplayOrder = 10;
                        le.UnitIndexes.Add(uiSqn);
                        le.SaveChanges();

                        var uiSqn2 = new UnitIndex();
                        uiSqn2.IndexCode = under + " " + y.Code + "S/RAF";
                        uiSqn2.UnitId = sqn.UnitId;
                        uiSqn2.IsSortIndex = false;
                        uiSqn2.IsDisplayIndex = true;
                        uiSqn2.IsAlt = false;
                        uiSqn2.DisplayOrder = 20;
                        le.UnitIndexes.Add(uiSqn2);
                        le.SaveChanges();

                        var uiSqn2A = new UnitIndex();
                        uiSqn2A.IndexCode = under + " " + GetLongCode(unitindices, y.Code + "S") + ", RAF";
                        uiSqn2A.UnitId = sqn.UnitId;
                        uiSqn2A.IsSortIndex = false;
                        uiSqn2A.IsDisplayIndex = true;
                        uiSqn2A.IsAlt = false;
                        uiSqn2A.DisplayOrder = 30;
                        le.UnitIndexes.Add(uiSqn2A);
                        le.SaveChanges();

                        var uiSqn3 = new UnitIndex();
                        uiSqn3.IndexCode = "~USAF " + y.Code + "S " + under;
                        uiSqn3.UnitId = sqn.UnitId;
                        uiSqn3.IsSortIndex = false;
                        uiSqn3.IsDisplayIndex = true;
                        uiSqn3.IsAlt = false;
                        uiSqn3.DisplayOrder = 50;
                        le.UnitIndexes.Add(uiSqn3);
                        le.SaveChanges();

                        var rel = new Relationship();
                        rel.RelTypeIdx = (int) Helper.Enumerators.RelationshipTypeBll.Organic;
                        rel.RelationshipGuid = Guid.NewGuid();
                        rel.RelationshipsFrom = unit;
                        rel.RelationshipsTo = sqn;
                        le.Relationships.Add(rel);
                        le.SaveChanges();

                        var tennantSqn = new Tennant();
                        tennantSqn.BaseId = newThing.BaseId;
                        tennantSqn.UnitId = sqn.UnitId;
                        tennantSqn.IsHost = false;
                        le.Tennants.Add(tennantSqn);
                        le.SaveChanges();

                        if (y.Code == "SF")
                        {
                            var rgtflt = new Unit();
                            rgtflt.Number = newThing.Number;
                            rgtflt.UseOrdinal = false;
                            rgtflt.MissionName = null;
                            rgtflt.ServiceIdx= (int)Liaison.Helper.Enumerators.ServicesBll.AirForce;
                            rgtflt.ServiceTypeIdx = (int)Helper.Enumerators.ServiceTypeBLL.Active;
                            rgtflt.RankSymbol = "|";
                            rgtflt.UnitGuid = Guid.NewGuid();
                            rgtflt.CanHide = true;
                            rgtflt.AdminCorpsId = 23;
                            le.Units.Add(rgtflt);
                            le.SaveChanges();

                            var uiRgtFlt = new UnitIndex();
                            uiRgtFlt.IndexCode = "RAFRGT|" + under;
                            uiRgtFlt.UnitId = rgtflt.UnitId;
                            uiRgtFlt.IsSortIndex = true;
                            uiRgtFlt.IsDisplayIndex = true;
                            uiRgtFlt.IsAlt = false;
                            uiRgtFlt.DisplayOrder = 10;
                            le.UnitIndexes.Add(uiRgtFlt);
                            le.SaveChanges();

                            var uiRgtFlt2 = new UnitIndex();
                            uiRgtFlt2.IndexCode = under + " FLT., RAF RGT.";
                            uiRgtFlt2.UnitId = uiRgtFlt.UnitId;
                            uiRgtFlt2.IsSortIndex = false;
                            uiRgtFlt2.IsDisplayIndex = true;
                            uiRgtFlt2.IsAlt = false;
                            uiRgtFlt2.DisplayOrder = 20;
                            le.UnitIndexes.Add(uiRgtFlt2);
                            le.SaveChanges();

                            var relRgtFlt = new Relationship();
                            relRgtFlt.RelationshipGuid = Guid.NewGuid();
                            relRgtFlt.RelTypeIdx = (int)Helper.Enumerators.RelationshipTypeBll.Organic;
                            relRgtFlt.RelFromUnitId = sqn.UnitId;
                            relRgtFlt.RelToUnitId = rgtflt.UnitId;
                            le.Relationships.Add(relRgtFlt);
                            le.SaveChanges();

                            var tennantRgtFlt = new Tennant();
                            tennantRgtFlt.BaseId = newThing.BaseId;
                            tennantRgtFlt.UnitId = rgtflt.UnitId;
                            tennantRgtFlt.IsHost = false;
                            le.Tennants.Add(tennantRgtFlt);
                            le.SaveChanges();

                            var polflt = new Unit();
                            polflt.Number = newThing.Number;
                            polflt.UseOrdinal = false;
                            polflt.MissionName = null;
                            polflt.ServiceIdx = (int)Liaison.Helper.Enumerators.ServicesBll.AirForce;
                            polflt.ServiceTypeIdx = (int)Helper.Enumerators.ServiceTypeBLL.Active;
                            polflt.RankSymbol = "|";
                            polflt.UnitGuid = Guid.NewGuid();
                            polflt.CanHide = true;
                            polflt.AdminCorpsId = 22;
                            le.Units.Add(polflt);
                            le.SaveChanges();

                            var uiPolFlt = new UnitIndex();
                            uiPolFlt.IndexCode = "RAFP|" + under;
                            uiPolFlt.UnitId = polflt.UnitId;
                            uiPolFlt.IsSortIndex = true;
                            uiPolFlt.IsDisplayIndex = true;
                            uiPolFlt.IsAlt = false;
                            le.UnitIndexes.Add(uiPolFlt);
                            le.SaveChanges();

                            var uiPolFlt2 = new UnitIndex();
                            uiPolFlt2.IndexCode = under + " FLT., RAFP";
                            uiPolFlt2.UnitId = polflt.UnitId;
                            uiPolFlt2.IsSortIndex = false;
                            uiPolFlt2.IsDisplayIndex = true;
                            uiPolFlt2.IsAlt = false;
                            le.UnitIndexes.Add(uiPolFlt2);
                            le.SaveChanges();

                            var relPolFlt = new Relationship();
                            relPolFlt.RelationshipGuid = Guid.NewGuid();
                            relPolFlt.RelTypeIdx = (int)Helper.Enumerators.RelationshipTypeBll.Organic;
                            relPolFlt.RelFromUnitId = sqn.UnitId;
                            relPolFlt.RelToUnitId = polflt.UnitId;
                            le.Relationships.Add(relPolFlt);
                            le.SaveChanges();

                            var tennantPolFlt = new Tennant();
                            tennantPolFlt.BaseId = newThing.BaseId;
                            tennantPolFlt.UnitId = polflt.UnitId;
                            tennantPolFlt.IsHost = false;
                            le.Tennants.Add(tennantPolFlt);
                            le.SaveChanges();
                        }

                    }
                }
            }

            return newThing;
        }

        private static string GetLongCode(Dictionary<string, string> unitindices, string candidate)
        {
            if (string.IsNullOrWhiteSpace(candidate))
            {
                throw new Exception("candidate empty");
            }
            if (!unitindices.ContainsKey(candidate))
            {
                throw new Exception("candidate="+candidate);
            }
            var r = unitindices.First(k => k.Key.Equals(candidate));

            return r.Value;
        }
    }
}