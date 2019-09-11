using System;
using System.Collections.Generic;
using System.Linq;
using Liaison.BLL.Models.Equipment;
using Liaison.BLL.Models.Objects;
using Liaison.BLL.Models.Unit.Interfaces;

namespace Liaison.BLL.Models.Unit.Abstracts
{
    public abstract class OneBar : AUnit, IUnit
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
			//var name = this.GetName();
            return this.Indices == null ? string.Empty : string.Join(",", DoFilter(this.Indices));
        }

		private string[] DoFilter(List<string> indices)
		{
			var name = this.GetName();

			for (int i = 0; i < indices.Count(); i++)
			{
				indices[i]= indices[i].Replace("_", "");
			}

			List<string> r = indices.Where(idx => idx != name).ToList();

			return r.ToArray();
		}

		public abstract EquipmentContainer GetEquipment();
        public bool IsTaskForce => false;
    }
}