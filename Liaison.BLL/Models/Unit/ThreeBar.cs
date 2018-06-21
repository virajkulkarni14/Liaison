using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Liaison.BLL.Models.Equipment;

namespace Liaison.BLL.Models.Unit
{
    public abstract class ThreeBar : AUnit, IUnit
    {
        public abstract string GetAdminCorps();
        public List<IEquipment> Equipment { get; set; }
        public bool UseOrdinal { get; set; }
        public abstract string GetName();
        public string PrintTree()
        {
            throw new NotImplementedException();
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
            return this.Indices == null ? string.Empty : string.Join(ExtensionMethods.Seperator, this.Indices);
        }

        public abstract string GetEquipment();

        
        public bool IsTaskForce => false;
    }
}