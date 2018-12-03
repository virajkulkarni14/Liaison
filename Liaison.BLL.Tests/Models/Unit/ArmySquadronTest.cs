using System;
using Liaison.Data.Sql.Edmx;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liaison.BLL.Tests.Models.Unit
{
    [TestClass]
    public class ArmySquadronTest
    {
        [TestMethod]
        public void VolCloseSupportSquadron()
        {
            var sql = new Liaison.Data.Sql.Edmx.Unit
            {
                UnitId = 3680,
                Number = 195,
                UseOrdinal = false,
                NickName = null,
                LegacyMissionName = null,
                MissionName = "Close Support",
                UniqueName = null,
                CommandName = null,
                UnitTypeVariant = null,
                ServiceIdx = 2,
                ServiceTypeIdx = 4,
                TerritorialDesignation = "NE",
                UnitGuid = new Guid("fcb8b858-df02-4684-b659-c23f3cd5d2c3"),
                RankSymbol = "|",
                Rank = new Rank { Symbol = "|" },
                AdminCorp = new AdminCorp { AdminCorpsId = 151, UnitDisplayName = "" },
                CanHide = true,
                Decommissioned = null
            };
            var group = new Liaison.BLL.Models.Unit.ArmySquadron(sql);

            Assert.AreEqual("195 (V) (NE) Close Support Sqn.",
                group.GetName());


        }
    }
}