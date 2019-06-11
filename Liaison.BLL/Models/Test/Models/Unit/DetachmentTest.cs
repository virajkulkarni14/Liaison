using System;
using Liaison.Data.Sql.Edmx;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liaison.BLL.Tests.Models.Unit
{
    [TestClass]
    public class DetachmentTest
    {
        [TestMethod]
        public void HHD()
        {
            var sql = new Liaison.Data.Sql.Edmx.Unit
            {
                UnitId = 3327,
                Number = null,
                UseOrdinal = false,
                Letter = null,
                NickName = null,
                LegacyMissionName = null,
                MissionName = "Headquarters & Headquarters",
                UniqueName = null,
                CommandName = "1st Bn.",
                UnitTypeVariant = null,
                ServiceIdx = 2,
                ServiceTypeIdx = 4,
                TerritorialDesignation = "UT",
                UnitGuid = new Guid("0813ecbc-2ca4-4225-8ebe-0418d36a1b22"),
                RankSymbol = "?",
                Rank = new Rank {Symbol = "?"},
                AdminCorp = new AdminCorp {AdminCorpsId = 161, UnitDisplayName = "19th Special Forces Rgt."},
                CanHide = true,
                Decommissioned = null
            };

            var detachment = new Liaison.BLL.Models.Unit.DetachmentBll(sql);

            Assert.AreEqual("HHQ Det. (V) (UT), 1st Bn., 19th Special Forces Rgt.",
                detachment.GetName());

        }

        public void DetHHD()
        {
            var sql = new Liaison.Data.Sql.Edmx.Unit
            {
                UnitId = 3350,
                Number = 1,
                UseOrdinal = false,
                Letter = null,
                NickName = null,
                LegacyMissionName = null,
                MissionName = null,
                UniqueName = null,
                CommandName = "HHD, ____1/___19 SFR",
                UnitTypeVariant = null,
                ServiceIdx = 2,
                ServiceTypeIdx = 4,
                TerritorialDesignation = "WA",
                UnitGuid = new Guid("702ff985-ae37-4181-9d22-89c62adb962d"),
                RankSymbol = "?",
                Rank = new Rank {Symbol = "?"},
                AdminCorp = new AdminCorp {AdminCorpsId = 161, UnitDisplayName = "19th Special Forces Rgt."},
                CanHide = true,
                Decommissioned = null
            };

            var detachment = new Liaison.BLL.Models.Unit.DetachmentBll(sql);

            Assert.AreEqual("Det. 1 (V) (WA), HHD, ____1/___19 SFR",
                detachment.GetName());

        }
        public void DetSFCoy()
        {
            var sql = new Liaison.Data.Sql.Edmx.Unit
            {
                UnitId = 3351,
                Number = 1,
                UseOrdinal = false,
                Letter = null,
                NickName = null,
                LegacyMissionName = null,
                MissionName = null,
                UniqueName = null,
                CommandName = "A Coy., 1st Bn., 19 SF Rgt.",
                UnitTypeVariant = null,
                ServiceIdx = 2,
                ServiceTypeIdx = 4,
                TerritorialDesignation = "WA",
                UnitGuid = new Guid("fe702a02-afc0-4fdc-856c-1a6bc2eac964"),
                RankSymbol = "?",
                Rank = new Rank { Symbol = "?" },
                AdminCorp = new AdminCorp { AdminCorpsId = 161, UnitDisplayName = "19th Special Forces Rgt." },
                CanHide = true,
                Decommissioned = null
            };

            var detachment = new Liaison.BLL.Models.Unit.DetachmentBll(sql);

            Assert.AreEqual("Det. 1 (V) (WA), A Coy., 1st Bn., 19 SF Rgt.",
                detachment.GetName());

        }
        [TestMethod]
        public void ChemicalDet()
        {
            var sql = new Liaison.Data.Sql.Edmx.Unit
            {
                UnitId = 3347,
                Number = 190,
                UseOrdinal = true,
                Letter = null,
                NickName = null,
                LegacyMissionName = null,
                MissionName = "Chemical",
                UniqueName = null,
                CommandName = null,
                UnitTypeVariant = null,
                ServiceIdx = 2,
                ServiceTypeIdx = 4,
                TerritorialDesignation = "MT",
                UnitGuid = new Guid("1d6d89b1-eaec-4b36-aef9-03ee9cd3381f"),
                RankSymbol = "?",
                Rank = new Rank {Symbol = "?"},
                AdminCorp = new AdminCorp {AdminCorpsId = 163, UnitDisplayName = "CHEMC"},
                CanHide = true,
                Decommissioned = null
            };

            var detachment = new Liaison.BLL.Models.Unit.DetachmentBll(sql);

            Assert.AreEqual("190th (V) (MT) Chemical Det., CHEMC",
                detachment.GetName());


        }
    }
}
