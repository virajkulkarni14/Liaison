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
            Assert.AreEqual("1st Infantry Division", division.GetFullName());
            Assert.AreEqual("us/army/1-id", division.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/1-id", division.CurrentOpsUrl);
            Assert.AreEqual("https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgSUQgMDAwMQ.png", division.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, division.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Division, division.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, division.ServiceTypeIdx);

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

            Assert.AreEqual("____1 Inf. Div.", division.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, division.ShortForm[0].Type);

            Assert.AreEqual("INF)____1", division.ShortForm[1].Text);
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
            Assert.AreEqual("38th Infantry Division", division.GetFullName());
            Assert.AreEqual("us/army/38-id", division.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/38-id", division.CurrentOpsUrl);
            Assert.AreEqual("https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgSUQgMDAzOA.png", division.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, division.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Division, division.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Volunteer, division.ServiceTypeIdx);

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

            Assert.AreEqual("___38 Inf. Div.", division.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, division.ShortForm[0].Type);

            Assert.AreEqual("INF)___38", division.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, division.ShortForm[1].Type);

            Assert.AreEqual("___38 ID", division.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, division.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToHHQBattalion_1id()
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
            Assert.AreEqual("Headquarters and Headquarters Bn., ____1 INF. DIV.", hhqbn.GetFullName());
            Assert.AreEqual("us/army/1-id/hhbn", hhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/1-id/hhbn", hhqbn.CurrentOpsUrl);
            Assert.AreEqual("", hhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, hhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Battalion, hhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Active, hhqbn.ServiceTypeIdx);

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

            Assert.AreEqual("HHQ Bn., ____1 Inf. Div.", hhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, hhqbn.ShortForm[0].Type);

            Assert.AreEqual("INF)____1@!", hhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, hhqbn.ShortForm[1].Type);

            Assert.AreEqual("HHB/____1 ID", hhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, hhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToHHQBattalion_38id()
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
            Assert.AreEqual("Headquarters and Headquarters Bn., ___38 INF. DIV.", hhqbn.GetFullName());
            Assert.AreEqual("us/army/38-id/hhbn", hhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/38-id/hhbn", hhqbn.CurrentOpsUrl);
            Assert.AreEqual("", hhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, hhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Battalion, hhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Volunteer, hhqbn.ServiceTypeIdx);

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

            Assert.AreEqual("HHQ Bn., ___38 Inf. Div.", hhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, hhqbn.ShortForm[0].Type);

            Assert.AreEqual("INF)___38@!", hhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, hhqbn.ShortForm[1].Type);

            Assert.AreEqual("HHB/___38 ID", hhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, hhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToHSCoyHHQBn_38id()
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
            Assert.AreEqual("Headquarters and Support Coy., HHQ BN., ___38 INF. DIV.", hschhqbn.GetFullName());
            Assert.AreEqual("us/army/38-id/hhbn/hsc", hschhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/38-id/hhbn/hsc", hschhqbn.CurrentOpsUrl);
            Assert.AreEqual("", hschhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, hschhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Company, hschhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Volunteer, hschhqbn.ServiceTypeIdx);

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

            Assert.AreEqual("HQ & Supt. Coy., HHQ Bn., ___38 Inf. Div.", hschhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, hschhqbn.ShortForm[0].Type);

            Assert.AreEqual("INF)___38@!|!", hschhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, hschhqbn.ShortForm[1].Type);

            Assert.AreEqual("HSC-HHB/___38 ID", hschhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, hschhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToHSCoy_76ibct()
        {
            var converter = new Liaison.Biz.Converter.ConvertToMilOrg();

            string xmlText = Liaison.Biz.Tests.Strings.HHQBnCompanyString.cCoyHeadquartersAndHq_76ibct;

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
            Assert.AreEqual("Headquarters and Headquarters Coy., ___76 INF. BCT", hschhqbn.GetFullName());
            Assert.AreEqual("us/army/76-ibct/hhc", hschhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/76-ibct/hhc", hschhqbn.CurrentOpsUrl);
            Assert.AreEqual("", hschhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, hschhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Company, hschhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Volunteer, hschhqbn.ServiceTypeIdx);

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

            Assert.AreEqual("HQ & HQ Coy., ___76 Inf. BCT", hschhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, hschhqbn.ShortForm[0].Type);

            Assert.AreEqual("INF*___76|!", hschhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, hschhqbn.ShortForm[1].Type);

            Assert.AreEqual("HHC-_____/___76 IBCT", hschhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, hschhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToACoyHHQBn_38id()
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
            Assert.AreEqual("A Coy., HHQ BN., ___38 INF. DIV.", hschhqbn.GetFullName());
            Assert.AreEqual("us/army/38-id/hhbn/co-a", hschhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/38-id/hhbn/co-a", hschhqbn.CurrentOpsUrl);
            Assert.AreEqual("", hschhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, hschhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Company, hschhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Volunteer, hschhqbn.ServiceTypeIdx);

            Assert.AreEqual(0, hschhqbn.Bases.Count);            

            Assert.AreEqual(1, hschhqbn.HigherHqs.Count);

            Assert.AreEqual("us/army/38-id/hhbn", hschhqbn.HigherHqs[0].CurrentOpsRef);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateFrom);
            Assert.AreEqual(null, hschhqbn.HigherHqs[0].DateUntil);
            Assert.AreEqual(true, hschhqbn.HigherHqs[0].IsCurrent);
            Assert.AreEqual(Liaison.Helper.Enumerators.HigherHqType.Organic, hschhqbn.HigherHqs[0].CommandRelationshipType);

            Assert.AreEqual(3, hschhqbn.ShortForm.Count);

            Assert.AreEqual("A Coy., HHQ Bn., ___38 Inf. Div.", hschhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, hschhqbn.ShortForm[0].Type);

            Assert.AreEqual("INF)___38@!|A", hschhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, hschhqbn.ShortForm[1].Type);

            Assert.AreEqual("A-HHB/___38 ID", hschhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, hschhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToCCoyHHQBn_38id()
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
            Assert.AreEqual("C Coy., HHQ BN., ___38 INF. DIV.", c_coyhhqbn.GetFullName());
            Assert.AreEqual("us/army/38-id/hhbn/co-c", c_coyhhqbn.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/38-id/hhbn/co-c", c_coyhhqbn.CurrentOpsUrl);
            Assert.AreEqual("", c_coyhhqbn.CurrentOpsLogo);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, c_coyhhqbn.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Company, c_coyhhqbn.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Volunteer, c_coyhhqbn.ServiceTypeIdx);

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

            Assert.AreEqual("C Coy., HHQ Bn., ___38 Inf. Div.", c_coyhhqbn.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, c_coyhhqbn.ShortForm[0].Type);

            Assert.AreEqual("INF)___38@!|C", c_coyhhqbn.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, c_coyhhqbn.ShortForm[1].Type);

            Assert.AreEqual("C-HHB/___38 ID", c_coyhhqbn.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, c_coyhhqbn.ShortForm[2].Type);
            #endregion
        }
        [TestMethod]
        public void ConvertToBCT()
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
            Assert.AreEqual("76th Infantry Brigade Combat Team", bct_76.GetFullName());
            Assert.AreEqual("us/army/76-ibct", bct_76.CurrentOpsRef);
            Assert.AreEqual("https://currentops.com/unit/us/army/76-ibct", bct_76.CurrentOpsUrl);
            Assert.AreEqual("https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgSUJDVCAwMDc2.png", bct_76.CurrentOpsLogo);
            Assert.IsTrue(((BrigadeOrg)bct_76).IsBrigadeCombatTeam);
            Assert.AreEqual(Liaison.Helper.Enumerators.Services.Army, bct_76.ServiceId);
            Assert.AreEqual(Liaison.Helper.Enumerators.UnitType.Brigade, bct_76.UnitTypeId);
            Assert.AreEqual(Liaison.Helper.Enumerators.ServiceType.Volunteer, bct_76.ServiceTypeIdx);

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

            Assert.AreEqual("___76 Inf. Bde.", bct_76.ShortForm[0].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.ShortName, bct_76.ShortForm[0].Type);

            Assert.AreEqual("INF*___76", bct_76.ShortForm[1].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.IndexName, bct_76.ShortForm[1].Type);

            Assert.AreEqual("___76 IB", bct_76.ShortForm[2].Text);
            Assert.AreEqual(Helper.Enumerators.ShortFormType.Other, bct_76.ShortForm[2].Type);
            #endregion
        }
    }
}
