using Liaison.Data.Sql.Edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Liaison.BLL.Models;
using Liaison.BLL.Models.Unit;
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

        public static IUnit GetTree(string input, int? depth)
        {
            depthRequired = depth == 0 ? null : depth;
            
            IUnit convUnit;
            if (Int32.TryParse(input, out int iInput))
            {
                using (var context = new LiaisonEntities())
                {
                    Unit sqlUnit = Liaison.Data.Sql.GetStuff.GetItWithContext(context, iInput);

                    convUnit = ConvertUnit(sqlUnit);
                }
            }
            else
            {
                using (var context = new LiaisonEntities())
                {
                    Unit sqlUnit = Liaison.Data.Sql.GetStuff.GetItWithContext(context, input);

                    convUnit = ConvertUnit(sqlUnit);
                }
            }

            return convUnit;
        }

        private static  int? depthRequired = null;        

        public static IUnit ConvertUnit(Unit sqlUnit) //, bool includeParent)
        {
            var thisUnitRankLevel = sqlUnit.Rank.RankLevel;
            if (depthRequired == null)
            {
                depthRequired =100;
            }

            if (thisUnitRankLevel > depthRequired)
            {
                return new DefaultUnit();
            }

            var cont = thisUnitRankLevel <= depthRequired;

            //var s = sqlUnit.Rank.RankLevel;
            if (sqlUnit.RankSymbol == "!")
            {
                return new Command(sqlUnit, cont); //, includeParent);
            }

            if (sqlUnit.RankSymbol == "\"")
            {
                return new Command(sqlUnit, cont); //, includeParent);
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
                    return new Division(sqlUnit); //, includeParent);
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
            }
            else if (sqlUnit.RankSymbol == "*")
            {
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Joint)
                {
                    return new Command(sqlUnit, cont); //, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce)
                {
                    return new AirForceBase(sqlUnit); //, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Army)
                {
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
            }
            else if (sqlUnit.RankSymbol == "/")
            {
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

                    return new Facility(sqlUnit);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce)
                {
                    return new AirWing(sqlUnit);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Marines)
                {

                    if (sqlUnit.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarinesAirArm||
                        sqlUnit.AdminCorpsId == (int)Helper.Enumerators.AdminCorps.RoyalIndianMarinesAirArm)
                    {
                        return new AirWing(sqlUnit);
                    }
                    if (sqlUnit.AdminCorpsId == (int)Helper.Enumerators.AdminCorps.RoyalMarineLogistics ||
                        sqlUnit.AdminCorp.AdminCorpsId == (int)Helper.Enumerators.AdminCorps.RoyalMarineArtillery ||
                        sqlUnit.AdminCorp.AdminCorpsId == (int)Helper.Enumerators.AdminCorps.RoyalMarineLightInfantry
                    )
                    {
                        return new Regiment(sqlUnit);
                    }

                    throw new Exception("Marine Air Corps not processed");
                }
            }
            else if (sqlUnit.RankSymbol == "@")
            {
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Marines)
                {
                    if (sqlUnit.AdminCorp.ParentAdminCorpsId == (int) Helper.Enumerators.AdminCorps.RoyalMarineAviation)
                    {
                        return new AirSquadron(sqlUnit);
                    }

                    throw new Exception("Other type of marine @");
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce)
                {
                    return new AirSquadron(sqlUnit);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
                {
                    if (sqlUnit.AdminCorpsId == (int) Helper.Enumerators.AdminCorps.FleetArmArm)
                    {
                        return new AirSquadron(sqlUnit);
                    }
                }
            }
            else if (sqlUnit.RankSymbol == "|")
            {
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce)
                {
                    return new Flight(sqlUnit);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
                {
                    if (sqlUnit.Ships != null && sqlUnit.Ships.Any())
                    {
                        return new Vessel(sqlUnit);
                    }

                    return new Facility(sqlUnit);
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

            throw new Exception("not found; " + sqlUnit.MissionName + "; Rank Symbol " + sqlUnit.RankSymbol +
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
                    unitIndex.IsDisplayIndex = false;
                    unitIndex.IsAlt = false;
                    le.UnitIndexes.Add(unitIndex);
                    le.SaveChanges();

                    unitIndex=new UnitIndex();
                    unitIndex.IndexCode = under + " " + x.Code + "W/RAF";
                    unitIndex.UnitId = unit.UnitId;
                    unitIndex.IsSortIndex = false;
                    unitIndex.IsDisplayIndex = true;
                    unitIndex.IsAlt = false;
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
                        uiSqn.IsDisplayIndex = false;
                        uiSqn.IsAlt = false;
                        le.UnitIndexes.Add(uiSqn);
                        le.SaveChanges();

                        var uiSqn2 = new UnitIndex();
                        uiSqn2.IndexCode = under + " " + y.Code + "S/RAF";
                        uiSqn2.UnitId = sqn.UnitId;
                        uiSqn2.IsSortIndex = false;
                        uiSqn2.IsDisplayIndex = true;
                        uiSqn2.IsAlt = false;
                        le.UnitIndexes.Add(uiSqn2);
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
                            uiRgtFlt.IsDisplayIndex = false;
                            uiRgtFlt.IsAlt = false;
                            le.UnitIndexes.Add(uiRgtFlt);
                            le.SaveChanges();

                            var uiRgtFlt2 = new UnitIndex();
                            uiRgtFlt2.IndexCode = under + " FLT., RAF RGT.";
                            uiRgtFlt2.UnitId = uiRgtFlt.UnitId;
                            uiRgtFlt2.IsSortIndex = false;
                            uiRgtFlt2.IsDisplayIndex = true;
                            uiRgtFlt2.IsAlt = false;
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
                            uiPolFlt.IsDisplayIndex = false;
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

    }
}