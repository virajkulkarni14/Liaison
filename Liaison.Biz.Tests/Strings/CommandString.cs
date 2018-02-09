using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.Biz.Tests.Strings
{
    public class BattalionString
    {
        internal static readonly string bn1105msb = @"<CurrentOpsObject>
<Url>https://currentops.com/unit/us/army/1105-msbn</Url>
<NameNode>
1105th Mobilization Support Battalion U.S. Army
</NameNode>
<FullName>1105th Mobilization Support Battalion, U.S. Army</FullName>
<SplitName>1105th Mobilization Support Battalion</SplitName>
<Service>U.S. Army</Service>
<LogoUrl/>
<Locations>
<LocationObject>
<DateRange>... - Present</DateRange>
<BaseName>Fort Eustis United States Army Reserve Center</BaseName>
<Deployment>false</Deployment>
<Location>
Fort Eustis | JB Langley-Eustis | Newport News, Virginia, United States
</Location>
<IsCurrent>true</IsCurrent>
<Url>
https://currentops.com/installations/us/va/fort-eustis/usarc
</Url>
<Id>us/va/fort-eustis/usarc</Id>
</LocationObject>
</Locations>
<HigherHq>
<HigherHqObject>
<Url>https://currentops.com/unit/us/army/1-msg</Url>
<Id>us/army/1-msg</Id>
<DateRange>... - Present</DateRange>
<IsCurrent>true</IsCurrent>
<HHQAcronym>1st MSG</HHQAcronym>
<Name>1st Mobilization Support Group</Name>
</HigherHqObject>
</HigherHq>
<Children/>
<UnitService>Army</UnitService>
<UnitComponent>Reserve</UnitComponent>
</CurrentOpsObject>";
        internal static readonly string bn1101msb = @"<CurrentOpsObject>
<Url>https://currentops.com/unit/us/army/1101-msbn</Url>
<NameNode>
1101st Mobilization Support Battalion U.S. Army
</NameNode>
<FullName>1101st Mobilization Support Battalion, U.S. Army</FullName>
<SplitName>1101st Mobilization Support Battalion</SplitName>
<Service>U.S. Army</Service>
<LogoUrl/>
<Locations>
<LocationObject>
<DateRange>... - Present</DateRange>
<BaseName>Ernie Pyle United States Army Reserve Center</BaseName>
<Deployment>false</Deployment>
<Location>Fort Totten, New York, United States</Location>
<IsCurrent>true</IsCurrent>
<Url>
https://currentops.com/installations/us/ny/ernie-pyle-usarc
</Url>
<Id>us/ny/ernie-pyle-usarc</Id>
</LocationObject>
</Locations>
<HigherHq>
<HigherHqObject>
<Url>https://currentops.com/unit/us/army/1-msg</Url>
<Id>us/army/1-msg</Id>
<DateRange>... - Present</DateRange>
<IsCurrent>true</IsCurrent>
<HHQAcronym>1st MSG</HHQAcronym>
<Name>1st Mobilization Support Group</Name>
</HigherHqObject>
</HigherHq>
<Children/>
<UnitService>Army</UnitService>
<UnitComponent>Reserve</UnitComponent>
</CurrentOpsObject>";
        internal static readonly string bn3_360= @"<CurrentOpsObject>
<Url>https://currentops.com/unit/us/army/360-rgt/3-bn</Url>
<NameNode>
3rd Battalion, 360th Regiment U.S. Army
</NameNode>
<FullName>3rd Battalion, 360th Regiment, U.S. Army</FullName>
<SplitName>3rd Battalion, 360th Regiment</SplitName>
<Service>U.S. Army</Service>
<LogoUrl/>
<Locations>
<LocationObject>
<DateRange>... - Present</DateRange>
<BaseName>Moore Hall United States Army Reserve Center</BaseName>
<Deployment>false</Deployment>
<Location>Salt Lake City, Utah, United States</Location>
<IsCurrent>true</IsCurrent>
<Url>
https://currentops.com/installations/us/ut/moore-hall-usarc
</Url>
<Id>us/ut/moore-hall-usarc</Id>
</LocationObject>
</Locations>
<HigherHq>
<HigherHqObject>
<Url>https://currentops.com/unit/us/army/5-armored-bde</Url>
<Id>us/army/5-armored-bde</Id>
<DateRange>... - Present</DateRange>
<IsCurrent>true</IsCurrent>
<HHQAcronym>5th Armored Brigade</HHQAcronym>
<Type>OPCON</Type>
<Name>5th Armored Brigade</Name>
</HigherHqObject>
<HigherHqObject>
<Url>https://currentops.com/unit/us/army/85-arsc</Url>
<Id>us/army/85-arsc</Id>
<DateRange>... - Present</DateRange>
<IsCurrent>true</IsCurrent>
<HHQAcronym>85th ARSC</HHQAcronym>
<Type>ADCON</Type>
<Name>85th Army Reserve Support Command</Name>
</HigherHqObject>
</HigherHq>
<Children/>
<UnitService>Army</UnitService>
<UnitComponent>Reserve</UnitComponent>
</CurrentOpsObject>";
        internal static readonly string bn2_363 = @"<CurrentOpsObject><Url>https://currentops.com/unit/us/army/363-rgt/2-bn</Url><NameNode>2nd Battalion, 363rd Regiment
                            U.S. Army</NameNode><FullName>2nd Battalion, 363rd Regiment, U.S. Army</FullName><SplitName>2nd Battalion, 363rd Regiment</SplitName><Service>U.S. Army</Service><LogoUrl/><Locations><LocationObject><DateRange>... - Present</DateRange><BaseName>Herrea Hall United States Army Reserve Center</BaseName><Deployment>false</Deployment><Location>Mesa, Arizona, United States</Location><IsCurrent>true</IsCurrent><Url>https://currentops.com/installations/us/az/herrea-hall-usarc</Url><Id>us/az/herrea-hall-usarc</Id></LocationObject></Locations><HigherHq><HigherHqObject><Url>https://currentops.com/unit/us/army/5-armored-bde</Url><Id>us/army/5-armored-bde</Id><DateRange>... - Present</DateRange><IsCurrent>true</IsCurrent><HHQAcronym>5th Armored Brigade</HHQAcronym><Type>OPCON</Type><Name>5th Armored Brigade</Name></HigherHqObject><HigherHqObject><Url>https://currentops.com/unit/us/army/402-fa-bde</Url><Id>us/army/402-fa-bde</Id><DateRange/><IsCurrent>false</IsCurrent><HHQAcronym>402nd FA Bde</HHQAcronym><Type>OPCON</Type><Name>402nd Field Artillery Brigade</Name></HigherHqObject><HigherHqObject><Url>https://currentops.com/unit/us/army/85-arsc</Url><Id>us/army/85-arsc</Id><DateRange>... - Present</DateRange><IsCurrent>true</IsCurrent><HHQAcronym>85th ARSC</HHQAcronym><Type>ADCON</Type><Name>85th Army Reserve Support Command</Name></HigherHqObject></HigherHq><Children/><UnitService>Army</UnitService><UnitComponent>Reserve</UnitComponent></CurrentOpsObject>";
    }
    public class GroupString
    {
        internal static readonly string grp1msg= @"<CurrentOpsObject>
<Url>https://currentops.com/unit/us/army/1-msg</Url>
<NameNode>
1st Mobilization Support Group U.S. Army
</NameNode>
<FullName>1st Mobilization Support Group, U.S. Army</FullName>
<SplitName>1st Mobilization Support Group</SplitName>
<Service>U.S. Army</Service>
<LogoUrl/>
<Locations>
<LocationObject>
<DateRange>... - Present</DateRange>
<BaseName>Ernie Pyle United States Army Reserve Center</BaseName>
<Deployment>false</Deployment>
<Location>Fort Totten, New York, United States</Location>
<IsCurrent>true</IsCurrent>
<Url>
https://currentops.com/installations/us/ny/ernie-pyle-usarc
</Url>
<Id>us/ny/ernie-pyle-usarc</Id>
</LocationObject>
</Locations>
<HigherHq>
<HigherHqObject>
<Url>https://currentops.com/unit/us/army/87-arsc</Url>
<Id>us/army/87-arsc</Id>
<DateRange>... - Present</DateRange>
<IsCurrent>true</IsCurrent>
<HHQAcronym>87th ARSC</HHQAcronym>
<Type>Assigned</Type>
<Name>87th Army Reserve Support Command</Name>
</HigherHqObject>
</HigherHq>
<Children>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1101-msbn</Url>
<FullName>1101st Mobilization Support Battalion</FullName>
<Name>1101st Mobilization Support Battalion</Name>
<Id>us/army/1101-msbn</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1102-msbn</Url>
<FullName>1102nd Mobilization Support Battalion</FullName>
<Name>1102nd Mobilization Support Battalion</Name>
<Id>us/army/1102-msbn</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1103-msbn</Url>
<FullName>1103rd Mobilization Support Battalion</FullName>
<Name>1103rd Mobilization Support Battalion</Name>
<Id>us/army/1103-msbn</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1104-msbn</Url>
<FullName>1104th Mobilization Support Battalion</FullName>
<Name>1104th Mobilization Support Battalion</Name>
<Id>us/army/1104-msbn</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1105-msbn</Url>
<FullName>1105th Mobilization Support Battalion</FullName>
<Name>1105th Mobilization Support Battalion</Name>
<Id>us/army/1105-msbn</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1106-msbn</Url>
<FullName>1106th Mobilization Support Battalion</FullName>
<Name>1106th Mobilization Support Battalion</Name>
<Id>us/army/1106-msbn</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1107-msbn</Url>
<FullName>1107th Mobilization Support Battalion</FullName>
<Name>1107th Mobilization Support Battalion</Name>
<Id>us/army/1107-msbn</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1108-msbn</Url>
<FullName>1108th Mobilization Support Battalion</FullName>
<Name>1108th Mobilization Support Battalion</Name>
<Id>us/army/1108-msbn</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1109-msbn</Url>
<FullName>1109th Mobilization Support Battalion</FullName>
<Name>1109th Mobilization Support Battalion</Name>
<Id>us/army/1109-msbn</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1110-msbn</Url>
<FullName>1110th Mobilization Support Battalion</FullName>
<Name>1110th Mobilization Support Battalion</Name>
<Id>us/army/1110-msbn</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1111-msbn</Url>
<FullName>1111th Mobilization Support Battalion</FullName>
<Name>1111th Mobilization Support Battalion</Name>
<Id>us/army/1111-msbn</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1112-msbn</Url>
<FullName>1112th Mobilization Support Battalion</FullName>
<Name>1112th Mobilization Support Battalion</Name>
<Id>us/army/1112-msbn</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1113-msbn</Url>
<FullName>1113th Mobilization Support Battalion</FullName>
<Name>1113th Mobilization Support Battalion</Name>
<Id>us/army/1113-msbn</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1114-msbn</Url>
<FullName>1114th Mobilization Support Battalion</FullName>
<Name>1114th Mobilization Support Battalion</Name>
<Id>us/army/1114-msbn</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1115-msbn</Url>
<FullName>1115th Mobilization Support Battalion</FullName>
<Name>1115th Mobilization Support Battalion</Name>
<Id>us/army/1115-msbn</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1116-msbn</Url>
<FullName>1116th Mobilization Support Battalion</FullName>
<Name>1116th Mobilization Support Battalion</Name>
<Id>us/army/1116-msbn</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
</Children>
<UnitService>Army</UnitService>
<UnitComponent>Reserve</UnitComponent>
</CurrentOpsObject>";
    }
    public class CommandString
    {
  
        public static readonly string armyMaterialCommand = @"<CurrentOpsObject>
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
        internal static readonly string cmd87aressupt = @"<CurrentOpsObject>
<Url>https://currentops.com/unit/us/army/87-arsc</Url>
<NameNode>
87th Army Reserve Support Command U.S. Army
</NameNode>
<FullName>87th Army Reserve Support Command, U.S. Army</FullName>
<SplitName>87th Army Reserve Support Command</SplitName>
<Service>U.S. Army</Service>
<LogoUrl>
https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgQVJTQyAwMDg3.png
</LogoUrl>
<Locations>
<LocationObject>
<DateRange>... - Present</DateRange>
<BaseName>Horace B. Hanson United States Army Reserve Center</BaseName>
<Deployment>false</Deployment>
<Location>Birmingham, Alabama, United States</Location>
<IsCurrent>true</IsCurrent>
<Url>
https://currentops.com/installations/us/al/horace-b-hanson-usarc
</Url>
<Id>us/al/horace-b-hanson-usarc</Id>
</LocationObject>
</Locations>
<HigherHq>
<HigherHqObject>
<Url>
https://currentops.com/unit/us/army/1-army/div-east
</Url>
<Id>us/army/1-army/div-east</Id>
<DateRange>... - Present</DateRange>
<IsCurrent>true</IsCurrent>
<HHQAcronym>Division East, 1st Army</HHQAcronym>
<Name>Division East, 1st Army</Name>
</HigherHqObject>
<HigherHqObject>
<Url>https://currentops.com/unit/us/army/usarc</Url>
<Id>us/army/usarc</Id>
<DateRange>... - Present</DateRange>
<IsCurrent>true</IsCurrent>
<HHQAcronym>USARC</HHQAcronym>
<Type>ADCON</Type>
<Name>United States Army Reserve Command</Name>
</HigherHqObject>
</HigherHq>
<Children>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/1-msg</Url>
<FullName>1st Mobilization Support Group</FullName>
<Name>1st Mobilization Support Group</Name>
<Id>us/army/1-msg</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
</Children>
<UnitService>Army</UnitService>
<UnitComponent>Reserve</UnitComponent>
</CurrentOpsObject>";
    }
}
