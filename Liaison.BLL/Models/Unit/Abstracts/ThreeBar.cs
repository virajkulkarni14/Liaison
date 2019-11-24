using System.Collections.Generic;
using Liaison.BLL.Models.Equipment;
using Liaison.BLL.Models.Objects;
using Liaison.BLL.Models.Unit.Interfaces;

namespace Liaison.BLL.Models.Unit.Abstracts
{
    public abstract class ThreeBar : AUnit, IUnit
    {
        internal static int ThreeBarTab = 11;
        internal static string ThreeBarStar = "|||";
        public abstract string GetAdminCorps();
        public List<IEquipment> Equipment { get; set; }
        public bool UseOrdinal { get; set; }
        public abstract string GetName();
        public string PrintTree()
        {
            return AUnit.PrintAnyTree(this);
        }

        public  int GetRankLevel()
        {
            return RankLevel ?? 0;
        }
        public string GetRankStar()
        {
            return RankStar;
        }

        public string GetIndexes()
        {
            return this.Indices == null ? string.Empty : string.Join(ResourceStrings.Seperator, this.Indices);
        }

        public abstract EquipmentContainer GetEquipment();

        
        public bool IsTaskForce => false;
        public bool IsDecommissioned()
        {
            return Decommissioned;
        }
    }
}