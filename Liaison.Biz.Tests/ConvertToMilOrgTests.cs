using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using Liaison.Biz.Objects;
using System.Xml.Serialization;
using System.Collections.Generic;
using Liaison.Biz.MilOrgs;
using System.IO;

namespace Liaison.Biz.Tests
{
    [TestClass]
    public class ConvertToMilOrgTests
    {
        [TestMethod]
        public void ConvertToCommand_ArmyMaterial()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.CommandString.armyMaterialCommand;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);

            var army1 = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(army1, typeof(CommandOrg));
            Assert.IsNull(army1.Number);
            Assert.IsFalse(army1.UseOrdinal);
            Assert.AreEqual("Materiel", army1.Mission);
            Assert.AreEqual("Army Materiel Command", army1.GetFullName());
            Assert.AreEqual("us/army/amc", army1.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/amc", army1.CurrentOpsUrl);
            Assert.AreEqual("https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgQU1D.png", army1.CurrentOpsLogo);

            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, army1.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Command, army1.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, army1.ServiceTypeIdx);
            Assert.AreEqual(null, army1.USState);

            Assert.AreEqual(1, army1.Bases.Count);

            Assert.AreEqual("us/al/redstone-ars", army1.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Redstone Arsenal", army1.Bases[0].Name);
            Assert.AreEqual("Alabama, United States", army1.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/al/redstone-ars", army1.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, army1.Bases[0].DateFrom);
            Assert.AreEqual(null, army1.Bases[0].DateUntil);
            Assert.AreEqual(false, army1.Bases[0].IsDeployment);
            Assert.AreEqual(true, army1.Bases[0].IsCurrent);

            Assert.AreEqual(1, army1.HigherHqs.Count);

            Assert.AreEqual("us/army/doa", army1.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, army1.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, army1.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, army1.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Unknown, army1.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, army1.ShortForm.Count);

            Assert.AreEqual("AMC", army1.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, army1.ShortForm[0].Type);

            Assert.AreEqual("AMC", army1.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, army1.ShortForm[1].Type);

            Assert.AreEqual("AMC", army1.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, army1.ShortForm[2].Type);

            Assert.AreEqual(2, army1.ChildOrgs.Count);

            Assert.AreEqual("us/army/asc", army1.ChildOrgs[0].CurrentOpsRef);
            Assert.AreEqual("Army Sustainment Command", army1.ChildOrgs[0].Name);
            Assert.IsFalse(army1.ChildOrgs[0].IsIndirect);

            Assert.AreEqual("us/army/us-army-contracting-cmd", army1.ChildOrgs[1].CurrentOpsRef);
            Assert.AreEqual("Army Contracting Command", army1.ChildOrgs[1].Name);
            Assert.IsFalse(army1.ChildOrgs[1].IsIndirect);
            #endregion
        }
        [TestMethod]
        public void ConvertToCommand_87ARSupt()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.CommandString.cmd87aressupt;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);


