using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.Biz.Tests.Strings
{
    public class DetachmentString
    {
        internal const string army_1_div_east_hhd= @"<CurrentOpsObject>
<Url>
https://currentops.com/unit/us/army/1-army/div-east/hhd
</Url>
<NameNode>
HHD, Div East, 1st Army U.S. Army
</NameNode>
<FullName>HHD, Div East, 1st Army, U.S. Army</FullName>
<SplitName>HHD, Div East, 1st Army</SplitName>
<Service>U.S. Army</Service>
<LogoUrl>
https://currentops.com/img/page-header-img/c3NpL1VTIEFSTVkgQVJNWSAwMDAx.
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
<Url>
https://currentops.com/unit/us/army/1-army/div-east
</Url>
<Id>us/army/1-army/div-east</Id>
<DateRange/>
<IsCurrent>false</IsCurrent>
<HHQAcronym>Division East, 1st Army</HHQAcronym>
<Type>Organic</Type>
<Name>Division East, 1st Army</Name>
</HigherHqObject>
</HigherHq>
<Children/>
</CurrentOpsObject>
";
    }
}
