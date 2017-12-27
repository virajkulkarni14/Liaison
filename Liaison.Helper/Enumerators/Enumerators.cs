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
        Division = 9,
        Battalion = 12,
    }

    public enum HigherHqType
    {
        Unknown,
        Alligned,
        Assigned,
    }
    public enum ServiceType
    {
        Active = 1,
        Reserve,//B. Army Reserve, US Army Reserve
        Volunteer, // TA, ANG
        StateVolunteer, // Local Defence
    }
}
