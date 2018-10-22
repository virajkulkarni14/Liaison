using System;
using System.Collections.Generic;
using Liaison.Data.Sql.Edmx;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liaison.BLL.Tests.Models.Unit
{
    [TestClass]
    public class TwoBarTest
    {
        [TestMethod]
        public void TestMethod1()
        {

        }
    }

    [TestClass]
    public class BattalionTest
    {
        [TestMethod]
        public void BattalionIntCorps()
        {
            var sql = new Liaison.Data.Sql.Edmx.Unit
            {
                UnitId = 2318,
                Number = 100,
                UseOrdinal = true,
                NickName = null,
                LegacyMissionName = null,
                MissionName = "Military Intelligence",
                UniqueName = "Special Forces",
                CommandName = null,
                UnitTypeVariant = null,
                ServiceIdx = 2,
                ServiceTypeIdx = 1,
                TerritorialDesignation = "Nigeria",
                UnitGuid = new Guid("1720b516-7fb2-47c8-b245-b9dabb6addad"),
                RankSymbol = "@",
                Rank = new Rank { Symbol = "@" },
                AdminCorp = new AdminCorp { AdminCorpsId = 120,UnitDisplayName = "IC"},
                CanHide = false,
                Decommissioned = null
            };
            var division = new Liaison.BLL.Models.Unit.Battalion(sql);

            Assert.AreEqual("100th (Special Forces) Military Intelligence Bn., IC",
                division.GetName());

        }
    }
    [TestClass]
    public class DivisionTest
    {
        public void SpecForTest()
        {
            var sql = new Liaison.Data.Sql.Edmx.Unit
            {
                UnitId = 2319,
                Number = 1,
                UseOrdinal = true,
                NickName = null,
                LegacyMissionName = null,
                MissionName = "Special Forces",
                UniqueName = null,
                CommandName = null,
                UnitTypeVariant = null,
                ServiceIdx = 2,
                ServiceTypeIdx = 1,
                TerritorialDesignation = null,
                UnitGuid = new Guid("f6c04887-f35d-4a23-8cbb-a9c1c345178a"),
                RankSymbol = ")",
                Rank = new Rank { Symbol = ")" },
                AdminCorp = new AdminCorp { AdminCorpsId = 112 },
                CanHide = false,
                Decommissioned = null
            };
            var division = new Liaison.BLL.Models.Unit.Division(sql);

            Assert.AreEqual("1st Special Forces Command (Airborne)",
                division.GetName());


        }
        [TestMethod]
        public void MechInfMilDistrictTest()
        {
            var sql = new Liaison.Data.Sql.Edmx.Unit
            {
                UnitId = 2244,
                Number = 1,
                UseOrdinal = true,
                NickName = null,
                LegacyMissionName = null,
                MissionName = "Mechanised Infantry",
                UniqueName = null,
                CommandName = "Military District of North West Nigeria",
                UnitTypeVariant = null,
                ServiceIdx = 2,
                ServiceTypeIdx = 1,
                TerritorialDesignation = "Nigeria",
                UnitGuid = new Guid("4c621d2b-dbc8-4f48-a5b6-22a7c627eb40"),
                RankSymbol = ")",
                Rank = new Rank { Symbol = ")"},
                AdminCorp=new AdminCorp{AdminCorpsId = 32},
                CanHide = false,
                Decommissioned = null
            };
            var division = new Liaison.BLL.Models.Unit.Division(sql);

            Assert.AreEqual("Military District of North West Nigeria / 1st (Nigeria) Mechanised Infantry Division",
                division.GetName());

        }
    }
}
