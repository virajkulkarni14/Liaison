using Liaison.BLL.Models.Unit.Interfaces;
using Liaison.Data.Sql.Edmx;

namespace Liaison.BLL.Models.Objects
{
	public class RelationshipTracker
	{
		public IUnit Unit;
		public RelationshipType RelationshipType;

		public RelationshipTracker(IUnit to, RelationshipType relType)
		{
			Unit = to;
			RelationshipType = relType;
		}
	}
}