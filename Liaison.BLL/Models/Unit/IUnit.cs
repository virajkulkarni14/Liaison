using System.Collections.Generic;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{

    public interface IUnit
    {
        string GetName();
        IEnumerable<RelationshipTracker> GetRelationships();
        string PrintTree();
        int GetRankLevel();
        string GetRankStar();
        string GetBase();
        string GetMission();
        string GetIndexes();
        string GetSortString();
        int GetId();
        string GetEquipment();
        bool IsTaskForce { get; }
        bool IsDecommissioned();

        //IEnumerable<IUnit> GetParents(int unitId, HigherHqType type);
        IEnumerable<RelationshipTracker> GetParents(int unitId, HigherHqType type);
        // object GetParents();

        bool GetIsHostUnit();
        string GetAdminCorps();
        
    }
}
