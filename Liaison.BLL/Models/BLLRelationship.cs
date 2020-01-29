using System;
using System.Collections.Generic;
using Liaison.BLL.Models.Unit;
using Liaison.BLL.Models.Unit.Interfaces;
using Liaison.BLL.Translators;
using Liaison.Data.Sql.Edmx;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models
{
    public class ParentsBll : List<ParentBll>
    {
        public ParentsBll(int unitId, List<Relationship> relMain)
        {
            foreach (var parent in relMain)
            {
                this.Add(new ParentBll(unitId, parent));

            }
        }
    }
    public class ParentBll
    {
        public ParentBll(int unitId, Relationship parent)
        {
            this.RelationshipType = (HigherHqType)parent.RelationshipType.RelationshipTypeId;
            this.CommandName = parent.RelationshipsFrom.CommandName;
            this.UnitId = unitId;
        }

        public HigherHqType RelationshipType { get; set; }
        public int UnitId { get; set; }
        public string CommandName { get; set; }
    }
    public class BLLRelationship
    {
        public BLLRelationship(int sourceUnitId, Data.Sql.Edmx.Relationship relationship)
        {
            var liaisonSql = new LiaisonSql();
            if (relationship.RelFromUnitId != sourceUnitId)
            {
                this.From = liaisonSql.ConvertUnit(relationship.RelationshipsFrom);
            }
            if (relationship.RelToUnitId!=sourceUnitId)
            {
                this.To = liaisonSql.ConvertUnit(relationship.RelationshipsTo);
            }
            
            this.RelationshipGuid = relationship.RelationshipGuid;
            this.RelationshipId = relationship.RelationshipId;
            this.RelType = relationship.RelationshipType;
        }

        public Guid RelationshipGuid { get; }
        public int RelationshipId { get; }
        public RelationshipType RelType { get; }
        //public IUnit With { get; set; }
        public IUnit From { get; private set; }
        public IUnit To { get; private set; }
    }
}