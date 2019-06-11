using System;
using Liaison.Data.Sql.Edmx;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liaison.BLL.Models.Test.Models.Unit
{
    [TestClass]
    public class AirSquadronTest
    {
        [TestMethod]
        public void MarineAviationLogisticsSquadron()
        {
            var sql = new Liaison.Data.Sql.Edmx.Unit
            {
                UnitId = 9000,
                Number = 31,
                UseOrdinal = false,
                NickName = null,
                LegacyMissionName  =null,
                MissionName = "Marine Aviation Logistics",
                UniqueName = null,
                CommandName = null,
                UnitTypeVariant = null,
                ServiceIdx = 4,
                ServiceTypeIdx = 1,
                TerritorialDesignation = null,
                UnitGuid = new Guid(),
                RankSymbol = "@",
                Rank = new Rank {Symbol = "@"},
                AdminCorp = new AdminCorp() {AdminCorpsId = 76, UnitDisplayName = "RMAA"},
                CanHide = false,
                Decommissioned = null
            };

            var group = new Liaison.BLL.Models.Unit.AirSquadron(sql);

            Assert.AreEqual("No. 31 Marine Aviation Logistics Sqn., RMAA",
                group.GetName());
        }

    }
}