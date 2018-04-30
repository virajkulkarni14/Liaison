using Liaison.Data.Sql.Edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Liaison.Data.Sql
{
    public class GetStuff
    {
   



        //public static UnitIndex GetItWithContext(LiaisonEntities context, string unitindex)
        //{
        //    if (string.IsNullOrEmpty(unitindex))
        //    {
        //        unitindex = "JFHQ";
        //    }

        //    var thisthing = context.UnitIndexes
        //        .Include(ui => ui.Unit)
        //        .Include(ui => ui.Unit.RelationshipsFrom)
        //        .Include(ui => ui.Unit.RelationshipsTo)
        //        .Include(ui => ui.Unit.RelationshipsFrom.Select(rf => rf.RelationshipType))
        //        .Where(ui => ui.IndexCode == unitindex);

        //    var x = thisthing.FirstOrDefault();

        //    return x;
        //}

            public static Unit GetItWithContext(LiaisonEntities context, string unitindex)
        {
            if (string.IsNullOrEmpty(unitindex))
            {
                unitindex = "JFHQ";
            }

            var xxxx = context.UnitIndexes
                .Where(ui => ui.IndexCode == unitindex).First();

            return GetUnit(context, xxxx.UnitId);
        }

        public static Unit GetUnit(LiaisonEntities context, int unitId)
        {
            var thisthing = context.Units
                .Include(ui=>ui.RelationshipsFrom)
                .Include(ui=>ui.RelationshipsTo)
                .Where(ui => ui.UnitId == unitId);

            return thisthing.First();

        }

        ////public static UnitIndex GetItWithContext(LiaisonEntities context, int input)
        ////{
        ////    using 
        ////}
    }
}
