using Liaison.Data.Sql.Edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using Liaison.BLL.Models.Unit;

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

        public static IUnit GetTree(string input)
        {
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

        public static IUnit ConvertUnit(Unit sqlUnit)//, bool includeParent)
        {
            if (sqlUnit.RankSymbol == "-")
            {
                return new Command(sqlUnit);//, includeParent);
            }

            if (sqlUnit.RankSymbol == "\"")
            {
                return new Command(sqlUnit);//, includeParent);
            }

            if (sqlUnit.RankSymbol == "$")
            {
                return new Command(sqlUnit);//, includeParent);
            }

            if (sqlUnit.RankSymbol == "%")
            {
                if (sqlUnit.Number != null)
                {
                    return new ArmyGroup(sqlUnit);//, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Joint ||
                    sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Army ||
                    sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce ||
                    sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
                {
                    return new Command(sqlUnit);//, includeParent);
                }
            }
            else if (sqlUnit.RankSymbol == "&")
            {
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Joint)
                {
                    return new Command(sqlUnit);//, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Army)
                {
                    return new FieldArmy(sqlUnit);//, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce)
                {
                    return new TacticalAirForce(sqlUnit);//, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
                {
                    if (sqlUnit.Number == null)
                    {
                        return new Command(sqlUnit);//, includeParent);
                    }
                    else
                    {
                        return new NamedFleet(sqlUnit);//, includeParent);
                    }
                }
            }
            else if (sqlUnit.RankSymbol == "(")
            {
                if (sqlUnit.ServiceIdx == (int)Helper.Enumerators.ServicesBll.Joint)
                {
                    return new Command(sqlUnit);//, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Army)
                {
                    if (sqlUnit.Number == null)
                    {
                        return new Command(sqlUnit);//, includeParent);
                    }

                    return new Corps(sqlUnit);//, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce)
                {
                    if (sqlUnit.Number == null)
                    {
                        return new Command(sqlUnit);//, includeParent);
                    }

                    return new NumberedAirForce(sqlUnit);
                }
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
                {
                    if (sqlUnit.Number == null)
                    {
                        return new Command(sqlUnit);//, includeParent);
                    }

                    return new NumberedFleet(sqlUnit);//, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Marines)
                {
                    if (sqlUnit.Number == null)
                    {
                        return new Command(sqlUnit);//, includeParent);
                    }
                }
            }
            else if (sqlUnit.RankSymbol == ")")
            {
                if (sqlUnit.ServiceIdx == (int)Helper.Enumerators.ServicesBll.Army)
                {
                    return new Division(sqlUnit);//, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
                {
                    if (sqlUnit.Number == null)
                    {
                        return new Command(sqlUnit);//, includeParent);
                    }
                }
            }
            else if (sqlUnit.RankSymbol == "*")
            {
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Joint)
                {
                    return new Command(sqlUnit);//, includeParent);
                }
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce)
                {
                    return new AirForceBase(sqlUnit);//, includeParent);
                }
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Army)
                {
                    return new Brigade(sqlUnit);//, includeParent);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
                {
                    return new Flotilla(sqlUnit);
                }
            }
            else if (sqlUnit.RankSymbol == "/")
            {
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
                {
                    if (sqlUnit.MissionName == "Submarine")
                    {
                        return new NavalSquadron(sqlUnit);
                    }

                    if (sqlUnit.AdminCorp?.Code == "FAA")
                    {
                        return new AirWing(sqlUnit); //, includeParent);
                    }

                    return new Facility(sqlUnit);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce)
                {
                    return new AirWing(sqlUnit);
                }
            }
            else if (sqlUnit.RankSymbol == "@")
            {
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Navy)
                {
                    if (sqlUnit.Ships != null && sqlUnit.Ships.Any())
                    {
                        return new Vessel(sqlUnit);
                    }

                    if (sqlUnit.AdminCorp?.Code == "FAA")
                    {
                        return new AirSquadron(sqlUnit); //, includeParent);
                    }
                    return new Facility(sqlUnit);
                }

                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.AirForce)
                {
                    return new AirSquadron(sqlUnit);
                }
            }
            else if (sqlUnit.RankSymbol == "^")
            {
                return new TaskForceBll(sqlUnit);//, includeParent);
            }
            else if (sqlUnit.RankSymbol == "?")
            {
                return new DetachmentBll(sqlUnit);//, includeParent);
            }

            throw new Exception("not found; "+ sqlUnit.MissionName +"; Rank Symbol " + sqlUnit.RankSymbol + "; Service: " +
                                (Helper.Enumerators.ServicesBll) sqlUnit.ServiceIdx + "(" + sqlUnit.ServiceIdx + ")");
        }


    }
}
