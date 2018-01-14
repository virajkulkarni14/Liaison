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
        public const string HH = "Headquarters and Headquarters";

        public const string Command = "Command";
        public const string FieldArmy = "Army";
        public const string Corps = "Corps";
        public const string Division = "Division";
        public const string Brigade = "Brigade";
        public const string BrigadeCT = "Brigade Combat Team";
        public const string Company = "Company";
        public const string Battalion = "Battalion";
        public const string Detachment = "Detachment";

        public const string BattalionHHB = HH + " "+ Battalion;
        public const string DetachmentHHD = HH + " " + Detachment;

    }
    public class ShortForm
    {
        public const char Army = 'G';
        public const char Fleet = 'N';
        public const char AirForce = 'A';
        public const char Marine = 'M';

        public const char Reserve = 'R';
        public const char Volunteer = 'V';

        //CA
        public const string Infantry = "Inf";
        public const string Armoured = "Arm";
        public const string Cavalry = "Cav";
        public static string HHD = "HHD";

        //CSA

        //CSSA
        public const string ConSupt = "Con. Supt";

        public const string FieldArmy = "Army";
        public const string Corps = "Corps";
        public const string Division = "Div";
        public const string Brigade = "Bde";
        public const string BrigadeCT = "BCT";
        public const string Company = "Coy";
        public const string Battalion = "Bn";
        public const string Detachment = "Det";

        
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
