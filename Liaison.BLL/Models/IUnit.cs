using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liaison.Helper.Enumerators;
using Liaison.Data.Sql.Edmx;
using Liaison.BLL.Translators;
using System.Web;

namespace Liaison.BLL.Models
{
    public class BLLUnit
    {
        

        public BLLUnit(Unit rel)
        {
           // this.relationshipsFrom = rel.;
        }
    }
    public class BLLRelationship
    {
        public BLLRelationship(int sourceUnitId, Data.Sql.Edmx.Relationship relationship)
        {
            if (relationship.RelFromUnitId != sourceUnitId)
            {
                this.From = LiaisonSql.ConvertUnit(relationship.RelationshipsFrom);
            }
            if (relationship.RelToUnitId!=sourceUnitId)
            {
                this.To = LiaisonSql.ConvertUnit(relationship.RelationshipsTo);
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
    public class Relationships : List<BLLRelationship>
    {

        public Relationships(Unit unit)
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
        public Relationships(int unitid, ICollection<Relationship> relationshipsFrom, ICollection<Relationship> relationshipsTo)
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

        public Relationships(int unitid, IEnumerable<Relationship> relf)
        {
            foreach (var rel in relf)
            {
                var relrel = new BLLRelationship(unitid, rel);
                this.Add(relrel);
            }
        }
    }
    public abstract class AUnit
    {
        internal int UnitId;
        internal Guid UnitGuid;
        internal ServicesBll Service;
        internal ServiceTypeBLL ServiceType;
        internal char RankSymbol;
        internal int? RankLevel;
        internal string RankStar;

        internal  List<BLLRelationship> Relationships;

        protected static string PrintAnyTree(IUnit unit)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            
            GetNextLine(ref sb, i, unit);

            return sb.ToString();
        }

        private static void GetNextLine(ref StringBuilder sb, int i, IUnit unit)
        {
            StringBuilder sbIndent = new StringBuilder();
            for (int j = 0; j < i; j++)
            {
                sbIndent.Append("&nbsp&nbsp&nbsp&nbsp");
            }

            string relSymbol = "• ";



            sb.Append(sbIndent.ToString() + relSymbol + unit.GetName() + unit.GetRankStar()+Environment.NewLine);

            foreach (IUnit childunit in unit.GetRelationships())
            {
                GetNextLine(ref sb, i+1, childunit);
            }
            //foreach (var r in )
        }
    }
    public interface IUnit
    {
        string GetName();
        IEnumerable<IUnit> GetRelationships();
        string PrintTree();
        string GetRankLevel();
        string GetRankStar();
    }
    public class Command : AUnit, IUnit
    {
        private string Name { get; set; }

        public Command(Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.UnitGuid = sqlUnit.UnitGuid;
            this.Name = sqlUnit.MissionName;
            this.Service = (ServicesBll)sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL)sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.RankLevel = sqlUnit.Rank.RankLevel;
            this.RankStar = sqlUnit.Rank.Rank1;

           // this.Relationships = new Relationships(sqlUnit)

            var relMain = sqlUnit.RelationshipsFrom.ToList();
            var relt = sqlUnit.RelationshipsTo.ToList();

            relMain.AddRange(relt);
            this.Relationships = new Relationships(sqlUnit.UnitId, relt);

           // this.Relationships = new Relationships(sqlUnit.RelationshipsFrom, sqlUnit.RelationshipsTo);
        }

        


        public string GetName()
        {
            return this.Name;
        }

        public IEnumerable<IUnit> GetRelationships()
        {
            IList<IUnit> childunits = new List<IUnit>();
            foreach (var r in this.Relationships)
            {
                childunits.Add(r.To);
            }

            return childunits;
        }

        private int _i = 0;
        public string PrintTree()
        {
            return AUnit.PrintAnyTree(this);

            

            //return HttpContext.Current.Server.HtmlDecode(sb.ToString()) + GetName();
        }

        public string GetRankLevel()
        {
          return (RankLevel ?? 0).ToString();          
        }

        public string GetRankStar()
        {
            return RankStar;
        }
    }

    public abstract class FourStar : IUnit
    {

    }
    public abstract class ThreeStar : IUnit
    {
        public string GetName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUnit> GetRelationships()
        {
            return null;
        }

        public string PrintTree()
        {
            throw new NotImplementedException();
        }

        public string GetRankLevel()
        {
            throw new NotImplementedException();
        }

        public string GetRankStar()
        {
            throw new NotImplementedException();
        }
    }

    public class FieldArmy : FourStar
    {

    }
    public class Corps :ThreeStar
    {

    }

}
