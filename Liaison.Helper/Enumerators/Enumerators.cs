using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.Helper.Enumerators
{
    public enum Services
    {
        Navy = 1,
        Army,
        AirForce,
        Marines,
        Joint

    }
    public enum UnitType
    {
        Detachment = 4,
        FieldArmy =7,
        Division = 9,
        Brigade = 10,
        Battalion = 12,
        Company = 13,

    }

    public enum HigherHqType
    {
        Unknown=1,
        Organic,
        Alligned,
        Assigned,
        
    }
    public enum ServiceType
    {
        Active = 1,
        AC_RC,
        Reserve,//B. Army Reserve, US Army Reserve
        Volunteer, // TA, ANG
        StateVolunteer, // Local Defence
    }
    public enum ShortFormType
    {
        ShortName = 1, // ___1 Inf. Div.
        IndexName=2, // INF)___1
        Other=3// ___1 ID
    }
}
