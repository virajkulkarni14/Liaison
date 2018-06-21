using System;

namespace Liaison.BLL.Models.Unit
{
    public class DetachmentBll : AUnit, IUnit
    {
        public string GetAdminCorps()
        {
            return "";
        }
        public DetachmentBll(Data.Sql.Edmx.Unit sqlUnit)
        {
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public string PrintTree()
        {
            throw new NotImplementedException();
        }

        public int GetRankLevel()
        {
            throw new NotImplementedException();
        }

        public string GetRankStar()
        {
            throw new NotImplementedException();
        }

        public string GetIndexes()
        {
            throw new NotImplementedException();
        }

        public string GetEquipment()
        {
            throw new NotImplementedException();
        }

        public bool IsTaskForce => false;
    }
}