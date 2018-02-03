using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.Helper.Constants
{
    public class LongForm
    {
        public const string Battlegroup = "Battlegroup";

        public const string Infantry = "Infantry";
        public const string Armoured = "Armoured";
        public const string Cavalry = "Cavalry";
        public const string Artillery = "Artillery";
        public const string HH = "Headquarters and Headquarters";
        public const string HS = "Headquarters and Support";

        public const string Command = "Command";
        public const string FieldArmy = "Army";
        public const string Corps = "Corps";
        public const string Division = "Division";
        public const string Brigade = "Brigade";
        public const string BrigadeCT = "Brigade Combat Team";
        public const string Group = "Group";
        public const string Battalion = "Battalion";
        public const string Company = "Company";
        public const string Troop = "Troop";
        public const string Battery = "Battery";
        public const string Detachment = "Detachment";

        public const string BattalionHHB = HH + " " + Battalion;
        public const string BatteryHHB = HH + " " + Battery;
        public const string DetachmentHHD = HH + " " + Detachment;

        public const string FieldArtillery = "Field " + Artillery;
    }
    public class Initials
    {
        public static string HH = "HH";
        public static string HS = "HS";

        public static string HHB = HH + ShortForm.Battalion.First().ToString();
        public static string HHBty = HH + ShortForm.Battery.First().ToString();
        public static string HHC = HH + ShortForm.Company.First().ToString();
        public static string HHS = HH + ShortForm.Squadron.First().ToString();
        public static string HHD = HH + ShortForm.Detachment.First().ToString();

        public static string HSC = HS + ShortForm.Company.First().ToString();
        public static string HSS = HS + ShortForm.Squadron.First().ToString();
        public static string HSBty = HS + ShortForm.Battery.First().ToString();

    }
    public class Initials_Service
       {
        public const string Army = "G";
        public const string Fleet = "N";
        public const string AirForce = "A";
        public const string Marine = "M";
    }
    public class Initials_Component
    {
        public const string Reserve = "R";
        public const string Volunteer = "V";
    }
    public class ShortForm
    {     
        public const string HH = "HQ & HQ";
        public const string HS = "HQ & Supt";

        public static string HHC = HH + " " + Company;
        public static string HHS = HH + " " + Squadron;
        public static string HHBty = HH + " " + Battery;
        public static string HHD = HH + " " + Detachment;

        public static string HSBty = HS + ". " + Battery;
        public static string HSC = HS + ". " + Company;
        public static string HSS = HS + ". " + Squadron;

        //CA
        public const string Infantry = "Inf";
        public const string Armoured = "Arm";
        public const string Cavalry = "Cav";
        public const string Artilery = "Art";
        public const string ArtilleryField = "Fld. " + Artilery;



        //CSA

        //CSSA
        public const string GSC = "GSC";
        public const string Support = "Supt";
        public const string ConSupt = "Con. " + Support;
        public const string MobilisationSupt = "Mob. " + Support;
        public string RemoveStop(string incoming)
        {
            return incoming.Replace(",", "");
        }

        // 
        public const string FieldArmy = "Army";
        public const string Corps = "Corps";
        public const string Division = "Div";
        public const string Brigade = "Bde";
        public const string BrigadeCT = "BCT";
        public const string Group = "Grp";
        public const string Battalion = "Bn";
        public const string Company = "Coy";
        public const string Squadron = "Sqn";
        public const string Battery = "Bty";
        public const string Detachment = "Det";

        
    }

    public class Symbol
    {
        public const string ucc = "\"";
        public const string namedfleet = "$", command = "$";
        public const string armygroup = "%";
        public const string fieldarmy = "&";
        public const string numberedflt = "(", corps = "(", nmbrdairforce = "(", mef = "(";
        public const string district = ")", division = ")", taskforce = ")";
        public const string flotilla = "*", brigade = "*", airbase = "*";
        public const string fleetsqn = "/", regiment = "/",  wing = "/";
        public const string sqn = "@", battalion = "@";
        public const string flight = "|", company = "|", battery = "|";
        public const string hq = "!";
        public const string det = "?";
    }
}
