using System;
using System.Collections.Generic;
using Liaison.BLL.Models.Equipment;
using Liaison.BLL.Models.Objects;
using Liaison.BLL.Models.Unit.Interfaces;

namespace Liaison.BLL.Models.Unit.Abstracts
{
    public abstract class ThreeBlob : AUnit, IUnit
    {
        public List<IEquipment> Equipment { get; set; }

        public abstract string GetAdminCorps();
        public bool IsDecommissioned()
        {
            return Decommissioned;
        }


        public abstract string GetName();
        public string PrintTree()
        {
            throw new NotImplementedException();
        }
        public int GetRankLevel()
        {
            return RankLevel ?? 0;
        }

        public string GetRankStar()
        {
            return RankStar;
        }

        public string GetIndexes()
        {
            return this.Indices == null ? string.Empty : string.Join("<font color='black'>/</font>", this.Indices);
        }

        public abstract EquipmentContainer GetEquipment();
        public bool IsTaskForce => false;
    }
}