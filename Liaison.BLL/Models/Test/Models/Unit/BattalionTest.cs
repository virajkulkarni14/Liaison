using System;
using Liaison.Data.Sql.Edmx;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liaison.BLL.Models.Test.Models.Unit
{
    
    [TestClass]
    public class BattalionTest
    {
        [TestMethod]
        public void BattalionMarineIntSupt()
        {
            var sql = new Liaison.Data.Sql.Edmx.Unit
            {
                UnitId = 9176,
                Number = null,
                UseOrdinal = false,
                NickName = null,
                LegacyMissionName = null,
                MissionName = "Intelligence Support",
                UniqueName = null,
                CommandName = null,
                UnitTypeVariant = null,
                ServiceIdx = 4,
                ServiceTypeIdx = 3,
                TerritorialDesignation = null,
                //UnitGuid = new Guid("70a377a7-6172-4c29-8a51-e8bead1d0cb4"),
                RankSymbol = "@",
                Rank = new Rank { Symbol = "@" },
                AdminCorp = new AdminCorp { AdminCorpsId = 3281, UnitDisplayName = "RMR" },
                CanHide = false,
                Decommissioned = null,
                //Language = "nl-be"
            };
            var isb = new BLL.Models.Unit.Battalion(sql);

            Assert.AreEqual("Intelligence Support Bn. (R), RMR",
                isb.GetName());

        }
        [TestMethod]
        public void BattalionParaBelgiandutch()
        {
            var sql = new Liaison.Data.Sql.Edmx.Unit
            {                          
                UnitId = 5534,
                Number = 1,
                UseOrdinal = true,
                NickName = null,
                LegacyMissionName = null,
                MissionName = "parachutisten",
                UniqueName = null,
                CommandName = null,
                UnitTypeVariant = null,
                ServiceIdx = 2,
                ServiceTypeIdx = 1,
                TerritorialDesignation = null,
                UnitGuid = new Guid("70a377a7-6172-4c29-8a51-e8bead1d0cb4"),
                RankSymbol = "@",
                Rank = new Rank { Symbol = "@" },
                AdminCorp = new AdminCorp { AdminCorpsId = 1192, UnitDisplayName = "Rgt. Para-Commando" },
                CanHide = false,
                Decommissioned = null,
                Language = "nl-be"
            };
            var division = new Liaison.BLL.Models.Unit.Battalion(sql);

            Assert.AreEqual("1ste bataljon parachutisten, Rgt. Para-Commando",
                division.GetName());

        }
        [TestMethod]
        public void BattalionSaka()
        {
            var sql = new Liaison.Data.Sql.Edmx.Unit
            {                        
                UnitId = 3371,
                Number = 236,
                UseOrdinal = true,
                NickName = null,
                LegacyMissionName = null,
                MissionName = "Commando",
                UniqueName = null,
                CommandName = null,
                UnitTypeVariant = null,
                ServiceIdx = 2,
                ServiceTypeIdx = 1,
                TerritorialDesignation = null,
                UnitGuid = new Guid("9549962c-1311-434c-a640-baf52fb12df6"),
                RankSymbol = "@",
                Rank = new Rank { Symbol = "@" },
                AdminCorp = new AdminCorp { AdminCorpsId = 169, UnitDisplayName = "SAKA" },
                CanHide = false,
                Decommissioned = null
            };
            var division = new Liaison.BLL.Models.Unit.Battalion(sql);

            Assert.AreEqual("236th Commando Bn., SAKA",
                division.GetName());

        }
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