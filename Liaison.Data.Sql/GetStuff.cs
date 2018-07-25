using Liaison.Data.Sql.Edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Liaison.Data.Sql
{
    public class GetStuff
    {
        public static Unit GetItWithContext(LiaisonEntities context, int unitId)
        {
            var xxx = context.UnitIndexes.First(ui => ui.UnitId == unitId);

            return GetUnit(context, xxx.UnitId);
        }

        public static Unit GetItWithContext(LiaisonEntities context, string unitindex)
        {
            if (string.IsNullOrEmpty(unitindex))
            {
                unitindex = "JFHQ";
            }

            var xxxx = context.UnitIndexes.First(ui => ui.IndexCode == unitindex);

            return GetUnit(context, xxxx.UnitId);
        }

        public static Unit GetUnit(LiaisonEntities context, int unitId)
        {
            var thisthing = context.Units
                .Include(ui => ui.RelationshipsFrom)
                .Include(ui => ui.RelationshipsTo)
                .Include(ui=>ui.Rank)
                .Where(ui => ui.UnitId == unitId);

            return thisthing.First();

        }

        public static List<string> GetSortOrder(LiaisonEntities context)
        {
            return context.SortOrders.OrderBy(so => so.SortOrderRank).Select(so => so.SearchTerm).ToList();
        }

        public static List<string> GetNavalSquadronMissionNames()
        {
            using (var context = new LiaisonEntities())
            {
                return context.ConfigSettings.First(m => m.ConfigSetting1== "NavalSquadronMissionNames").ConfigValue.Split(',').ToList();
            }
              
        }
    }
}
