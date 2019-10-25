using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.Helper.Enumerators
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum UnitRankSymbol
    {
        Detachment = '?',
        XXXXXXX = '"',
        XXXXXX = '$',
        XXXXX = '%',
        XXXX = '&',
        XXX = '(',
        XX = ')',
        X = '*',
        III = '/',
        II = '@',
        I = '|',
        ooo = '¦',
        oo = '+'
    }

    public enum RelationshipTypeBll
    {
        Organic = 1,
        Assigned = 2,
        Attached = 3,
        Opcon = 4,
        Tacon = 5,
        Adcon = 6,
        Detached = 7,
        TaskForce = 8
    }

    public enum ServicesBll
    {
        Unknown = 0,
        Navy = 1,
        Army = 2,
        AirForce = 3,
        Marines = 4,
        CoastGuard = 6,
        FleetAuxiliary = 7,
        Joint = 8,
    }

    public enum UnitType
    {
        Command = 1,
        Detachment = 4,
        FieldArmy = 7,
        Division = 9,
        Brigade = 10,
        Group = 11,
        Battalion = 12,
        Company = 13,
        Battery = 14,
        SquadronCavalry = 15,
    }

    public enum AdminCorps
    {
        EngineerInChief = 114,
        OffCivilAffairs = 170,

        //SF
        NavalSpecialWarfare = 1198,
        DGSpecialForces = 112,
        SpecialAirService = 1201,
        RoyalMarineCommando = 175,

        //N/AIR
        NavalAviation = 78,
        FleetArmArm = 1,

        //G/LAND/CA/AVN
        ArmyAirCorps = 1195,
        ArmyAirCorpsBn = 1196,

        //G/LAND/CA/SF/SAS

        //AIR/FLY
        RAFTraining = 35,
        RAFFlyingTraining = 1230,

        //AIR/INF
        RAFRegiment = 23,
        RAFRRegiment = 1173,
        RAuxAFRegiment = 52,

        //AIR/OPS
        RAFAirOperationsCentres = 1264,

        //M
        MAGTF = 1277,
        RoyalMarineLandForces = 71,
        CorpsOfRoyalMarines = 74,
        RoyalMarinesReserve = 3289,

        //M/C2
        MarineHeadquartersGroups = 3278,

        //M/LAND/CAV

        RoyalMarineCavalry = 3283,
        RoyalMarineCavalryReserve = 4286,

        //M/LAND/INF
        RoyalMarineLightInfantry = 75,
        RRM10 = 1278,

        //M/LAND/RMA
        RoyalMarineArtillery = 72,
        RoyalMarinesAirArmAirDefence = 3276,
        RoyalNavalMedicalService = 3277,

        //M/AIR
        RoyalMarineAviation = 68,
        RoyalMarinesAirArm = 76,
        RoyalMarineReserveAirArm = 5286,
        RoyalIndianMarinesAirArm = 77,

        //M/CS
        RoyalMarineIntelligence = 1280,
        RoyalMarineCommunications = 1281,
        RoyalMarinesEngineers = 3279,
        RoyalMarinesEngineersReserve = 4287,

        //M/CSS
        RoyalMarineLogistics = 73,
        RoyalMarinesReserveLogistics = 5287,
        RoyalMarinesMilitaryPolice = 1282,
        RMRCivilAffairs = 3286,
    }


    public enum HigherHqType
    {
        Unknown = 0,
        Organic = 1,
        Assigned = 2,
        Attached = 3,
        OPCON = 4,
        TACON = 5,
        ADCON = 6,
        Detached = 7,
        TaskForce = 8
    }

    public enum ServiceTypeBLL
    {
        Unknown,
        Active = 1,
        AC_RC = 2,
        Reserve = 3, //B. Army Reserve, US Army Reserve
        Volunteer = 4, // TA, ANG
        StateVolunteer = 5, // Local Defence
    }

    public enum ShortFormType
    {
        ShortName = 1, // ___1 Inf. Div.
        IndexName = 2, // INF)___1
        Other = 3,
        Placeholder = 4 // ___1 ID

    }

    public enum USState
    {
        Alabama = 1,
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
