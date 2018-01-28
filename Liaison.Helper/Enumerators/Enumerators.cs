using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.Helper.Enumerators
{
    public enum Services
    {
        Unknown,
        Navy,
        Army,
        AirForce,
        Marines,
        Joint

    }
    public enum UnitType
    {
        Command = 1,
        Detachment = 4,
        FieldArmy = 7,
        Division = 9,
        Brigade = 10,
        Battalion = 12,
        Company = 13,
        Battery = 14,
        SquadronCavalry = 15,
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
        Unknown,
        Active,
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
    public enum USState
    {
        Alabama=1,
        Alaska, 
        Arizona,
        Arkansas,
        California,
        Colorado,
        Connecticut,
        Delaware,
        Florida,
        Georgia,
        Hawaii,
        Idaho,
        Illinois,
        Indiana,
        Iowa,
        Kansas,
        Kentucky,
        Louisiana,
        Maine,
        Maryland,
        Massachusetts,
        Michigan,
        Minnesota,
        Mississippi,
        Missouri,
        Montana,
        Nebraska,
        Nevada,
        NewHampshire,
        NewJersey,
        NewMexico,
        NewYork,
        NorthCarolina,
        NorthDakota,
        Ohio,
        Oklahoma,
        Oregon,
        Pennsylvania,
        RhodeIsland,
        SouthCarolina,
        SouthDakota,
        Tennessee,
        Texas,
        Utah,
        Vermont,
        Virginia,
        Washington,
        WestVirginia,
        Wisconsin,
        Wyoming,
        PuertoRico,
        Guam,
        DistrictColumbia,



    }
}
