using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.Helper
{
    public static class GlobalSecurityHelper
    {
        public static readonly string gsec = "https://www.globalsecurity.org/military/agency/";
    }
    public static class RallyPointHelper
    {
        public static readonly string rpoint = "https://www.rallypoint.com"; // do not add closing /'/'!!!
        public static readonly string rpointunits = "https://www.rallypoint.com/units/";
        public static readonly string rpointlocs = "https://www.rallypoint.com/locations/";
    }
    public static class CurrentOpsHelper
    {
        public static readonly string ctops = "https://currentops.com/unit/";
        public static readonly string ctopsUSArmy = "https://currentops.com/unit/us/army/";

        public static readonly string USArmy = "U.S. Army";
        public static readonly string USAF = "USAF";
        public static readonly string USN = "USN";
        public static readonly string USMC = "USMC";
    }
}
