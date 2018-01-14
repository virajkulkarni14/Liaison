using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.Biz.Tests.Strings
{
    public class CommandString
    {
        public static string armyMaterialCommand = @"  <CurrentOpsObject>
    <Url>https://currentops.com/unit/us/army/amc</Url>
    <NameNode>Army Materiel Command
                            U.S. Army</NameNode>
    <FullName>Army Materiel Command, U.S. Army</FullName>
    <SplitName>Army Materiel Command</SplitName>
    <Service>U.S. Army</Service>
    <LogoUrl>https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgQU1D.png</LogoUrl>
    <Locations>
      <LocationObject>
        <DateRange>... - Present</DateRange>
        <BaseName>Redstone Arsenal</BaseName>
        <Deployment>false</Deployment>
        <Location>Alabama, United States</Location>
        <IsCurrent>true</IsCurrent>
        <Url>https://currentops.com/installations/us/al/redstone-ars</Url>
        <Id>us/al/redstone-ars</Id>
      </LocationObject>
    </Locations>
    <HigherHq>
      <HigherHqObject>
        <Url>https://currentops.com/unit/us/army/doa</Url>
        <Id>us/army/doa</Id>
        <DateRange>... - Present</DateRange>
        <IsCurrent>true</IsCurrent>
        <HHQAcronym>Department of the Army</HHQAcronym>
        <Name>Department of the Army</Name>
      </HigherHqObject>
    </HigherHq>
    <Children>
      <SubUnitObject>
        <Url>https://currentops.com/unit/us/army/asc</Url>
        <FullName>United States Army Sustainment Command</FullName>
        <Name>United States Army Sustainment Command</Name>
        <Id>us/army/asc</Id>
        <IsIndirect>false</IsIndirect>
      </SubUnitObject>
      <SubUnitObject>
        <Url>https://currentops.com/unit/us/army/us-army-contracting-cmd</Url>
        <FullName>United States Army Contracting Command</FullName>
        <Name>United States Army Contracting Command</Name>
        <Id>us/army/us-army-contracting-cmd</Id>
        <IsIndirect>false</IsIndirect>
      </SubUnitObject>
    </Children>
    <UnitService>Army</UnitService>
    <UnitComponent>Active</UnitComponent>
  </CurrentOpsObject>
";
    }
}
