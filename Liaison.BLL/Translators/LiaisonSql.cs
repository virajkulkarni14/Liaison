using Liaison.BLL.Models;
using Liaison.Data.Sql.Edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.BLL.Translators
{
    public class LiaisonSql
    {
        public static IUnit GetTree(string input)
        {

            IUnit convUnit;
            using (var context = new LiaisonEntities())
            {
                Unit sqlUnit = Liaison.Data.Sql.GetStuff.GetItWithContext(context, input);

                convUnit = ConvertUnit(sqlUnit);

                var a = "b";



            }

            return convUnit;
        }
        //public static IUnit GetTree(int input)
        //{
        //    var sqlunit = Liaison.Data.Sql.GetStuff.GetIt(input);

        //    IUnit convUnit = ConvertUnit
        //}

        public static IUnit ConvertUnit(Unit sqlUnit)
        {
            if (sqlUnit.RankSymbol == "-")
            {
                return new Command(sqlUnit);
            }
            else if (sqlUnit.RankSymbol == "\"")
            {
                return new Command(sqlUnit);
            }
            else if (sqlUnit.RankSymbol == "$")
            {
                    return new Command(sqlUnit);
                }            
            else if (sqlUnit.RankSymbol == "%")
            {
                if (sqlUnit.ServiceIdx == (int)Helper.Enumerators.ServicesBll.Joint ||
                    sqlUnit.ServiceIdx == (int)Helper.Enumerators.ServicesBll.Army ||
                    sqlUnit.ServiceIdx==(int)Helper.Enumerators.ServicesBll.Navy)
                {
                    return new Command(sqlUnit);
                }

            }
            else if (sqlUnit.RankSymbol == "&")
            {
                if (sqlUnit.ServiceIdx == (int) Helper.Enumerators.ServicesBll.Joint)
                {
                    return new Command(sqlUnit);
                }
                else if (sqlUnit.ServiceIdx == (int)Helper.Enumerators.ServicesBll.Army)
                {
                    return new FieldArmy(sqlUnit);
                }
            }

            throw new Exception("not found; "+ sqlUnit.MissionName +"; Rank Symbol " + sqlUnit.RankSymbol + "; Service: " +
                                (Helper.Enumerators.ServicesBll) sqlUnit.ServiceIdx + "(" + sqlUnit.ServiceIdx + ")");
        }
    }
}
