using System.Text;
using Liaison.Data.Sql.Edmx;

namespace Liaison.BLL.Models.Objects
{
	public class VesselClass
	{
		public VesselClass(ShipClass shipClass, bool isLeadShip)
		{
			if (shipClass == null) return;

			this.ClassName = shipClass.ClassName;
			this.ClassCode = new HCS(shipClass.ClassCodeHCS, shipClass.ClassCodeNumber);
			this.ModName = shipClass.ModName;
			this.IsLeadShip = isLeadShip;
		}

		public bool IsLeadShip { get; set; }

		public HCS ClassCode { get; set; }

		public string ClassName { get; set; }
		public string ModName { get; set; }

		public string GetClassName()
		{
			StringBuilder sb=new StringBuilder();
			if (this.IsLeadShip) { sb.Append("<u>"); }
			sb.Append(this.ClassName);
			sb.Append(" class");
			if (this.IsLeadShip) { sb.Append("</u>"); }
			if (!string.IsNullOrWhiteSpace(this.ModName)) { sb.Append(", " + this.ModName); }

			return sb.ToString();
		}
	}
}