using Liaison.BLL.Models.Unit.Abstracts;
using Liaison.BLL.Models.Unit.Interfaces;

namespace Liaison.BLL.Models.Unit
{
	public class DefaultUnit : AUnit, IUnit
	{
		public string GetName()
		{
			return "";
		}

		public string PrintTree()
		{
			return "";
		}

		public bool IsDecommissioned()
		{
			return false;
		}

		public new bool GetIsHostUnit()
		{
			return false;
		}
		public int GetRankLevel()
		{
			return 0;
		}

		public string GetRankStar()
		{
			return "";
		}

		public string GetIndexes()
		{
			return "";
		}

		public string GetEquipment()
		{
			return "";
		}

		public bool IsTaskForce { get; }
		public string GetAdminCorps()
		{
			return "";
		}
	}
}