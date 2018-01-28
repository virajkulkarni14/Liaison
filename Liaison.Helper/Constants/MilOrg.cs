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
    public class ShortForm_Service
       {
        public const char Army = 'G';
        public const char Fleet = 'N';
        public const char AirForce = 'A';
        public const char Marine = 'M';
    }
    public class ShortForm_Component
    {
        public const char Reserve = 'R';
        public const char Volunteer = 'V';
    }
    public class ShortForm
    {     
        //CA
        public const string Infantry = "Inf";
        public const string Armoured = "Arm";
        public const string Cavalry = "Cav";
        public const string Artilery = "Art";

        public const string HH = "HQ & HQ";
        public const string HS = "HQ & Supt";

        public const string ArtilleryField = "Fld. " + Artilery;

        public static string HHC = HH + " " + Company;
        public static string HHS = HH + " " + Squadron;
        public static string HHBty = HH + " " + Battery;
        public static string HHD = HH + " " + Detachment;

        public static string HSBty = HS + ". " + Battery;
        public static string HSC = HS + ". " + Company;
        public static string HSS = HS + ". " + Squadron;

        //CSA

        //CSSA
        public const string ConSupt = "Con. Supt";
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
        public const string Company = "Coy";
        public const string Battalion = "Bn";
        public const string Detachment = "Det";
        public const string Squadron = "Sqn";
        public const string Battery = "Bty";

        
    }

    public class Symbol
    {
        public const char ucc = '"';
        public const char namedfleet = '$', command = '$';
        public const char armygroup = '%';
        public const char fieldarmy = '&';
        public const char numberedflt = '(', corps = '(', nmbrdairforce = '(', mef = '(';
        public const char district = ')', division = ')', taskforce = ')';
        public const char flotilla = '*', brigade = '*', airbase = '*';
        public const char fleetsqn = '/', regiment = '/', wing = '/';
        public const char sqn = '@', battalion = '@';
        public const char flight = '|', company = '|', battery = '|';
        public const char hq = '!';
        public const char det = '?';
    }
}
