using System;
using Liaison.Data.Sql.Edmx;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liaison.BLL.Tests.Models.Unit
{
    
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
}