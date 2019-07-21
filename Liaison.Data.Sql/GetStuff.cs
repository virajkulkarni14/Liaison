using System;
using Liaison.Data.Sql.Edmx;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
                .Include(ui => ui.Rank)
                .Include(ui => ui.AdminCorp)
                .Where(ui => ui.UnitId == unitId);

            return thisthing.First();
        }

        public static List<string> GetSortOrder(LiaisonEntities context)
        {
            return context.SortOrders.OrderBy(so => so.SortOrderRank).Select(so => so.SearchTerm).ToList();
        }

        public static List<string> GetJointGroupMissionNames()
        {
            using (var context = new LiaisonEntities())
            {
                return context.ConfigSettings.First(m => m.ConfigSetting1 == "JointGroupMissionNames").ConfigValue.Split(',').ToList();
            }
        }

        public static List<string> GetNavalSquadronMissionNames()
        {
            using (var context = new LiaisonEntities())
            {
                return context.ConfigSettings.First(m => m.ConfigSetting1== "NavalSquadronMissionNames").ConfigValue.Split(',').ToList();
            }
              
        }
        public static List<string> GetFacilityMissionNames()
        {
            using (var context = new LiaisonEntities())
            {
                return context.ConfigSettings.First(m=>m.ConfigSetting1== "FacilityMissionNames").ConfigValue.Split(',').ToList();
            }
        }

        public static List<int> GetConfigSetting(string input)
        {
            using (var context = new LiaisonEntities())
            {
                return context.ConfigSettings.First(m => m.ConfigSetting1 == input).ConfigValue.Split(',').ToList().Select(int.Parse).ToList();
            }
        }

        public static List<string> GetBattalionNoBracketMissionNames()
        {
            using (var context = new LiaisonEntities())
            {
                return context.ConfigSettings.First(m => m.ConfigSetting1 == "BattalionNoBracketMissionNames").ConfigValue.Split(',').ToList();
            }
        }

        //private static List<Tuple<string, string, string>> _dictionary = null;
        //public static Dictionary<string, string> GetDictionary(string dicttype)
        //{
        //    if (_dictionary == null)
        //    {
        //        _dictionary = new List<Tuple<string, string, string>>();
        //        using (var context = new LiaisonEntities())
        //        {
        //            var data = context.Dictionaries;

        //            foreach (var d in data)
        //            {
        //                _dictionary.Add(new Tuple<string, string, string>(d.Key, d.Value, d.Type));
        //            }


        //        }
        //    }

        //    Dictionary<string, string> returnable = new Dictionary<string, string>();
        //    foreach (var d in _dictionary)
        //    {
        //        if (d.Item3 == dicttype)
        //        {
        //            returnable.Add(d.Item1, d.Item2);
        //        }
        //    }

        //    return returnable;
        //}
        public static Dictionary<string, string> GetDictionary(string dicttype)
        {
            using (var context = new LiaisonEntities())
            {
                var data = context.Dictionaries.Where(d => d.Type == dicttype);
                var dictionary = new Dictionary<string, string>();
                foreach (var d in data)
                {
                    dictionary.Add(d.Key, d.Value);
                }

                return dictionary;
            }
        }
    }
}
