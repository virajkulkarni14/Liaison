using System;
using System.Collections.Generic;
using System.Linq;
using Liaison.Data.Sql.Edmx;

namespace Liaison.BLL.Models
{
    public class BLLRelationships : List<BLLRelationship>
    {

        public BLLRelationships(Data.Sql.Edmx.Unit unit)
        {
            foreach (var from in unit.RelationshipsFrom)
            {
                var relFrom = new BLLRelationship(unit.UnitId, from);
                this.Add(relFrom);
            }
            foreach (var to in unit.RelationshipsTo)
            {
                var relTo = new BLLRelationship(unit.UnitId, to);
                this.Add(relTo);
            }
        }
        public BLLRelationships(int unitid, ICollection<Relationship> relationshipsFrom, ICollection<Relationship> relationshipsTo)
        {
            foreach (var from in relationshipsFrom)
            {
                var relFrom = new BLLRelationship(unitid, from);
                this.Add(relFrom);
            }
            foreach (var to in relationshipsTo)
            {
                var relTo = new BLLRelationship(unitid, to);
                this.Add(relTo);
            }
        }

        public BLLRelationships(int unitid, IEnumerable<Relationship> relf)
        {
            var relationships = relf.ToList();
            if (relationships.Any() && relationships.Any(rr => rr.RelationshipsTo == null))
            {
                return;
            }

            Relationship rel1;

            IOrderedEnumerable<Relationship> ordered;
            try
            {
                ordered = relationships.OrderByDescending(rr => rr.RelationshipsTo.Rank.RankLevel);

                foreach (var rel in ordered)
                {
                    rel1 = rel;
                    var relrel = new BLLRelationship(unitid, rel);
                    this.Add(relrel);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public BLLRelationships(int unitId, List<Relationship> relParent, bool isParent)
        {
            foreach (var rel in relParent)
            {
                var relpar = new BLLRelationship(unitId, rel);
                this.Add(relpar);
            }
        }
    }
}