            var hschhqbn = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(hschhqbn, typeof(CommandOrg));
            Assert.AreEqual(87, hschhqbn.Number);
            Assert.IsTrue(hschhqbn.UseOrdinal);
            Assert.AreEqual("Army Reserve Support", hschhqbn.Mission);
            Assert.AreEqual("___87th Army Reserve Support Command", hschhqbn.GetFullName());
            Assert.AreEqual("us/army/87-arsc", hschhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/87-arsc", hschhqbn.CurrentOpsUrl);
            Assert.AreEqual("https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgQVJTQyAwMDg3.png", hschhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, hschhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Command, hschhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Reserve, hschhqbn.ServiceTypeIdx);
            Assert.AreEqual(null, hschhqbn.USState);

            Assert.AreEqual(1, hschhqbn.Bases.Count);

            Assert.AreEqual("us/al/horace-b-hanson-usarc", hschhqbn.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Horace B. Hanson Army Reserve Centre", hschhqbn.Bases[0].Name);
            Assert.AreEqual(null, hschhqbn.Bases[0].ParentBase);
            Assert.AreEqual("Birmingham, Alabama, United States", hschhqbn.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/al/horace-b-hanson-usarc", hschhqbn.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, hschhqbn.Bases[0].DateFrom);
            Assert.AreEqual(null, hschhqbn.Bases[0].DateUntil);
            Assert.AreEqual(false, hschhqbn.Bases[0].IsDeployment);
            Assert.AreEqual(true, hschhqbn.Bases[0].IsCurrent);

            Assert.AreEqual(2, hschhqbn.HigherHqs.Count);

            Assert.AreEqual("us/army/1-army/div-east", hschhqbn.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, hschhqbn.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Unknown, hschhqbn.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual("us/army/usarc", hschhqbn.HigherHqs[1].CurrentOpsRef);
            Assert.AreEqual(null, hschhqbn.HigherHqs[1].DateFrom);
            Assert.AreEqual(null, hschhqbn.HigherHqs[1].DateUntil);
            Assert.AreEqual(false, hschhqbn.HigherHqs[1].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Unknown, hschhqbn.HigherHqs[1].CommandRelationshipType);

            Assert.AreEqual(3, hschhqbn.ShortForm.Count);

            Assert.AreEqual("___87 ARSC", hschhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, hschhqbn.ShortForm[0].Type);

            Assert.AreEqual("G*SUPT___87", hschhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, hschhqbn.ShortForm[1].Type);

            Assert.AreEqual("___87 ARSC", hschhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, hschhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToArmy_1()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.FieldArmyString.army_1;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);

            var army1 = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(army1, typeof(FieldArmyOrg));
            Assert.AreEqual(1, army1.Number);
            Assert.IsTrue(army1.UseOrdinal);
            Assert.AreEqual("", army1.Mission);
            Assert.AreEqual("First Army", army1.GetFullName());
            Assert.AreEqual("us/army/1-army", army1.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/1-army", army1.CurrentOpsUrl);
            Assert.AreEqual("https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgQVJNWSAwMDAx.png", army1.CurrentOpsLogo);

            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, army1.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.FieldArmy, army1.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, army1.ServiceTypeIdx);
            Assert.AreEqual(null, army1.USState);

            Assert.AreEqual(1, army1.Bases.Count);

            Assert.AreEqual("us/il/rock-island-ars", army1.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Rock Island Arsenal", army1.Bases[0].Name);
            Assert.AreEqual("Rock Island, Illinois, United States", army1.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/il/rock-island-ars", army1.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, army1.Bases[0].DateFrom);
            Assert.AreEqual(null, army1.Bases[0].DateUntil);
            Assert.AreEqual(false, army1.Bases[0].IsDeployment);
            Assert.AreEqual(true, army1.Bases[0].IsCurrent);

            Assert.AreEqual(1, army1.HigherHqs.Count);

            Assert.AreEqual("us/army/forscom", army1.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, army1.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, army1.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, army1.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Assigned, army1.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, army1.ShortForm.Count);

            Assert.AreEqual("____1 ARMY", army1.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, army1.ShortForm[0].Type);

            Assert.AreEqual("G&____1", army1.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, army1.ShortForm[1].Type);

            Assert.AreEqual("____1 ARMY", army1.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, army1.ShortForm[2].Type);

            Assert.AreEqual(2, army1.ChildOrgs.Count);

            Assert.AreEqual("us/army/1-army/div-east", army1.ChildOrgs[0].CurrentOpsRef);
            Assert.AreEqual("Division East, 1st Army", army1.ChildOrgs[0].Name);
            Assert.IsFalse(army1.ChildOrgs[0].IsIndirect);

            Assert.AreEqual("us/army/1-army/div-west", army1.ChildOrgs[1].CurrentOpsRef);
            Assert.AreEqual("Division West, 1st Army", army1.ChildOrgs[1].Name);
            Assert.IsFalse(army1.ChildOrgs[1].IsIndirect);
            #endregion
        }
        [TestMethod]
        public void ConvertToDivision_1id()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Liaison.Biz.Tests.Strings.DivisionString.infantrydivision_1id;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);


            var division = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(division, typeof(DivisionOrg));
            Assert.AreEqual(1, division.Number);
            Assert.IsTrue(division.UseOrdinal);
            Assert.AreEqual("Infantry", division.Mission);
            Assert.AreEqual("____1st Infantry Division", division.GetFullName());
            Assert.AreEqual("us/army/1-id", division.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/1-id", division.CurrentOpsUrl);
            Assert.AreEqual("https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgSUQgMDAwMQ.png", division.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, division.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Division, division.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, division.ServiceTypeIdx);
            Assert.AreEqual(null, division.USState);

            Assert.AreEqual(2, division.Bases.Count);

            Assert.AreEqual("us/ks/fort-riley", division.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Fort Riley", division.Bases[0].Name);
            Assert.AreEqual("Junction City, Kansas, United States", division.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/ks/fort-riley", division.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(2006, division.Bases[0].DateFrom);
            Assert.AreEqual(null, division.Bases[0].DateUntil);
            Assert.AreEqual(false, division.Bases[0].IsDeployment);
            Assert.AreEqual(true, division.Bases[0].IsCurrent);

            Assert.AreEqual("de/by/leighton-bks", division.Bases[1].CurrentOpsBaseRef);
            Assert.AreEqual("Leighton Barracks", division.Bases[1].Name);
            Assert.AreEqual("Würzburg, Bavaria, Germany", division.Bases[1].Location);
            Assert.AreEqual("https://currentops.com/installations/de/by/leighton-bks", division.Bases[1].CurrentOpsUrl);
            Assert.AreEqual(null, division.Bases[1].DateFrom);
            Assert.AreEqual(2006, division.Bases[1].DateUntil);
            Assert.AreEqual(false, division.Bases[1].IsDeployment);
            Assert.AreEqual(false, division.Bases[1].IsCurrent);

            Assert.AreEqual(3, division.HigherHqs.Count);

            Assert.AreEqual("us/army/usaraf", division.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, division.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, division.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, division.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Alligned, division.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual("us/army/forscom", division.HigherHqs[1].CurrentOpsRef);
            Assert.AreEqual(null, division.HigherHqs[1].DateFrom);
            Assert.AreEqual(null, division.HigherHqs[1].DateUntil);
            Assert.AreEqual(true, division.HigherHqs[1].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Assigned, division.HigherHqs[1].CommandRelationshipType);

            Assert.AreEqual("us/army/v-corps", division.HigherHqs[2].CurrentOpsRef);
            Assert.AreEqual(null, division.HigherHqs[2].DateFrom);
            Assert.AreEqual(2006, division.HigherHqs[2].DateUntil);
            Assert.AreEqual(false, division.HigherHqs[2].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Unknown, division.HigherHqs[2].CommandRelationshipType);

            Assert.AreEqual(3, division.ShortForm.Count);

            Assert.AreEqual("____1 INF. DIV.", division.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, division.ShortForm[0].Type);

            Assert.AreEqual("G)INF____1", division.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, division.ShortForm[1].Type);

            Assert.AreEqual("____1 ID", division.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, division.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToDivision_38id()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Liaison.Biz.Tests.Strings.DivisionString.infantrydivision_38id;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);


            var division = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(division, typeof(DivisionOrg));
            Assert.AreEqual(38, division.Number);
            Assert.IsTrue(division.UseOrdinal);
            Assert.AreEqual("Infantry", division.Mission);
            Assert.AreEqual("___38th (V) (IN) Infantry Division", division.GetFullName());
            Assert.AreEqual("us/army/38-id", division.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/38-id", division.CurrentOpsUrl);
            Assert.AreEqual("https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgSUQgMDAzOA.png", division.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, division.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Division, division.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Volunteer, division.ServiceTypeIdx);
            Assert.AreEqual("IN", division.USState);

            Assert.AreEqual(1, division.Bases.Count);

            Assert.AreEqual("us/in/division-armory", division.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Division Armory", division.Bases[0].Name);
            Assert.AreEqual("Indianapolis, Indiana, United States", division.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/in/division-armory", division.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, division.Bases[0].DateFrom);
            Assert.AreEqual(null, division.Bases[0].DateUntil);
            Assert.AreEqual(false, division.Bases[0].IsDeployment);
            Assert.AreEqual(true, division.Bases[0].IsCurrent);
            
            Assert.AreEqual(1, division.HigherHqs.Count);

            Assert.AreEqual("us/army/jfhq-in/arng-elt", division.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, division.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, division.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, division.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Unknown, division.HigherHqs[0].CommandRelationshipType);        

            Assert.AreEqual(3, division.ShortForm.Count);

            Assert.AreEqual("___38 (V) INF. DIV.", division.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, division.ShortForm[0].Type);

            Assert.AreEqual("G)INF___38", division.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, division.ShortForm[1].Type);

            Assert.AreEqual("___38 ID", division.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, division.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToDivisionArmy_1West()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.DivisionString.army_1_div_west;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);

            var army1 = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(army1, typeof(DivisionOrg));
            Assert.IsNull(army1.Number);
            Assert.IsFalse(army1.UseOrdinal);
            Assert.AreEqual("", army1.Mission);
            Assert.AreEqual("Division West, First Army", army1.GetFullName());
            Assert.AreEqual("us/army/1-army/div-west", army1.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/1-army/div-west", army1.CurrentOpsUrl);
            Assert.AreEqual("https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgQVJNWSAwMDAx.png", army1.CurrentOpsLogo);

            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, army1.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Division, army1.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, army1.ServiceTypeIdx);

            Assert.AreEqual(1, army1.Bases.Count);

            Assert.AreEqual("us/tx/fort-hood", army1.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Fort Hood", army1.Bases[0].Name);
            Assert.AreEqual("Killeen, Texas, United States", army1.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/tx/fort-hood", army1.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, army1.Bases[0].DateFrom);
            Assert.AreEqual(null, army1.Bases[0].DateUntil);
            Assert.AreEqual(false, army1.Bases[0].IsDeployment);
            Assert.AreEqual(true, army1.Bases[0].IsCurrent);

            Assert.AreEqual(1, army1.HigherHqs.Count);

            Assert.AreEqual("us/army/1-army", army1.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, army1.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, army1.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, army1.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Assigned, army1.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, army1.ShortForm.Count);

            Assert.AreEqual("DIV. WEST, ____1 ARMY", army1.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, army1.ShortForm[0].Type);

            Assert.AreEqual("G&____1)WEST", army1.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, army1.ShortForm[1].Type);

            Assert.AreEqual("W DIV, ____1 A", army1.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, army1.ShortForm[2].Type);

            Assert.AreEqual(8, army1.ChildOrgs.Count);

            Assert.AreEqual("us/army/1-army/div-west/hhd", army1.ChildOrgs[0].CurrentOpsRef);
            Assert.AreEqual("Headquarters and Headquarters Detachment, Division West, First Army", army1.ChildOrgs[0].Name);
            Assert.IsFalse(army1.ChildOrgs[0].IsIndirect);

            Assert.AreEqual("us/army/5-armored-bde", army1.ChildOrgs[1].CurrentOpsRef);
            Assert.AreEqual("5th Armored Brigade", army1.ChildOrgs[1].Name);
            Assert.IsFalse(army1.ChildOrgs[1].IsIndirect);

            Assert.AreEqual("us/army/120-in-bde", army1.ChildOrgs[2].CurrentOpsRef);
            Assert.AreEqual("120th Infantry Brigade", army1.ChildOrgs[2].Name);
            Assert.IsFalse(army1.ChildOrgs[2].IsIndirect);

            Assert.AreEqual("us/army/402-fa-bde", army1.ChildOrgs[5].CurrentOpsRef);
            Assert.AreEqual("402nd Field Artillery Brigade", army1.ChildOrgs[5].Name);
            Assert.IsFalse(army1.ChildOrgs[5].IsIndirect);

            Assert.AreEqual("us/army/166-avn-bde", army1.ChildOrgs[6].CurrentOpsRef);
            Assert.AreEqual("166th Aviation Brigade", army1.ChildOrgs[6].Name);
            Assert.IsFalse(army1.ChildOrgs[5].IsIndirect);

            Assert.AreEqual("us/army/85-arsc", army1.ChildOrgs[7].CurrentOpsRef);
            Assert.AreEqual("85th Army Reserve Support Command", army1.ChildOrgs[7].Name);
            Assert.IsFalse(army1.ChildOrgs[7].IsIndirect);
            #endregion

        }
        [TestMethod]
        public void ConvertToDivisionArmy_1East()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.DivisionString.army_1_div_east;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);

            var army1 = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(army1, typeof(DivisionOrg));
            Assert.IsNull(army1.Number);
            Assert.IsFalse(army1.UseOrdinal);
            Assert.AreEqual("", army1.Mission);
            Assert.AreEqual("Division East, First Army", army1.GetFullName());
            Assert.AreEqual("us/army/1-army/div-east", army1.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/1-army/div-east", army1.CurrentOpsUrl);
            Assert.AreEqual("https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgQVJNWSAwMDAx.png", army1.CurrentOpsLogo);

            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, army1.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Division, army1.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, army1.ServiceTypeIdx);
            Assert.AreEqual(null, army1.USState);

            Assert.AreEqual(1, army1.Bases.Count);

            Assert.AreEqual("us/md/fort-george-g-meade", army1.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Fort George G. Meade", army1.Bases[0].Name);
            Assert.AreEqual("Maryland, United States", army1.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/md/fort-george-g-meade", army1.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, army1.Bases[0].DateFrom);
            Assert.AreEqual(null, army1.Bases[0].DateUntil);
            Assert.AreEqual(false, army1.Bases[0].IsDeployment);
            Assert.AreEqual(true, army1.Bases[0].IsCurrent);

            Assert.AreEqual(1, army1.HigherHqs.Count);

            Assert.AreEqual("us/army/1-army", army1.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, army1.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, army1.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, army1.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Assigned, army1.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, army1.ShortForm.Count);

            Assert.AreEqual("DIV. EAST, ____1 ARMY", army1.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, army1.ShortForm[0].Type);

            Assert.AreEqual("G&____1)EAST", army1.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, army1.ShortForm[1].Type);

            Assert.AreEqual("E DIV, ____1 A", army1.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, army1.ShortForm[2].Type);

            Assert.AreEqual(10, army1.ChildOrgs.Count);

            Assert.AreEqual("us/army/1-army/div-east/hhd", army1.ChildOrgs[0].CurrentOpsRef);
            Assert.AreEqual("Headquarters and Headquarters Detachment, Division East, First Army", army1.ChildOrgs[0].Name);
            Assert.IsFalse(army1.ChildOrgs[0].IsIndirect);

            Assert.AreEqual("us/army/4-cav-bde", army1.ChildOrgs[1].CurrentOpsRef);
            Assert.AreEqual("4th Cavalry Brigade", army1.ChildOrgs[1].Name);
            Assert.IsFalse(army1.ChildOrgs[1].IsIndirect);

            Assert.AreEqual("us/army/177-armored-bde", army1.ChildOrgs[2].CurrentOpsRef);
            Assert.AreEqual("177th Armored Brigade", army1.ChildOrgs[2].Name);
            Assert.IsFalse(army1.ChildOrgs[2].IsIndirect);

            Assert.AreEqual("us/army/174-in-bde", army1.ChildOrgs[5].CurrentOpsRef);
            Assert.AreEqual("174th Infantry Brigade", army1.ChildOrgs[5].Name);
            Assert.IsFalse(army1.ChildOrgs[5].IsIndirect);

            Assert.AreEqual("us/army/72-fa-bde", army1.ChildOrgs[8].CurrentOpsRef);
            Assert.AreEqual("72nd Field Artillery Brigade", army1.ChildOrgs[8].Name);
            Assert.IsFalse(army1.ChildOrgs[8].IsIndirect);

            Assert.AreEqual("us/army/87-arsc", army1.ChildOrgs[9].CurrentOpsRef);
            Assert.AreEqual("87th Army Reserve Support Command", army1.ChildOrgs[9].Name);
            Assert.IsFalse(army1.ChildOrgs[9].IsIndirect);
            #endregion
        }
        [TestMethod]
        public void ConvertToBattalionHHQ_1id()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Liaison.Biz.Tests.Strings.HHQBnString.headquartersAndHqBn_1id;          

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);


            var hhqbn = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(hhqbn, typeof(BattalionOrg));
            Assert.AreEqual(null, hhqbn.Number);
            Assert.IsFalse(hhqbn.UseOrdinal);
            Assert.AreEqual("Headquarters and Headquarters", hhqbn.Mission);
            Assert.AreEqual("Headquarters and Headquarters Bn., ____1st Inf. Div.", hhqbn.GetFullName());
            Assert.AreEqual("us/army/1-id/hhbn", hhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/1-id/hhbn", hhqbn.CurrentOpsUrl);
            Assert.AreEqual("", hhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, hhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Battalion, hhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, hhqbn.ServiceTypeIdx);
            Assert.AreEqual(null, hhqbn.USState);

            Assert.AreEqual(1, hhqbn.Bases.Count);

            Assert.AreEqual("us/ks/fort-riley", hhqbn.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Fort Riley", hhqbn.Bases[0].Name);
            Assert.AreEqual("Junction City, Kansas, United States", hhqbn.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/ks/fort-riley", hhqbn.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, hhqbn.Bases[0].DateFrom);
            Assert.AreEqual(null, hhqbn.Bases[0].DateUntil);
            Assert.AreEqual(false, hhqbn.Bases[0].IsDeployment);
            Assert.AreEqual(true, hhqbn.Bases[0].IsCurrent);         

            Assert.AreEqual(1, hhqbn.HigherHqs.Count);

            Assert.AreEqual("us/army/1-id", hhqbn.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, hhqbn.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, hhqbn.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, hhqbn.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Organic, hhqbn.HigherHqs[0].CommandRelationshipType);            

            Assert.AreEqual(3, hhqbn.ShortForm.Count);

            Assert.AreEqual("HHQ BN., ____1 INF. DIV.", hhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, hhqbn.ShortForm[0].Type);

            Assert.AreEqual("G)INF____1@!", hhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, hhqbn.ShortForm[1].Type);

            Assert.AreEqual("HHB/____1 ID", hhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, hhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToBattalionHHQ_38id()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Liaison.Biz.Tests.Strings.HHQBnString.headquartersAndHqBn_38id;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);


            var hhqbn = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(hhqbn, typeof(BattalionOrg));
            Assert.AreEqual(null, hhqbn.Number);
            Assert.IsFalse(hhqbn.UseOrdinal);
            Assert.AreEqual("Headquarters and Headquarters", hhqbn.Mission);
            Assert.AreEqual("Headquarters and Headquarters Bn., ___38th (V) (IN) Inf. Div.", hhqbn.GetFullName());
            Assert.AreEqual("us/army/38-id/hhbn", hhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/38-id/hhbn", hhqbn.CurrentOpsUrl);
            Assert.AreEqual("", hhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, hhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Battalion, hhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Volunteer, hhqbn.ServiceTypeIdx);
            Assert.AreEqual("IN", hhqbn.USState);

            Assert.AreEqual(1, hhqbn.Bases.Count);

            Assert.AreEqual("us/in/division-armory", hhqbn.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Division Armory", hhqbn.Bases[0].Name);
            Assert.AreEqual("Indianapolis, Indiana, United States", hhqbn.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/in/division-armory", hhqbn.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, hhqbn.Bases[0].DateFrom);
            Assert.AreEqual(null, hhqbn.Bases[0].DateUntil);
            Assert.AreEqual(false, hhqbn.Bases[0].IsDeployment);
            Assert.AreEqual(true, hhqbn.Bases[0].IsCurrent);

            Assert.AreEqual(1, hhqbn.HigherHqs.Count);

            Assert.AreEqual("us/army/38-id", hhqbn.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, hhqbn.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, hhqbn.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, hhqbn.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Organic, hhqbn.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, hhqbn.ShortForm.Count);

            Assert.AreEqual("HHQ BN., ___38 (V) INF. DIV.", hhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, hhqbn.ShortForm[0].Type);

            Assert.AreEqual("G)INF___38@!", hhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, hhqbn.ShortForm[1].Type);

            Assert.AreEqual("HHB/___38 ID", hhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, hhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToCompanyHSHHQBn_38id()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Liaison.Biz.Tests.Strings.HHQBnCompanyString.hscHeadquartersAndHqBn_38id;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);


            var hschhqbn = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(hschhqbn, typeof(CompanyOrg));
            Assert.AreEqual(null, hschhqbn.Number);
            Assert.IsFalse(hschhqbn.UseOrdinal);
            Assert.AreEqual("Headquarters and Support", hschhqbn.Mission);
            Assert.AreEqual("Headquarters and Support Coy., HHQ Bn., ___38th (V) (IN) Inf. Div.", hschhqbn.GetFullName());
            Assert.AreEqual("us/army/38-id/hhbn/hsc", hschhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/38-id/hhbn/hsc", hschhqbn.CurrentOpsUrl);
            Assert.AreEqual("", hschhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, hschhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Company, hschhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Volunteer, hschhqbn.ServiceTypeIdx);
            Assert.AreEqual("IN", hschhqbn.USState);

            Assert.AreEqual(1, hschhqbn.Bases.Count);

            Assert.AreEqual("us/in/division-armory", hschhqbn.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Division Armory", hschhqbn.Bases[0].Name);
            Assert.AreEqual("Indianapolis, Indiana, United States", hschhqbn.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/in/division-armory", hschhqbn.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, hschhqbn.Bases[0].DateFrom);
            Assert.AreEqual(null, hschhqbn.Bases[0].DateUntil);
            Assert.AreEqual(false, hschhqbn.Bases[0].IsDeployment);
            Assert.AreEqual(true, hschhqbn.Bases[0].IsCurrent);

            Assert.AreEqual(1, hschhqbn.HigherHqs.Count);

            Assert.AreEqual("us/army/38-id/hhbn", hschhqbn.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, hschhqbn.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Organic, hschhqbn.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, hschhqbn.ShortForm.Count);

            Assert.AreEqual("HQ & SUPT. COY., HHQ BN., ___38 (V) INF. DIV.", hschhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, hschhqbn.ShortForm[0].Type);

            Assert.AreEqual("G)INF___38@!|!", hschhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, hschhqbn.ShortForm[1].Type);

            Assert.AreEqual("HSC-HHB/___38 ID", hschhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, hschhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToCompanyHS_76ibct()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Liaison.Biz.Tests.Strings.HHQBnCompanyString.coyHeadquartersAndHq_76ibct;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);


            var hschhqbn = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(hschhqbn, typeof(CompanyOrg));
            Assert.AreEqual(null, hschhqbn.Number);
            Assert.IsFalse(hschhqbn.UseOrdinal);
            Assert.AreEqual("Headquarters and Headquarters", hschhqbn.Mission);
            Assert.AreEqual("Headquarters and Headquarters Coy., ___76th (V) (IN) Inf. BCT", hschhqbn.GetFullName());
            Assert.AreEqual("us/army/76-ibct/hhc", hschhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/76-ibct/hhc", hschhqbn.CurrentOpsUrl);
            Assert.AreEqual("", hschhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, hschhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Company, hschhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Volunteer, hschhqbn.ServiceTypeIdx);
            Assert.AreEqual("IN", hschhqbn.USState);

            Assert.AreEqual(1, hschhqbn.Bases.Count);

            Assert.AreEqual("us/in/lawrence-armory", hschhqbn.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Lawrence Armory", hschhqbn.Bases[0].Name);
            Assert.AreEqual("Lawrence, Indiana, United States", hschhqbn.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/in/lawrence-armory", hschhqbn.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, hschhqbn.Bases[0].DateFrom);
            Assert.AreEqual(null, hschhqbn.Bases[0].DateUntil);
            Assert.AreEqual(false, hschhqbn.Bases[0].IsDeployment);
            Assert.AreEqual(true, hschhqbn.Bases[0].IsCurrent);

            Assert.AreEqual(1, hschhqbn.HigherHqs.Count);

            Assert.AreEqual("us/army/76-ibct", hschhqbn.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, hschhqbn.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Organic, hschhqbn.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, hschhqbn.ShortForm.Count);

            Assert.AreEqual("HQ & HQ COY., ___76 (V) INF. BCT", hschhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, hschhqbn.ShortForm[0].Type);

            Assert.AreEqual("G*INF___76|!", hschhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, hschhqbn.ShortForm[1].Type);

            Assert.AreEqual("HHC-_____/___76 IBCT", hschhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, hschhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToSqdrnHH_177Armd()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.HHQBnCompanyString.coyHeadquartersAndHq_177ArmdBde;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);


            var hschhqbn = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(hschhqbn, typeof(Squadron_CavalryOrg));
            Assert.AreEqual(null, hschhqbn.Number);
            Assert.IsFalse(hschhqbn.UseOrdinal);
            Assert.AreEqual("Headquarters and Headquarters", hschhqbn.Mission);
            Assert.AreEqual("Headquarters and Headquarters Sqn., __177th Arm. Bde.", hschhqbn.GetFullName());
            Assert.AreEqual("us/army/177-armored-bde/hhc", hschhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/177-armored-bde/hhc", hschhqbn.CurrentOpsUrl);
            Assert.AreEqual("", hschhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, hschhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.SquadronCavalry, hschhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, hschhqbn.ServiceTypeIdx);
            Assert.AreEqual(null, hschhqbn.USState);

            Assert.AreEqual(1, hschhqbn.Bases.Count);

            Assert.AreEqual("us/ms/camp-shelby", hschhqbn.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Camp Shelby", hschhqbn.Bases[0].Name);
            Assert.AreEqual("Mississippi, United States", hschhqbn.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/ms/camp-shelby", hschhqbn.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, hschhqbn.Bases[0].DateFrom);
            Assert.AreEqual(null, hschhqbn.Bases[0].DateUntil);
            Assert.AreEqual(false, hschhqbn.Bases[0].IsDeployment);
            Assert.AreEqual(true, hschhqbn.Bases[0].IsCurrent);

            Assert.AreEqual(1, hschhqbn.HigherHqs.Count);

            Assert.AreEqual("us/army/177-armored-bde", hschhqbn.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, hschhqbn.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Organic, hschhqbn.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, hschhqbn.ShortForm.Count);

            Assert.AreEqual("HQ & HQ SQN., __177 ARM. BDE.", hschhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, hschhqbn.ShortForm[0].Type);

            Assert.AreEqual("G*ARM__177|!", hschhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, hschhqbn.ShortForm[1].Type);

            Assert.AreEqual("HHS-_____/__177 AB", hschhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, hschhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToSqdrnHH_157Inf()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.HHQBnCompanyString.coyHeadquartersAndHq_157InfBde;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);


            var hschhqbn = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(hschhqbn, typeof(CompanyOrg));
            Assert.AreEqual(null, hschhqbn.Number);
            Assert.IsFalse(hschhqbn.UseOrdinal);
            Assert.AreEqual("Headquarters and Headquarters", hschhqbn.Mission);
            Assert.AreEqual("Headquarters and Headquarters Coy., __157th Inf. Bde.", hschhqbn.GetFullName());
            Assert.AreEqual("us/army/157-in-bde/hhc", hschhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/157-in-bde/hhc", hschhqbn.CurrentOpsUrl);
            Assert.AreEqual("", hschhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, hschhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Company, hschhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, hschhqbn.ServiceTypeIdx);
            Assert.AreEqual(null, hschhqbn.USState);

            Assert.AreEqual(2, hschhqbn.Bases.Count);

            Assert.AreEqual("us/in/camp-atterbury", hschhqbn.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Camp Atterbury", hschhqbn.Bases[0].Name);
            Assert.AreEqual("Edinburgh, Indiana, United States", hschhqbn.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/in/camp-atterbury", hschhqbn.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(2011, hschhqbn.Bases[0].DateFrom);
            Assert.AreEqual(null, hschhqbn.Bases[0].DateUntil);
            Assert.AreEqual(false, hschhqbn.Bases[0].IsDeployment);
            Assert.AreEqual(true, hschhqbn.Bases[0].IsCurrent);

            Assert.AreEqual("us/sc/fort-jackson", hschhqbn.Bases[1].CurrentOpsBaseRef);
            Assert.AreEqual("Fort Jackson", hschhqbn.Bases[1].Name);
            Assert.AreEqual("Columbia, South Carolina, United States", hschhqbn.Bases[1].Location);
            Assert.AreEqual("https://currentops.com/installations/us/sc/fort-jackson", hschhqbn.Bases[1].CurrentOpsUrl);
            Assert.AreEqual(null, hschhqbn.Bases[1].DateFrom);
            Assert.AreEqual(2011, hschhqbn.Bases[1].DateUntil);
            Assert.AreEqual(false, hschhqbn.Bases[1].IsDeployment);
            Assert.AreEqual(false, hschhqbn.Bases[1].IsCurrent);

            Assert.AreEqual(1, hschhqbn.HigherHqs.Count);

            Assert.AreEqual("us/army/157-in-bde", hschhqbn.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, hschhqbn.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Organic, hschhqbn.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, hschhqbn.ShortForm.Count);

            Assert.AreEqual("HQ & HQ COY., __157 INF. BDE.", hschhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, hschhqbn.ShortForm[0].Type);

            Assert.AreEqual("G*INF__157|!", hschhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, hschhqbn.ShortForm[1].Type);

            Assert.AreEqual("HHC-_____/__157 IB", hschhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, hschhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToSqdrnHH_4Cav()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.TroopString.trpHeadquartersAndHq_4cav;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);


            var hschhqbn = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(hschhqbn, typeof(Squadron_CavalryOrg));
            Assert.AreEqual(null, hschhqbn.Number);
            Assert.IsFalse(hschhqbn.UseOrdinal);
            Assert.AreEqual("Headquarters and Headquarters", hschhqbn.Mission);
            Assert.AreEqual("Headquarters and Headquarters Sqn., ____4th Cav. Bde.", hschhqbn.GetFullName());
            Assert.AreEqual("us/army/4-cav-bde/hht", hschhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/4-cav-bde/hht", hschhqbn.CurrentOpsUrl);
            Assert.AreEqual("", hschhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, hschhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.SquadronCavalry, hschhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, hschhqbn.ServiceTypeIdx);
            Assert.AreEqual(null, hschhqbn.USState);

            Assert.AreEqual(1, hschhqbn.Bases.Count);

            Assert.AreEqual("us/ky/fort-knox", hschhqbn.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Fort Knox", hschhqbn.Bases[0].Name);
            Assert.AreEqual("Radcliff, Kentucky, United States", hschhqbn.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/ky/fort-knox", hschhqbn.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, hschhqbn.Bases[0].DateFrom);
            Assert.AreEqual(null, hschhqbn.Bases[0].DateUntil);
            Assert.AreEqual(false, hschhqbn.Bases[0].IsDeployment);
            Assert.AreEqual(true, hschhqbn.Bases[0].IsCurrent);

            Assert.AreEqual(1, hschhqbn.HigherHqs.Count);

            Assert.AreEqual("us/army/4-cav-bde", hschhqbn.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, hschhqbn.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Organic, hschhqbn.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, hschhqbn.ShortForm.Count);

            Assert.AreEqual("HQ & HQ SQN., ____4 CAV. BDE.", hschhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, hschhqbn.ShortForm[0].Type);

            Assert.AreEqual("G*CAV____4|!", hschhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, hschhqbn.ShortForm[1].Type);

            Assert.AreEqual("HHS-_____/____4 CB", hschhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, hschhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToBtyHH_72Arty()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.BatteryString.btyHqAndHq_72Art;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);


            var hschhqbn = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(hschhqbn, typeof(BatteryOrg));
            Assert.AreEqual(null, hschhqbn.Number);
            Assert.IsFalse(hschhqbn.UseOrdinal);
            Assert.AreEqual("Headquarters and Headquarters", hschhqbn.Mission);
            Assert.AreEqual("Headquarters and Headquarters Bty., ___72nd Fld. Art. Bde.", hschhqbn.GetFullName());
            Assert.AreEqual("us/army/72-fa-bde/hhb", hschhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/72-fa-bde/hhb", hschhqbn.CurrentOpsUrl);
            Assert.AreEqual("", hschhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, hschhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Battery, hschhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, hschhqbn.ServiceTypeIdx);
            Assert.AreEqual(null, hschhqbn.USState);

            Assert.AreEqual(2, hschhqbn.Bases.Count);

            Assert.AreEqual("us/nj/fort-dix", hschhqbn.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Fort Dix", hschhqbn.Bases[0].Name);
            Assert.AreEqual("JB McGuire-Dix-Lakehurst", hschhqbn.Bases[0].ParentBase);
            Assert.AreEqual("New Jersey, United States", hschhqbn.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/nj/fort-dix", hschhqbn.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, hschhqbn.Bases[0].DateFrom);
            Assert.AreEqual(null, hschhqbn.Bases[0].DateUntil);
            Assert.AreEqual(false, hschhqbn.Bases[0].IsDeployment);
            Assert.AreEqual(true, hschhqbn.Bases[0].IsCurrent);

            Assert.AreEqual("us/md/fort-george-g-meade", hschhqbn.Bases[1].CurrentOpsBaseRef);
            Assert.AreEqual("Fort George G. Meade", hschhqbn.Bases[1].Name);
            Assert.AreEqual(null, hschhqbn.Bases[1].ParentBase);
            Assert.AreEqual("Maryland, United States", hschhqbn.Bases[1].Location);
            Assert.AreEqual("https://currentops.com/installations/us/md/fort-george-g-meade", hschhqbn.Bases[1].CurrentOpsUrl);
            Assert.AreEqual(null, hschhqbn.Bases[1].DateFrom);
            Assert.AreEqual(null, hschhqbn.Bases[1].DateUntil);
            Assert.AreEqual(false, hschhqbn.Bases[1].IsDeployment);
            Assert.AreEqual(false, hschhqbn.Bases[1].IsCurrent);

            Assert.AreEqual(1, hschhqbn.HigherHqs.Count);

            Assert.AreEqual("us/army/72-fa-bde", hschhqbn.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, hschhqbn.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Organic, hschhqbn.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, hschhqbn.ShortForm.Count);

            Assert.AreEqual("HQ & HQ BTY., ___72 FLD. ART. BDE.", hschhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, hschhqbn.ShortForm[0].Type);

            Assert.AreEqual("G*ART___72|!", hschhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, hschhqbn.ShortForm[1].Type);

            Assert.AreEqual("HHB-_____/___72 FAB", hschhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, hschhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToCompanyAHHQBn_38id()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Liaison.Biz.Tests.Strings.HHQBnCompanyString.aCoyHeadquartersAndHqBn_38id;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);


            var hschhqbn = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(hschhqbn, typeof(CompanyOrg));
            Assert.AreEqual(null, hschhqbn.Number);
            Assert.IsFalse(hschhqbn.UseOrdinal);
            Assert.AreEqual(null, hschhqbn.Mission);
            Assert.AreEqual("A Coy., HHQ Bn., ___38th (V) (IN) Inf. Div.", hschhqbn.GetFullName());
            Assert.AreEqual("us/army/38-id/hhbn/co-a", hschhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/38-id/hhbn/co-a", hschhqbn.CurrentOpsUrl);
            Assert.AreEqual("", hschhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, hschhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Company, hschhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Volunteer, hschhqbn.ServiceTypeIdx);
            Assert.AreEqual("IN", hschhqbn.USState);

            Assert.AreEqual(0, hschhqbn.Bases.Count);            

            Assert.AreEqual(1, hschhqbn.HigherHqs.Count);

            Assert.AreEqual("us/army/38-id/hhbn", hschhqbn.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, hschhqbn.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Organic, hschhqbn.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, hschhqbn.ShortForm.Count);

            Assert.AreEqual("A COY., HHQ BN., ___38 (V) INF. DIV.", hschhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, hschhqbn.ShortForm[0].Type);

            Assert.AreEqual("G)INF___38@!|A", hschhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, hschhqbn.ShortForm[1].Type);

            Assert.AreEqual("A-HHB/___38 ID", hschhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, hschhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToCompanyCHHQBn_38id()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Liaison.Biz.Tests.Strings.HHQBnCompanyString.cCoyHeadquartersAndHqBn_38id;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);


            var c_coyhhqbn = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(c_coyhhqbn, typeof(CompanyOrg));
            Assert.AreEqual(null, c_coyhhqbn.Number);
            Assert.IsFalse(c_coyhhqbn.UseOrdinal);
            Assert.AreEqual(null, c_coyhhqbn.Mission);
            Assert.AreEqual("C Coy., HHQ Bn., ___38th (V) (IN) Inf. Div.", c_coyhhqbn.GetFullName());
            Assert.AreEqual("us/army/38-id/hhbn/co-c", c_coyhhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/38-id/hhbn/co-c", c_coyhhqbn.CurrentOpsUrl);
            Assert.AreEqual("", c_coyhhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, c_coyhhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Company, c_coyhhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Volunteer, c_coyhhqbn.ServiceTypeIdx);
            Assert.AreEqual("IN", c_coyhhqbn.USState);

            Assert.AreEqual(1, c_coyhhqbn.Bases.Count);

            Assert.AreEqual("us/in/anderson-armory", c_coyhhqbn.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Anderson Armory", c_coyhhqbn.Bases[0].Name);
            Assert.AreEqual("Anderson, Indiana, United States", c_coyhhqbn.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/in/anderson-armory", c_coyhhqbn.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, c_coyhhqbn.Bases[0].DateFrom);
            Assert.AreEqual(null, c_coyhhqbn.Bases[0].DateUntil);
            Assert.AreEqual(false, c_coyhhqbn.Bases[0].IsDeployment);
            Assert.AreEqual(true, c_coyhhqbn.Bases[0].IsCurrent);

            Assert.AreEqual(1, c_coyhhqbn.HigherHqs.Count);

            Assert.AreEqual("us/army/38-id/hhbn", c_coyhhqbn.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, c_coyhhqbn.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, c_coyhhqbn.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, c_coyhhqbn.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Organic, c_coyhhqbn.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, c_coyhhqbn.ShortForm.Count);

            Assert.AreEqual("C COY., HHQ BN., ___38 (V) INF. DIV.", c_coyhhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, c_coyhhqbn.ShortForm[0].Type);

            Assert.AreEqual("G)INF___38@!|C", c_coyhhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, c_coyhhqbn.ShortForm[1].Type);

            Assert.AreEqual("C-HHB/___38 ID", c_coyhhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, c_coyhhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToBrigade_408ConSupt()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.BrigadeString.brigadeContrSupt_408;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);

            var army1 = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(army1, typeof(BrigadeOrg));
            Assert.AreEqual(408, army1.Number);
            Assert.IsTrue(army1.UseOrdinal);
            Assert.AreEqual("Contracting Support", army1.Mission);
            Assert.AreEqual("__408th Contracting Support Brigade", army1.GetFullName());
            Assert.AreEqual("us/army/408-csb", army1.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/408-csb", army1.CurrentOpsUrl);
            Assert.AreEqual("", army1.CurrentOpsLogo);

            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, army1.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Brigade, army1.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, army1.ServiceTypeIdx);

            Assert.AreEqual(1, army1.Bases.Count);

            Assert.AreEqual("us/sc/shaw-afb", army1.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Shaw Air Force Base", army1.Bases[0].Name);
            Assert.AreEqual("Sumter, South Carolina, United States", army1.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/sc/shaw-afb", army1.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, army1.Bases[0].DateFrom);
            Assert.AreEqual(null, army1.Bases[0].DateUntil);
            Assert.AreEqual(false, army1.Bases[0].IsDeployment);
            Assert.AreEqual(true, army1.Bases[0].IsCurrent);

            Assert.AreEqual(1, army1.HigherHqs.Count);

            Assert.AreEqual("us/army/ecc", army1.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, army1.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, army1.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, army1.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Unknown, army1.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, army1.ShortForm.Count);

            Assert.AreEqual("__408 CON. SUPT. BDE.", army1.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, army1.ShortForm[0].Type);

            Assert.AreEqual("G*CONSUPT__408", army1.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, army1.ShortForm[1].Type);

            Assert.AreEqual("__408 CON SUPT BDE", army1.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, army1.ShortForm[2].Type);

            Assert.AreEqual(0, army1.ChildOrgs.Count);

            //Assert.AreEqual("us/army/asc", army1.ChildOrgs[0].CurrentOpsRef);
            //Assert.AreEqual("Army Sustainment Command", army1.ChildOrgs[0].Name);
            //Assert.IsFalse(army1.ChildOrgs[0].IsIndirect);

            //Assert.AreEqual("us/army/us-army-contracting-cmd", army1.ChildOrgs[1].CurrentOpsRef);
            //Assert.AreEqual("Army Contracting Command", army1.ChildOrgs[1].Name);
            //Assert.IsFalse(army1.ChildOrgs[1].IsIndirect);
            #endregion
        }
        [TestMethod]
        public void ConvertToBrigade_4Cav()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.BrigadeString.brigadeCav_4;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);

            var brig = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(brig, typeof(BrigadeOrg));
            Assert.AreEqual(4, brig.Number);
            Assert.IsTrue(brig.UseOrdinal);
            Assert.AreEqual("Cavalry", brig.Mission);
            Assert.AreEqual("____4th Cavalry Brigade", brig.GetFullName());
            Assert.AreEqual("us/army/4-cav-bde", brig.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/4-cav-bde", brig.CurrentOpsUrl);
            Assert.AreEqual("", brig.CurrentOpsLogo);

            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, brig.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Brigade, brig.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, brig.ServiceTypeIdx);            

            Assert.AreEqual(1, brig.Bases.Count);

            Assert.AreEqual("us/ky/fort-knox", brig.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Fort Knox", brig.Bases[0].Name);
            Assert.AreEqual("Radcliff, Kentucky, United States", brig.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/ky/fort-knox", brig.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, brig.Bases[0].DateFrom);
            Assert.AreEqual(null, brig.Bases[0].DateUntil);
            Assert.AreEqual(false, brig.Bases[0].IsDeployment);
            Assert.AreEqual(true, brig.Bases[0].IsCurrent);

            Assert.AreEqual(1, brig.HigherHqs.Count);

            Assert.AreEqual("us/army/1-army/div-east", brig.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, brig.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, brig.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, brig.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Assigned, brig.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, brig.ShortForm.Count);

            Assert.AreEqual("____4 CAV. BDE.", brig.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, brig.ShortForm[0].Type);

            Assert.AreEqual("G*CAV____4", brig.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, brig.ShortForm[1].Type);

            Assert.AreEqual("____4 CB", brig.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, brig.ShortForm[2].Type);

            Assert.AreEqual(1, brig.ChildOrgs.Count);

            Assert.AreEqual("us/army/4-cav-bde/hht", brig.ChildOrgs[0].CurrentOpsRef);
            Assert.AreEqual("Headquarters and Headquarters Troop, 4th Cavalry Brigade", brig.ChildOrgs[0].Name);
            Assert.IsFalse(brig.ChildOrgs[0].IsIndirect);
            #endregion
        }
        [TestMethod]
        public void ConvertToBrigade_177Arm()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.BrigadeString.brigadeArm_177;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);

            var brig = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(brig, typeof(BrigadeOrg));
            Assert.AreEqual(177, brig.Number);
            Assert.IsTrue(brig.UseOrdinal);
            Assert.AreEqual("Armoured", brig.Mission);
            Assert.AreEqual("__177th Armoured Brigade", brig.GetFullName());
            Assert.AreEqual("us/army/177-armored-bde", brig.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/177-armored-bde", brig.CurrentOpsUrl);
            Assert.AreEqual("", brig.CurrentOpsLogo);

            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, brig.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Brigade, brig.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, brig.ServiceTypeIdx);

            Assert.AreEqual(1, brig.Bases.Count);

            Assert.AreEqual("us/ms/camp-shelby", brig.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Camp Shelby", brig.Bases[0].Name);
            Assert.AreEqual("Mississippi, United States", brig.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/ms/camp-shelby", brig.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, brig.Bases[0].DateFrom);
            Assert.AreEqual(null, brig.Bases[0].DateUntil);
            Assert.AreEqual(false, brig.Bases[0].IsDeployment);
            Assert.AreEqual(true, brig.Bases[0].IsCurrent);

            Assert.AreEqual(1, brig.HigherHqs.Count);

            Assert.AreEqual("us/army/1-army/div-east", brig.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, brig.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, brig.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, brig.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Assigned, brig.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, brig.ShortForm.Count);

            Assert.AreEqual("__177 ARM. BDE.", brig.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, brig.ShortForm[0].Type);

            Assert.AreEqual("G*ARM__177", brig.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, brig.ShortForm[1].Type);

            Assert.AreEqual("__177 AB", brig.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, brig.ShortForm[2].Type);

            Assert.AreEqual(1, brig.ChildOrgs.Count);

            Assert.AreEqual("us/army/177-armored-bde/hhc", brig.ChildOrgs[0].CurrentOpsRef);
            Assert.AreEqual("Headquarters and Headquarters Company, 177th Armored Brigade", brig.ChildOrgs[0].Name);
            Assert.IsFalse(brig.ChildOrgs[0].IsIndirect);
            #endregion
        }
        [TestMethod]
        public void ConvertToBrigade_157Inf()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.BrigadeString.brigadeInf_157;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);

            var brig = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(brig, typeof(BrigadeOrg));
            Assert.AreEqual(157, brig.Number);
            Assert.IsTrue(brig.UseOrdinal);
            Assert.AreEqual("Infantry", brig.Mission);
            Assert.AreEqual("__157th Infantry Brigade", brig.GetFullName());
            Assert.AreEqual("us/army/157-in-bde", brig.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/157-in-bde", brig.CurrentOpsUrl);
            Assert.AreEqual("https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgSU4gQkRFIDAxNTc.png", brig.CurrentOpsLogo);

            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, brig.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Brigade, brig.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, brig.ServiceTypeIdx);

            Assert.AreEqual(2, brig.Bases.Count);

            Assert.AreEqual("us/in/camp-atterbury", brig.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Camp Atterbury", brig.Bases[0].Name);
            Assert.AreEqual("Edinburgh, Indiana, United States", brig.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/in/camp-atterbury", brig.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, brig.Bases[0].DateFrom);
            Assert.AreEqual(null, brig.Bases[0].DateUntil);
            Assert.AreEqual(false, brig.Bases[0].IsDeployment);
            Assert.AreEqual(true, brig.Bases[0].IsCurrent);

            Assert.AreEqual("us/sc/fort-jackson", brig.Bases[1].CurrentOpsBaseRef);
            Assert.AreEqual("Fort Jackson", brig.Bases[1].Name);
            Assert.AreEqual("Columbia, South Carolina, United States", brig.Bases[1].Location);
            Assert.AreEqual("https://currentops.com/installations/us/sc/fort-jackson", brig.Bases[1].CurrentOpsUrl);
            Assert.AreEqual(null, brig.Bases[1].DateFrom);
            Assert.AreEqual(null, brig.Bases[1].DateUntil);
            Assert.AreEqual(false, brig.Bases[1].IsDeployment);
            Assert.AreEqual(false, brig.Bases[1].IsCurrent);

            Assert.AreEqual(1, brig.HigherHqs.Count);

            Assert.AreEqual("us/army/1-army/div-east", brig.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, brig.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, brig.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, brig.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Assigned, brig.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, brig.ShortForm.Count);

            Assert.AreEqual("__157 INF. BDE.", brig.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, brig.ShortForm[0].Type);

            Assert.AreEqual("G*INF__157", brig.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, brig.ShortForm[1].Type);

            Assert.AreEqual("__157 IB", brig.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, brig.ShortForm[2].Type);

            Assert.AreEqual(1, brig.ChildOrgs.Count);

            Assert.AreEqual("us/army/157-in-bde/hhc", brig.ChildOrgs[0].CurrentOpsRef);
            Assert.AreEqual("Headquarters and Headquarters Company, 157th Infantry Brigade", brig.ChildOrgs[0].Name);
            Assert.IsFalse(brig.ChildOrgs[0].IsIndirect);
            #endregion
        }
        [TestMethod]
        public void ConvertToBrigade_72FA()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.BrigadeString.brigadeArt__72;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);

            var brig = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(brig, typeof(BrigadeOrg));
            Assert.AreEqual(72, brig.Number);
            Assert.IsTrue(brig.UseOrdinal);
            Assert.AreEqual("Field Artillery", brig.Mission);
            Assert.AreEqual("___72nd Field Artillery Brigade", brig.GetFullName());
            Assert.AreEqual("us/army/72-fa-bde", brig.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/72-fa-bde", brig.CurrentOpsUrl);
            Assert.AreEqual("https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgRkEgQkRFIDAwNzI.png", brig.CurrentOpsLogo);

            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, brig.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Brigade, brig.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, brig.ServiceTypeIdx);

            Assert.AreEqual(2, brig.Bases.Count);

            Assert.AreEqual("us/nj/fort-dix", brig.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Fort Dix", brig.Bases[0].Name);
            Assert.AreEqual("JB McGuire-Dix-Lakehurst", brig.Bases[0].ParentBase);
            Assert.AreEqual("New Jersey, United States", brig.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/nj/fort-dix", brig.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, brig.Bases[0].DateFrom);
            Assert.AreEqual(null, brig.Bases[0].DateUntil);
            Assert.AreEqual(false, brig.Bases[0].IsDeployment);
            Assert.AreEqual(true, brig.Bases[0].IsCurrent);

            Assert.AreEqual("us/md/fort-george-g-meade", brig.Bases[1].CurrentOpsBaseRef);
            Assert.AreEqual("Fort George G. Meade", brig.Bases[1].Name);
            Assert.AreEqual(null, brig.Bases[1].ParentBase);
            Assert.AreEqual("Maryland, United States", brig.Bases[1].Location);
            Assert.AreEqual("https://currentops.com/installations/us/md/fort-george-g-meade", brig.Bases[1].CurrentOpsUrl);
            Assert.AreEqual(null, brig.Bases[1].DateFrom);
            Assert.AreEqual(null, brig.Bases[1].DateUntil);
            Assert.AreEqual(false, brig.Bases[1].IsDeployment);
            Assert.AreEqual(false, brig.Bases[1].IsCurrent);

            Assert.AreEqual(2, brig.HigherHqs.Count);

            Assert.AreEqual("us/army/1-army/div-east", brig.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, brig.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, brig.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, brig.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Assigned, brig.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual("us/army/vii-corps-arty", brig.HigherHqs[1].CurrentOpsRef);
            Assert.AreEqual(null, brig.HigherHqs[1].DateFrom);
            Assert.AreEqual(null, brig.HigherHqs[1].DateUntil);
            Assert.AreEqual(false, brig.HigherHqs[1].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Unknown, brig.HigherHqs[1].CommandRelationshipType);

            Assert.AreEqual(3, brig.ShortForm.Count);

            Assert.AreEqual("___72 FLD. ART. BDE.", brig.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, brig.ShortForm[0].Type);

            Assert.AreEqual("G*ART___72", brig.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, brig.ShortForm[1].Type);

            Assert.AreEqual("___72 FAB", brig.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, brig.ShortForm[2].Type);

            Assert.AreEqual(1, brig.ChildOrgs.Count);

            Assert.AreEqual("us/army/72-fa-bde/hhb", brig.ChildOrgs[0].CurrentOpsRef);
            Assert.AreEqual("Headquarters and Headquarters Battery, 72nd Field Artillery Brigade", brig.ChildOrgs[0].Name);
            Assert.IsFalse(brig.ChildOrgs[0].IsIndirect);
            #endregion
        }
        [TestMethod]
        public void ConvertToBrigadeCT()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Liaison.Biz.Tests.Strings.BrigadeString.brigadeCombatTeam_76;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);


            var bct_76 = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(bct_76, typeof(BrigadeOrg));
            Assert.AreEqual(76, bct_76.Number);
            Assert.IsTrue(bct_76.UseOrdinal);
            Assert.AreEqual("Infantry", bct_76.Mission);
            Assert.AreEqual("___76th (V) (IN) Infantry Brigade Combat Team", bct_76.GetFullName());
            Assert.AreEqual("us/army/76-ibct", bct_76.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/76-ibct", bct_76.CurrentOpsUrl);
            Assert.AreEqual("https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgSUJDVCAwMDc2.png", bct_76.CurrentOpsLogo);
            Assert.IsTrue(((BrigadeOrg)bct_76).IsBrigadeCombatTeam);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, bct_76.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Brigade, bct_76.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Volunteer, bct_76.ServiceTypeIdx);
            Assert.AreEqual("IN", bct_76.USState);

            Assert.AreEqual(1, bct_76.Bases.Count);

            Assert.AreEqual("us/in/lawrence-armory", bct_76.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Lawrence Armory", bct_76.Bases[0].Name);
            Assert.AreEqual("Lawrence, Indiana, United States", bct_76.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/in/lawrence-armory", bct_76.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, bct_76.Bases[0].DateFrom);
            Assert.AreEqual(null, bct_76.Bases[0].DateUntil);
            Assert.AreEqual(false, bct_76.Bases[0].IsDeployment);
            Assert.AreEqual(true, bct_76.Bases[0].IsCurrent);

            Assert.AreEqual(1, bct_76.HigherHqs.Count);

            Assert.AreEqual("us/army/38-id", bct_76.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, bct_76.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, bct_76.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, bct_76.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Unknown, bct_76.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, bct_76.ShortForm.Count);

            Assert.AreEqual("___76 (V) INF. BCT", bct_76.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, bct_76.ShortForm[0].Type);

            Assert.AreEqual("G*INF___76", bct_76.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, bct_76.ShortForm[1].Type);

            Assert.AreEqual("___76 IBCT", bct_76.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, bct_76.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToGroup_1MSG()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.GroupString.grp1msg;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);


            var mobsuptgru = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(mobsuptgru, typeof(GroupOrg));
            Assert.AreEqual(1, mobsuptgru.Number);
            Assert.IsTrue(mobsuptgru.UseOrdinal);
            Assert.AreEqual("Mobilization Support", mobsuptgru.Mission);
            Assert.AreEqual("____1st (R) Mobilization Support Group", mobsuptgru.GetFullName());
            Assert.AreEqual("us/army/1-msg", mobsuptgru.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/1-msg", mobsuptgru.CurrentOpsUrl);
            Assert.AreEqual("", mobsuptgru.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, mobsuptgru.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Group, mobsuptgru.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Reserve, mobsuptgru.ServiceTypeIdx);
            Assert.AreEqual(null, mobsuptgru.USState);

            Assert.AreEqual(1, mobsuptgru.Bases.Count);

            Assert.AreEqual("us/ny/ernie-pyle-usarc", mobsuptgru.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Ernie Pyle Army Reserve Centre", mobsuptgru.Bases[0].Name);
            Assert.AreEqual(null, mobsuptgru.Bases[0].ParentBase);
            Assert.AreEqual("Fort Totten, New York, United States", mobsuptgru.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/ny/ernie-pyle-usarc", mobsuptgru.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, mobsuptgru.Bases[0].DateFrom);
            Assert.AreEqual(null, mobsuptgru.Bases[0].DateUntil);
            Assert.AreEqual(false, mobsuptgru.Bases[0].IsDeployment);
            Assert.AreEqual(true, mobsuptgru.Bases[0].IsCurrent);

            Assert.AreEqual(1, mobsuptgru.HigherHqs.Count);

            Assert.AreEqual("us/army/87-arsc", mobsuptgru.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, mobsuptgru.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, mobsuptgru.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, mobsuptgru.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Assigned, mobsuptgru.HigherHqs[0].CommandRelationshipType);
            
            Assert.AreEqual(4, mobsuptgru.ShortForm.Count);

            Assert.AreEqual("____1 (R) MOB. SUPT. GRP.", mobsuptgru.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, mobsuptgru.ShortForm[0].Type);

            Assert.AreEqual("G/MOBSUPT____1", mobsuptgru.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, mobsuptgru.ShortForm[1].Type);

            Assert.AreEqual("G*MOBSUPT____1", mobsuptgru.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Placeholder, mobsuptgru.ShortForm[2].Type);

            Assert.AreEqual("____1 MSG", mobsuptgru.ShortForm[3].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, mobsuptgru.ShortForm[3].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToGroup_1101MSB()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.BattalionString.bn1101msb;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);


            var mobsuptbn = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(mobsuptbn, typeof(BattalionOrg));
            Assert.AreEqual(1101, mobsuptbn.Number);
            Assert.IsTrue(mobsuptbn.UseOrdinal);
            Assert.AreEqual("Mobilization Support", mobsuptbn.Mission);
            Assert.AreEqual("_1101st (R) Mobilization Support Bn.", mobsuptbn.GetFullName());
            Assert.AreEqual("us/army/1101-msbn", mobsuptbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/1101-msbn", mobsuptbn.CurrentOpsUrl);
            Assert.AreEqual("", mobsuptbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, mobsuptbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Battalion, mobsuptbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Reserve, mobsuptbn.ServiceTypeIdx);
            Assert.AreEqual(null, mobsuptbn.USState);

            Assert.AreEqual(1, mobsuptbn.Bases.Count);

            Assert.AreEqual("us/ny/ernie-pyle-usarc", mobsuptbn.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Ernie Pyle Army Reserve Centre", mobsuptbn.Bases[0].Name);
            Assert.AreEqual(null, mobsuptbn.Bases[0].ParentBase);
            Assert.AreEqual("Fort Totten, New York, United States", mobsuptbn.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/ny/ernie-pyle-usarc", mobsuptbn.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, mobsuptbn.Bases[0].DateFrom);
            Assert.AreEqual(null, mobsuptbn.Bases[0].DateUntil);
            Assert.AreEqual(false, mobsuptbn.Bases[0].IsDeployment);
            Assert.AreEqual(true, mobsuptbn.Bases[0].IsCurrent);

            Assert.AreEqual(1, mobsuptbn.HigherHqs.Count);

            Assert.AreEqual("us/army/1-msg", mobsuptbn.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, mobsuptbn.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, mobsuptbn.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, mobsuptbn.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Unknown, mobsuptbn.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(4, mobsuptbn.ShortForm.Count);

            Assert.AreEqual("_1101 (R) MOB. SUPT. BN., GSC", mobsuptbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, mobsuptbn.ShortForm[0].Type);

            Assert.AreEqual("GSC@_1101", mobsuptbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, mobsuptbn.ShortForm[1].Type);

            Assert.AreEqual("G*MOBSUPT____1", mobsuptbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Placeholder, mobsuptbn.ShortForm[2].Type);

            Assert.AreEqual("____1 MSG", mobsuptbn.ShortForm[3].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, mobsuptbn.ShortForm[3].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToDetachmentHHArmy_1East()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Strings.DetachmentString.army_1_div_east_hhd;

            var xmlreader = XmlReader.Create(new StringReader(xmlText));
            xmlreader.Read();

            var serializer = new XmlSerializer(typeof(CurrentOpsObject));
            var currentOpsObject = (CurrentOpsObject)serializer.Deserialize(xmlreader);

            var army1 = Converter.ConvertToMilOrg.Convert(currentOpsObject);

            #region assertions
            Assert.IsInstanceOfType(army1, typeof(DetachmentOrg));
            Assert.IsNull(army1.Number);
            Assert.IsFalse(army1.UseOrdinal);
            Assert.AreEqual("Headquarters and Headquarters", army1.Mission);
            Assert.AreEqual("Headquarters and Headquarters Detachment, Division East, First Army", army1.GetFullName());
            Assert.AreEqual("us/army/1-army/div-east/hhd", army1.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/1-army/div-east/hhd", army1.CurrentOpsUrl);
            Assert.AreEqual("https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgQVJNWSAwMDAx.png", army1.CurrentOpsLogo);

            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, army1.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Detachment, army1.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, army1.ServiceTypeIdx);
            Assert.AreEqual(null, army1.USState);

            Assert.AreEqual(1, army1.Bases.Count);

            Assert.AreEqual("us/md/fort-george-g-meade", army1.Bases[0].CurrentOpsBaseRef);
            Assert.AreEqual("Fort George G. Meade", army1.Bases[0].Name);
            Assert.AreEqual("Maryland, United States", army1.Bases[0].Location);
            Assert.AreEqual("https://currentops.com/installations/us/md/fort-george-g-meade", army1.Bases[0].CurrentOpsUrl);
            Assert.AreEqual(null, army1.Bases[0].DateFrom);
            Assert.AreEqual(null, army1.Bases[0].DateUntil);
            Assert.AreEqual(false, army1.Bases[0].IsDeployment);
            Assert.AreEqual(true, army1.Bases[0].IsCurrent);

            Assert.AreEqual(1, army1.HigherHqs.Count);

            Assert.AreEqual("us/army/1-army/div-east", army1.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, army1.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, army1.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, army1.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Organic, army1.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, army1.ShortForm.Count);

            Assert.AreEqual("HQ & HQ DET., DIV. EAST, ____1 ARMY", army1.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, army1.ShortForm[0].Type);

            Assert.AreEqual("G&____1)EAST?!", army1.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, army1.ShortForm[1].Type);

            Assert.AreEqual("HHD, E DIV, ____1 A", army1.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, army1.ShortForm[2].Type);

            Assert.AreEqual(0, army1.ChildOrgs.Count);

            #endregion
        }
        
    }
}
