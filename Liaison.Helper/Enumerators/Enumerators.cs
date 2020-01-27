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
        
        RAFFlyingTraining = 1230,

        NumberedAirForces = 5289,
        RAFFlyingBranch = 1216,
        RAFMS = 3,
        RAFReserveMedical = 1234,
        RAuxAFMedical = 1249,
        RAFMSAeromedicalDental = 36,
        RAFMSAeromedicalEvacuation = 18337,
        RAFRMSAeromedicalEvacuation = 18344,
        RAFMSAerospaceMedicine = 38,
        RAFRMSAerospaceMedicine = 64,
        RAFRMSAeromedicalStaging = 63,
        RAFMSDiagnosticsTheraputics = 14326,
        RAFMSDental = 37,
        RAFMSInpatientOps = 14328,
        RAFMSOperations = 9,
        RAFMSSupport = 10,
        RAFMSSurgicalOperations = 8299,
        RAFMaintenance = 11,
        RAFReserveMaintenance = 1233,
        RAuxAFMaintenance = 1248,
        RAFMaintenanceAMX = 13,
        RAFReserveMaintenanceAMX = 1237,
        RAuxAFMaintenanceAMX = 1251,
        RAFCommoditiesMaintenance = 13321,
        RAFMaintenanceCommsMX = 18339,
        RAFElectronicsMaintenance = 13322,
        RAFMaintenanceEquipmentMX = 40,
        RAFMaintenanceMaterial = 18335,
        RAFMaintenanceMunitions = 14,
        RAFMaintenanceOperations = 15,
        RAFReserveMaintOperations = 1235,
        RAuxAFMaintOps = 1250,
        RAFMaintenanceSupport = 1273,
        RAFSoftwareMaintenance = 13323,
        RAFMsnSupt = 16,
        RAFReserveMissionSupport = 1232,
        RAFRRegionalSupport = 54,
        RAuxAFMissionSupport = 1246,
        RAFAircraftSustainment = 13324,
        RAFCivilEngineeringConstructionTraining = 18342,
        RAFCivilEngineering = 4,
        RAFRCivilEngineer = 65,
        RAuxAFCivilEngineer = 1254,
        RAFAirSpaceComms = 18341,
        RAuxAFTacticalCommunications = 1258,
        RAFTacticalCommunications = 46,
        RAFComputerSystems = 5294,
        RAFCommunications = 17,
        RAFRCommuniations = 1238,
        RAuxAFCommunications = 1253,
        RAFCommunicationsSupport = 5292,
        RAFContracting = 18,
        RAFRContracting = 20349,
        RAFComptroller = 7,
        RAuxAFComptroller = 57,
        RAFCombatSustainment = 13325,
        RAFCombatCamera = 18346,
        RAFRCombatCamera = 18347,
        RAuxAFEngineeringInstallation = 1257,
        RAFForceSupport = 19,
        RAFRForceSupport = 1239,
        RAuxAFForceSupport = 61,
        RAFRAirliftControl = 18345,
        RAFAirMobility = 44,
        RAFLogisticsAerialPort = 62,
        RAFRAerialPort = 18348,
        RAFLogisticsReadiness = 20,
        RAFRLogisticsReadiness = 1240,
        RAuxAFLogisticsReadiness = 1252,
        RAFMobilityResponse = 45,
        RAFLogisticsReadinessVehicle = 18336,
        RAFAirPostal = 18334,
        RAFRSecurityForces = 66,
        RAFSecurityForces = 21,
        RAuxAFSecurityForces = 59,
        RAuxAFSupport = 11310,
        RAFNuclearCorps = 15328,
        RAFNuclearMunitionsMaintenance = 15330,
        RAFNuclearMunitionsSupport = 15329,
        RAFOpsAirBase = 48,
        RAuxAFAirBase = 51,
        RAFAirControl = 1222,
        RAuxAFAirControl = 1269,
        RAFAirGroundOperations = 10304,

        //AIR/OPS
        RAFAirOperationsCentres = 1264,
        RAFAirOperations = 41,
        RAuxAFAirOperations = 1256,
        RAFCommandandControl = 6294,        
        RAFCombatSupport = 15327,
        RAFInformationOperations = 14324,
        RAuxAFInfoOps = 14325,
        RAFCyberspaceOperations = 11305,
        RAuxAFCyberspaceOperations = 11307,
        RAFFlying = 5290,
        RAFFlyingAirlift = 10302,
        RAFRFlyingAirlift = 13316,
        RAuxAFFlyingAirlift = 13317,
        RAFFlyingAbnAirControl = 10305,
        RAuxAFFlyingAbnAirControl = 13320,
        RAFFlyingAirMobility = 18349,
        RAFRFlyingAirMobility = 18350,
        RAuxAFFlyingAirMobility = 18351,
        RAFRFlyingAirMobilityOperations = 18354,
        RAFFlyingAirRefueling = 15326,
        RAFRFlyingAirRefueling = 18352,
        RAuXAFFlyingAirRefueling = 18353,
        RAFFlyingAttack = 14322,
        RAuxAFFlyingAttack = 14321,
        RAFFlyingFighter = 1221,
        RAuxAFFlyingFighter = 11308,
        RAFFlyingFighterTraining = 13319,
        RAuxAFFlyingFighterTraining = 14323,
        RAFFlyingFlightTest = 1225,
        RAFFlyingHelicopter = 8302,
        RAFFlyingReconnaissance = 18343,
        RAFFlyingRange = 11314,
        RAFFlyingRescue = 1229,
        RAFFlyingSpecialOperations = 24,
        RAFReserveFlyingSpecialOperations = 1231,
        RAuxAFFlyingSpecialOperations = 1244,
        RAuxAFAirSupportOps = 1255,
        RAFSpecialTacticsSupport = 1204,
        RAFIntelligence = 1205,
        RAuxAFIntelligence = 11304,
        RAuxAFOpsRegionalSupport = 1245,
        RAFSpecOpsAdvancedCapabilities = 1266,
        RAFTestandEvaluation = 6292,
        RAFTraining = 1226,
        RAuxAFTraining = 1267,
        RAFTrainingCombatTraining = 35,
        RAFTrainingFlyingTraining = 1230,
        RAFRTrainingFlyingTraining = 19349,
        RAFTrainingTrainingSupport = 1228,
        RAFTrainingTrainingSystems = 6293,
        RAuxAFWeather = 1260,
        RAFOperationsSupport = 8,
        RAFROperationsSupport = 1243,
        RAuxAFOperationsSupport = 1261,
        RAFMusic = 8301,
        RAuxAFMusic = 1259,

        //AIR/INF
        RAFRegiment = 23,
        RAFRRegiment = 1173,
        RAuxAFRegiment = 52,

        //AIR/POL
        RAFPolice= 22,
        RafrPolice= 1242,
        RAuxAFPolice= 53,

        
        

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
        Expeditionary = 6,
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
