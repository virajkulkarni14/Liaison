using System;
using Liaison.Data.Sql.Edmx;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liaison.BLL.Models.Test.Models.Unit
{
    [TestClass]
    public class JointGroupTest
    {
        [TestMethod]
        public void ReservePsyOpsGroup()
        {
            var sql = new Liaison.Data.Sql.Edmx.Unit
            {
                UnitId = 3561,
                Number = 2,
                UseOrdinal = true,
                NickName = null,
                LegacyMissionName = null,
                MissionName = "Psychological Operations",
                UniqueName = null,
                CommandName = null,
                UnitTypeVariant = null,
                ServiceIdx = 2,
                ServiceTypeIdx = 3,
                TerritorialDesignation = null,
                UnitGuid = new Guid("13fd61a1-89e9-4e97-9cf2-642258f495c5"),
                RankSymbol = "/",
                Rank = new Rank { Symbol = "/" },
                AdminCorp = new AdminCorp { AdminCorpsId = 170, UnitDisplayName = "CAMG" },
                CanHide = false,
                Decommissioned = null
            };
            var group = new Liaison.BLL.Models.Unit.JointGroup(sql);

            Assert.AreEqual("2nd (R) Psychological Operations Group, CAMG",
                group.GetName());

        }
        [TestMethod]
        public void AirbornePsyOpsGroup()
        {
            var sql = new Liaison.Data.Sql.Edmx.Unit
            {
                UnitId = 3559,
                Number = 4,
                UseOrdinal = true,
                NickName = null,
                LegacyMissionName = null,
                MissionName = "Psychological Operations",
                UniqueName = null,
                CommandName = null,
                UnitTypeVariant = "Airborne",
                ServiceIdx = 2,
                ServiceTypeIdx = 1,
                TerritorialDesignation = null,
                UnitGuid = new Guid("77e013c6-989d-472b-92f8-c10fc40ccd5e"),
                RankSymbol = "/",
                Rank = new Rank { Symbol = "/" },
                AdminCorp = new AdminCorp { AdminCorpsId = 170, UnitDisplayName = "CAMG" },
                CanHide = false,
                Decommissioned = null
            };
            var group = new Liaison.BLL.Models.Unit.JointGroup(sql);

            Assert.AreEqual("4th Psychological Operations Group (Airborne), CAMG",
                group.GetName());

        }
    }
}
