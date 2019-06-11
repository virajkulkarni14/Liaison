using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Liaison.BLL.Models.Objects;
using Liaison.BLL.Models.Unit.Interfaces;
using Liaison.BLL.Translators;
using Liaison.Data.Sql.Edmx;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit.Abstracts
{
	public abstract class AUnit
    {
        internal bool Decommissioned;
        internal int UnitId;
        internal Guid UnitGuid;
        internal ServicesBll Service;
        internal ServiceTypeBLL ServiceType;
        internal char RankSymbol;
        internal int? RankLevel;
        internal string RankStar;
        internal int? Number;
        internal string NickName;
        internal string MissionName;
        internal string SortIndex;
        internal bool CanHide;
        internal string Language;

        internal List<BLLRelationship> Relationships;

        //public List<Relationship> Parents2 { get; set; }
        //public List<int> Parents1 { get; set; }
        //internal List<int> Parents;
        //internal List<ParentBll> Parents;

        internal BLLAdminCorps AdminCorps;
        internal BLLBase Base;
        internal List<BllMission> Mission;
        internal List<string> Indices;

        public bool GetIsHostUnit()
        {
            return this.Base.IsHost;
        }

        //public string GetAdminCorps()
        //{
        //    return this.AdminCorps == null ? string.Empty : this.AdminCorps.DisplayName;
        //}

        public IEnumerable<RelationshipTracker> GetParents(int unitId, HigherHqType type)
        {
            List<RelationshipTracker> list;
			using (var entities2 = new LiaisonEntities())

            using (var entities = new LiaisonEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;

                var parents = entities.Relationships.Where(r => r.RelToUnitId == unitId)
                    .Include(r => r.RelationshipType)
                    .Include(r => r.RelationshipsFrom)
                    .Include(r => r.RelationshipsFrom.TaskForce)
                    .Include(r => r.RelationshipsFrom.Rank)
                    .Include(r=>r.RelationshipsFrom.AdminCorp);

                list = new List<RelationshipTracker>();
                foreach (var par in parents)
                {
                    list.Add(new RelationshipTracker(LiaisonSql.ConvertUnit(par.RelationshipsFrom),
                        par.RelationshipType));
                }
            }

            return list;
        }

        protected static string PrintAnyTree(IUnit unit)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            
            var tr= new RelationshipTracker(unit, null);
            GetNextLine(ref sb, i, tr);
            //TODO if unit.GetCanHide()
            return sb.ToString();
        }


        private static void GetNextLine(ref StringBuilder sb, int i, RelationshipTracker rt)
        {
            if (rt.Unit.GetType() == typeof(DefaultUnit))
            {
                return;
            
            }


            var unit = rt.Unit;

            var isTaskForce = rt.Unit.IsTaskForce;
            var numbRl = rt.Unit.GetRankLevel();
            var name = rt.Unit.GetName();
            var relationships = unit.GetRelationships();
            string taskforceMainName = "";

            var relationshipTrackers = relationships.ToList();
            //var relParents = unit.GetParents();
            if (isTaskForce)
            {
                var tfMainUnit = relationshipTrackers.FirstOrDefault(c =>
                    c.RelationshipType.RelationshipTypeId == (int) HigherHqType.TaskForce);
                if (tfMainUnit != null)
                {
                    numbRl = tfMainUnit.Unit.GetRankLevel();
                    taskforceMainName = tfMainUnit.Unit.GetName();
                }
            }

            StringBuilder sbIndent = new StringBuilder();
            for (int j = 0; j < numbRl; j++)
            {
                sbIndent.Append("&nbsp&nbsp&nbsp&nbsp");
            }

            string relSymbol = "• ";
            string otherCommand = "";
            if (rt.RelationshipType != null)
            {
                switch (rt.RelationshipType.RelationshipTypeId)
                {
                    case (int) HigherHqType.OPCON:
                    {
                        relSymbol = "+ ";


                        var x = (HigherHqType) rt.RelationshipType.RelationshipTypeId;
                        //var relSix1 = rt.Unit.GetParents(unit.GetId(), x);
                        //var relSix2 = relSix1.Where(r =>
                        //    r.RelationshipType.RelationshipTypeId == (int) HigherHqType.ADCON);
                        //var relSix3 = relSix2.Select(r => r.Unit.GetName());


                        List<string> relSix = rt.Unit.GetParents(unit.GetId(), x)
                            .Where(r => r.RelationshipType.RelationshipTypeId == (int) HigherHqType.ADCON)                         
                            .Select(r => r.Unit.GetName()).ToList();
                        //   var relSix = relationshipTrackers.Where(r =>
                        //    r.RelationshipType.RelationshipTypeId == (int) Helper.Enumerators.HigherHqType.ADCON)
                        // .Select(r => r.Unit.GetName());
                        otherCommand = string.Join(",", relSix);
                        otherCommand = " <span class='lzRelOpcon'>" + otherCommand + "</span>";
                        break;
                    }
                    case (int) HigherHqType.ADCON:
                    {
                        relSymbol = "- ";
                        var x = (HigherHqType)rt.RelationshipType.RelationshipTypeId;
                            //var relFour = relationshipTrackers
                            //  .Where(r => r.RelationshipType.RelationshipTypeId == (int) HigherHqType.OPCON)
                            // .Select(r => r.Unit.GetName());
                        var relFourA = rt.Unit.GetParents(unit.GetId(), x);
                        var relFourB = relFourA.Where(r => r.RelationshipType.RelationshipTypeId == (int) HigherHqType.OPCON);
                        var relFour = relFourB.Select(r => r.Unit.GetName()).ToList();

                        otherCommand = string.Join(",", relFour);
                        otherCommand = " <span class='lzRelAdcon'>" + otherCommand + "</span>";
                        break;
                    }
                }
            }

            var name2 = unit.GetName();

            var unitid = unit.GetId();

            string mission = unit.GetMission();
            string indexes = unit.GetIndexes().Replace("_", "");
            string equipment = unit.GetEquipment();
            string unitAdminCorps = unit.GetAdminCorps();



            sb.Append(sbIndent.ToString() + relSymbol);
            sb.Append("<span class='lzRankStar'>" + unit.GetRankStar() + "</span>");
            sb.Append(" <span class='lzUnitName'>(" + unitid + ") " + name + "</span>");
            if (!string.IsNullOrWhiteSpace(indexes))
            {
                sb.Append(" <span class='lzIndex'>(" + indexes + ")</span>");
            }

            if (!string.IsNullOrWhiteSpace(otherCommand))
            {
                sb.Append(otherCommand);
            }

            if (!string.IsNullOrWhiteSpace(taskforceMainName))
            {
                sb.Append(" <span class='lzTaskForce'>(" + taskforceMainName + ")</span>");
            }

            if (!string.IsNullOrWhiteSpace(mission))
            {
                sb.Append(" [<span class='lzMission'>" + mission + "</span>]");
            }

            if (!string.IsNullOrWhiteSpace(equipment))
            {
                sb.Append(" <span class='lzEquipment'>" + equipment + "</span>");
            }

            if (!string.IsNullOrWhiteSpace(unitAdminCorps))
            {
                sb.Append(" <span class='lzAdminCorps'>" + unitAdminCorps + "</span>");
            }

            if (unit.GetIsHostUnit())
            {
                sb.Append(", <span class='lzBaseHost'>" + unit.GetBase() + "</span>");
            }
            else
            {
                sb.Append(", <span class='lzBase'>" + unit.GetBase() + "</span>");
            }

            sb.Append(Environment.NewLine);

            var rels = SortRelationships(relationshipTrackers);

            int[] types = {1, 2, 4, 6};
            foreach (RelationshipTracker childunit in rels.Where(r =>
                types.Contains(r.RelationshipType.RelationshipTypeId)))
            {
                GetNextLine(ref sb, i + 1, childunit);
            }
        }

        private static IEnumerable<RelationshipTracker> SortRelationships(IEnumerable<RelationshipTracker> enumerable)
        {
            List<RelationshipTracker> units = enumerable.Where(u=>u.Unit.IsDecommissioned()==false).ToList();
            int preCount = units.ToList().Count();

            List<RelationshipTracker> returnable2 = new List<RelationshipTracker>();
            List<string> log = new List<string>();

            for (int i = 20; i > -1; i--)
            {                
                IList<RelationshipTracker> currentRankedUnits = units.Where(u => u.Unit.GetRankLevel() == i).OrderBy(u=>u.Unit.GetSortString()).ToList();

                if (currentRankedUnits.Count() != 0)
                {
                    List<string> sortOrder = LiaisonSql.GetSortOrder();

                    foreach (var str in sortOrder)
                    {
                        var found = currentRankedUnits.Where(u => u.Unit.GetSortString().StartsWith(str)).ToList();
                        if (found.Count > 0)
                        {
                            returnable2.AddRange(found);
                            var l = found.Select(c => c.Unit.GetSortString());
                            log.Add("Adding " + string.Join(",", l) + " because " + str);
                        }

                        foreach (var f in found)
                        {
                            currentRankedUnits.Remove(f);
                        }
                    }
                    returnable2.AddRange(currentRankedUnits);
                    log.Add("Adding " + string.Join(",", currentRankedUnits) + " because last remaining");
                }
                

            }
        
            if (returnable2.Count != preCount)
            {
                throw new Exception("Counts don't match");
            }
            return returnable2;
           
        }

        public string GetSortString() => this.SortIndex ?? "";
        public int GetId() => this.UnitId;

        public string GetIndex()
        {
            return this.Indices == null ? string.Empty : string.Join(",", this.Indices);
        }

        public string GetMission()
        {
            if (this.Mission == null)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            
            foreach (var mission in this.Mission)
            {               
                sb.Append("(" + mission.MissionId + ") ");
                sb.Append(mission.DisplayName);
                if (!string.IsNullOrWhiteSpace(mission.Variant))
                {
                    sb.Append(" - " + mission.Variant);
                }

                sb.Append(ResourceStrings.Seperator);
            }

            var returnable = sb.ToString();
            if (string.IsNullOrWhiteSpace(returnable))
            {
                return returnable;
            }
            return returnable.Substring(0, returnable.Length - ResourceStrings.Seperator.Length);
        }

        public string GetBase()
        {
            if (this.Base == null)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("(" + this.Base.BaseId + ") ");
            if (!string.IsNullOrWhiteSpace(this.Base.Prefix))
            {
                sb.Append(this.Base.Prefix + " ");
            }

            sb.Append(this.Base.Name);
            if (!string.IsNullOrWhiteSpace(this.Base.Suffix))
            {
                if (!this.Base.Suffix.StartsWith(","))
                {
                    sb.Append(" ");
                }

                sb.Append(this.Base.Suffix);
            }

            if (!string.IsNullOrWhiteSpace(this.Base.CommissionedName))
            {
                sb.Append(" (" + this.Base.CommissionedName + ")");
            }

            if (!string.IsNullOrWhiteSpace(this.Base.City))
            {
                sb.Append(", "+this.Base.City);                
            }

            if (!string.IsNullOrWhiteSpace(this.Base.Iso3166))
            {
                sb.Append(", " + this.Base.Iso3166);
            }

            return sb.ToString();

        }

        public IEnumerable<RelationshipTracker> GetRelationships()
        {
            IList<RelationshipTracker> childunits = new List<RelationshipTracker>();
            var x = this.GetId();
            if (x == 0 && (this.GetType() == typeof(DefaultUnit)))
            {
                return childunits;
            }
            try
            {
                
                foreach (var r in this.Relationships.OrderBy(ro => ro.To.GetRankLevel()))
                {
                    childunits.Add(new RelationshipTracker(r.To, r.RelType));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return childunits;
        }

        //public IEnumerable<RelationshipTracker> GetParent()
        //{
        //    IList<RelationshipTracker> parentUnits = new List<RelationshipTracker>();
        //    foreach (var r in this.Parents.OrderBy(par => par.CommandName))
        //    {

        //    }

        //    return parentUnits;
        //}



        internal string GetSortIndex(ICollection<UnitIndex> unitIndexes)
        {
            return unitIndexes.FirstOrDefault(ii => ii.IsSortIndex == true)?.IndexCode;
        }

        protected static AdminCorps GetAdminCorpsHelper(IUnit iunit)
        {
            using (var content = new LiaisonEntities())
            {
                int i = iunit.GetId();
                var thisThing = content.Units.First(u => u.UnitId == i);

                var sqlAdminCorps = content.AdminCorps.FirstOrDefault(ac => ac.AdminCorpsId == thisThing.AdminCorpsId); ;
                if (sqlAdminCorps != null)
                {
                return new AdminCorps(sqlAdminCorps.Code, sqlAdminCorps.Name,
                        sqlAdminCorps.AdminCorpsId);
                }
            }

            throw new Exception();
        }

        protected static void GetServiceType(StringBuilder sb, 
            ServiceTypeBLL thisServiceType, 
            string thisTerritorialDesignation, 
            string thisCommandName, 
            bool precedingSpace, 
            bool trailingSpace)
        {
            if (precedingSpace)
            {
                sb.Append(" ");
            }
            if (thisServiceType == ServiceTypeBLL.Reserve && !CommandNameContains(thisCommandName, "(R)"))
            {
                sb.Append("(R)");
            }
            else if (thisServiceType == ServiceTypeBLL.Volunteer && !CommandNameContains(thisCommandName, "(V)"))
            {
                sb.Append("(V) ");
                sb.Append("(" + thisTerritorialDesignation + ")");
            }

            if (trailingSpace)
            {
                sb.Append(" ");
            }
        }

        private static bool CommandNameContains(string thisCommandName, string s)
        {
            if (thisCommandName == null)
            {
                return false;
            }

            if (thisCommandName.Contains(s))
            {
                return true;
            }

            return false;

        }
    }
}