using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.Biz.Tests.Strings
{
   public static class DivisionString
    {
        public static string army_1_div_west = @"<CurrentOpsObject>
<Url>
https://currentops.com/unit/us/army/1-army/div-west
</Url>
<NameNode>
Division West, 1st Army U.S. Army
</NameNode>
<FullName>Division West, 1st Army, U.S. Army</FullName>
<SplitName>Division West, 1st Army</SplitName>
<Service>U.S. Army</Service>
<LogoUrl>
https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgQVJNWSAwMDAx.png
</LogoUrl>
<Locations>
<LocationObject>
<DateRange>... - Present</DateRange>
<BaseName>Fort Hood</BaseName>
<Deployment>false</Deployment>
<Location>Killeen, Texas, United States</Location>
<IsCurrent>true</IsCurrent>
<Url>
https://currentops.com/installations/us/tx/fort-hood
</Url>
<Id>us/tx/fort-hood</Id>
</LocationObject>
</Locations>
<HigherHq>
<HigherHqObject>
<Url>https://currentops.com/unit/us/army/1-army</Url>
<Id>us/army/1-army</Id>
<DateRange>... - Present</DateRange>
<IsCurrent>true</IsCurrent>
<HHQAcronym>1st Army</HHQAcronym>
<Type>Assigned</Type>
<Name>1st Army</Name>
</HigherHqObject>
</HigherHq>
<Children>
<SubUnitObject>
<Url>
https://currentops.com/unit/us/army/1-army/div-west/hhd
</Url>
<FullName>
Headquarters and Headquarters Detachment, Division West, 1st Army
</FullName>
<Name>Headquarters and Headquarters Detachment</Name>
<Id>us/army/1-army/div-west/hhd</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/5-armored-bde</Url>
<FullName>5th Armored Brigade</FullName>
<Name>5th Armored Brigade</Name>
<Id>us/army/5-armored-bde</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/120-in-bde</Url>
<FullName>120th Infantry Brigade</FullName>
<Name>120th Infantry Brigade</Name>
<Id>us/army/120-in-bde</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/181-in-bde</Url>
<FullName>181st Infantry Brigade</FullName>
<Name>181st Infantry Brigade</Name>
<Id>us/army/181-in-bde</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/189-in-bde</Url>
<FullName>189th Infantry Brigade</FullName>
<Name>189th Infantry Brigade</Name>
<Id>us/army/189-in-bde</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/402-fa-bde</Url>
<FullName>402nd Field Artillery Brigade</FullName>
<Name>402nd Field Artillery Brigade</Name>
<Id>us/army/402-fa-bde</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/166-avn-bde</Url>
<FullName>166th Aviation Brigade</FullName>
<Name>166th Aviation Brigade</Name>
<Id>us/army/166-avn-bde</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/85-arsc</Url>
<FullName>85th Army Reserve Support Command</FullName>
<Name>
85th Army Reserve Support Command (USAR)
</Name>
<Id>us/army/85-arsc</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
</Children>
<UnitService>Army</UnitService>
<UnitComponent>Active</UnitComponent>
</CurrentOpsObject>";
        public static string army_1_div_east = @"<CurrentOpsObject>
<Url>
https://currentops.com/unit/us/army/1-army/div-east
</Url>
<NameNode>
Division East, 1st Army U.S. Army
</NameNode>
<FullName>Division East, 1st Army, U.S. Army</FullName>
<SplitName>Division East, 1st Army</SplitName>
<Service>U.S. Army</Service>
<LogoUrl>
https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgQVJNWSAwMDAx.png
</LogoUrl>
<Locations>
<LocationObject>
<DateRange>... - Present</DateRange>
<BaseName>Fort George G. Meade</BaseName>
<Deployment>false</Deployment>
<Location>Maryland, United States</Location>
<IsCurrent>true</IsCurrent>
<Url>
https://currentops.com/installations/us/md/fort-george-g-meade
</Url>
<Id>us/md/fort-george-g-meade</Id>
</LocationObject>
</Locations>
<HigherHq>
<HigherHqObject>
<Url>https://currentops.com/unit/us/army/1-army</Url>
<Id>us/army/1-army</Id>
<DateRange>... - Present</DateRange>
<IsCurrent>true</IsCurrent>
<HHQAcronym>1st Army</HHQAcronym>
<Type>Assigned</Type>
<Name>1st Army</Name>
</HigherHqObject>
</HigherHq>
<Children>
<SubUnitObject>
<Url>
https://currentops.com/unit/us/army/1-army/div-east/hhd
</Url>
<FullName>
Headquarters and Headquarters Detachment, Division East, 1st Army
</FullName>
<Name>Headquarters and Headquarters Detachment</Name>
<Id>us/army/1-army/div-east/hhd</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/4-cav-bde</Url>
<FullName>4th Cavalry Brigade</FullName>
<Name>4th Cavalry Brigade</Name>
<Id>us/army/4-cav-bde</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>
https://currentops.com/unit/us/army/177-armored-bde
</Url>
<FullName>177th Armored Brigade</FullName>
<Name>177th Armored Brigade</Name>
<Id>us/army/177-armored-bde</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/157-in-bde</Url>
<FullName>157th Infantry Brigade</FullName>
<Name>157th Infantry Brigade</Name>
<Id>us/army/157-in-bde</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/158-in-bde</Url>
<FullName>158th Infantry Brigade</FullName>
<Name>158th Infantry Brigade</Name>
<Id>us/army/158-in-bde</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/174-in-bde</Url>
<FullName>174th Infantry Brigade</FullName>
<Name>174th Infantry Brigade</Name>
<Id>us/army/174-in-bde</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/188-in-bde</Url>
<FullName>188th Infantry Brigade</FullName>
<Name>188th Infantry Brigade</Name>
<Id>us/army/188-in-bde</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/205-in-bde</Url>
<FullName>205th Infantry Brigade</FullName>
<Name>205th Infantry Brigade</Name>
<Id>us/army/205-in-bde</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/72-fa-bde</Url>
<FullName>72nd Field Artillery Brigade</FullName>
<Name>72nd Field Artillery Brigade</Name>
<Id>us/army/72-fa-bde</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
<SubUnitObject>
<Url>https://currentops.com/unit/us/army/87-arsc</Url>
<FullName>87th Army Reserve Support Command</FullName>
<Name>
87th Army Reserve Support Command (USAR)
</Name>
<Id>us/army/87-arsc</Id>
<IsIndirect>false</IsIndirect>
</SubUnitObject>
</Children>
<UnitService>Army</UnitService>
<UnitComponent>Active</UnitComponent>
</CurrentOpsObject>";
        public static string infantrydivision_38id = @"<CurrentOpsObject><Url>https://currentops.com/unit/us/army/38-id</Url><NameNode>38th Infantry Division
                            U.S. Army</NameNode><FullName>38th Infantry Division, U.S. Army</FullName><SplitName>38th Infantry Division</SplitName><Service>U.S. Army</Service><LogoUrl>https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgSUQgMDAzOA.png</LogoUrl><Locations><LocationObject><DateRange>... - Present</DateRange><BaseName>Division Armory</BaseName><Deployment>false</Deployment><Location>Indianapolis, Indiana, United States</Location><IsCurrent>true</IsCurrent><Url>https://currentops.com/installations/us/in/division-armory</Url><Id>us/in/division-armory</Id></LocationObject></Locations><HigherHq><HigherHqObject><Url>https://currentops.com/unit/us/army/jfhq-in/arng-elt</Url><Id>us/army/jfhq-in/arng-elt</Id><DateRange/><IsCurrent>false</IsCurrent><HHQAcronym>ARNG Elt, JFHQ Indiana</HHQAcronym><Name>Army National Guard Element, Joint Force Headquarters Indiana</Name></HigherHqObject></HigherHq><Children><SubUnitObject><Url>https://currentops.com/unit/us/army/38-id/hhbn</Url><FullName>Headquarters and Headquarters Battalion, 38th Infantry Division</FullName><Name>Headquarters and Headquarters Battalion</Name><Id>us/army/38-id/hhbn</Id><IsIndirect>false</IsIndirect></SubUnitObject><SubUnitObject><Url>https://currentops.com/unit/us/army/76-ibct</Url><FullName>76th Infantry Brigade Combat Team</FullName><Name>76th Infantry Brigade Combat Team</Name><Id>us/army/76-ibct</Id><IsIndirect>false</IsIndirect></SubUnitObject><SubUnitObject><Url>https://currentops.com/unit/us/army/37-ibct</Url><FullName>37th Infantry Brigade Combat Team</FullName><Name>37th Infantry Brigade Combat Team

                    (OH ARNG)
        
                    
        
                                    (Aligned)</Name><Id>us/army/37-ibct</Id><IsIndirect>false</IsIndirect></SubUnitObject><SubUnitObject><Url>https://currentops.com/unit/us/army/278-acr</Url><FullName>278th Armored Cavalry Regiment</FullName><Name>278th Armored Cavalry Regiment

                    (TN ARNG)
        
                    
        
                                    (Aligned)</Name><Id>us/army/278-acr</Id><IsIndirect>false</IsIndirect></SubUnitObject><SubUnitObject><Url>https://currentops.com/unit/us/army/38-id/cab</Url><FullName>Combat Aviation Brigade, 38th Infantry Division</FullName><Name>Combat Aviation Brigade</Name><Id>us/army/38-id/cab</Id><IsIndirect>false</IsIndirect></SubUnitObject><SubUnitObject><Url>https://currentops.com/unit/us/army/138-fib</Url><FullName>138th Fires Brigade</FullName><Name>138th Fires Brigade

                    (KY ARNG)
        
                    
        
                                    (Aligned)</Name><Id>us/army/138-fib</Id><IsIndirect>false</IsIndirect></SubUnitObject><SubUnitObject><Url>https://currentops.com/unit/us/army/219-bfsb</Url><FullName>219th Battlefield Surveillance Brigade</FullName><Name>219th Battlefield Surveillance Brigade</Name><Id>us/army/219-bfsb</Id><IsIndirect>false</IsIndirect></SubUnitObject><SubUnitObject><Url>https://currentops.com/unit/us/army/38-sb</Url><FullName>38th Sustainment Brigade</FullName><Name>38th Sustainment Brigade</Name><Id>us/army/38-sb</Id><IsIndirect>false</IsIndirect></SubUnitObject><SubUnitObject><Url>https://currentops.com/unit/us/army/38-mp-co</Url><FullName>38th Military Police Company</FullName><Name>38th Military Police Company</Name><Id>us/army/38-mp-co</Id><IsIndirect>false</IsIndirect></SubUnitObject></Children><UnitService>Army</UnitService><UnitComponent>Volunteer</UnitComponent><UnitNGState>IN</UnitNGState></CurrentOpsObject>";
       public static string infantrydivision_1id = @"<CurrentOpsObject><Url>https://currentops.com/unit/us/army/1-id</Url><NameNode>1st Infantry Division
                            U.S. Army</NameNode><FullName>1st Infantry Division, U.S. Army</FullName><SplitName>1st Infantry Division</SplitName><Service>U.S. Army</Service><LogoUrl>https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgSUQgMDAwMQ.png</LogoUrl><Locations><LocationObject><DateRange>2006 - Present</DateRange><BaseName>Fort Riley</BaseName><Deployment>false</Deployment><Location>Junction City, Kansas, United States</Location><IsCurrent>true</IsCurrent><Url>https://currentops.com/installations/us/ks/fort-riley</Url><Id>us/ks/fort-riley</Id></LocationObject><LocationObject><DateRange>... - 2006</DateRange><BaseName>Leighton Barracks</BaseName><Deployment>false</Deployment><Location>Würzburg, Bavaria, Germany</Location><IsCurrent>false</IsCurrent><Url>https://currentops.com/installations/de/by/leighton-bks</Url><Id>de/by/leighton-bks</Id></LocationObject></Locations><HigherHq><HigherHqObject><Url>https://currentops.com/unit/us/army/usaraf</Url><Id>us/army/usaraf</Id><DateRange>... - Present</DateRange><IsCurrent>true</IsCurrent><HHQAcronym>USARAF</HHQAcronym><Type>Aligned</Type><Name>United States Army Africa</Name></HigherHqObject><HigherHqObject><Url>https://currentops.com/unit/us/army/forscom</Url><Id>us/army/forscom</Id><DateRange>... - Present</DateRange><IsCurrent>true</IsCurrent><HHQAcronym>FORSCOM</HHQAcronym><Type>Assigned</Type><Name>United States Army Forces Command</Name></HigherHqObject><HigherHqObject><Url>https://currentops.com/unit/us/army/v-corps</Url><Id>us/army/v-corps</Id><DateRange>... - 2006</DateRange><IsCurrent>false</IsCurrent><HHQAcronym>V Corps</HHQAcronym><Name>V Corps</Name></HigherHqObject></HigherHq><Children><SubUnitObject><Url>https://currentops.com/unit/us/army/1-id/hhbn</Url><FullName>Headquarters and Headquarters Battalion, 1st Infantry Division</FullName><Name>Headquarters and Headquarters Battalion</Name><Id>us/army/1-id/hhbn</Id><IsIndirect>false</IsIndirect></SubUnitObject><SubUnitObject><Url>https://currentops.com/unit/us/army/1-id/1-bct</Url><FullName>1st Brigade Combat Team, 1st Infantry Division</FullName><Name>1st Brigade Combat Team</Name><Id>us/army/1-id/1-bct</Id><IsIndirect>false</IsIndirect></SubUnitObject><SubUnitObject><Url>https://currentops.com/unit/us/army/1-id/2-bct</Url><FullName>2nd Brigade Combat Team, 1st Infantry Division</FullName><Name>2nd Brigade Combat Team</Name><Id>us/army/1-id/2-bct</Id><IsIndirect>false</IsIndirect></SubUnitObject><SubUnitObject><Url>https://currentops.com/unit/us/army/1-id/4-bct</Url><FullName>4th Brigade Combat Team, 1st Infantry Division</FullName><Name>4th Brigade Combat Team</Name><Id>us/army/1-id/4-bct</Id><IsIndirect>false</IsIndirect></SubUnitObject><SubUnitObject><Url>https://currentops.com/unit/us/army/1-id/cab</Url><FullName>Combat Aviation Brigade, 1st Infantry Division</FullName><Name>Combat Aviation Brigade</Name><Id>us/army/1-id/cab</Id><IsIndirect>false</IsIndirect></SubUnitObject><SubUnitObject><Url>https://currentops.com/unit/us/army/75-fa-bde</Url><FullName>75th Field Artillery Brigade</FullName><Name>75th Field Artillery Brigade</Name><Id>us/army/75-fa-bde</Id><IsIndirect>false</IsIndirect></SubUnitObject><SubUnitObject><Url>https://currentops.com/unit/us/army/1-sb</Url><FullName>1st Sustainment Brigade</FullName><Name>1st Sustainment Brigade</Name><Id>us/army/1-sb</Id><IsIndirect>false</IsIndirect></SubUnitObject></Children><UnitService>Army</UnitService><UnitComponent>Active</UnitComponent></CurrentOpsObject>";
    }
}
