using System;
using System.Collections.Generic;
using Liaison.BLL.Models.Equipment;
using Liaison.BLL.Models.Unit;
using Liaison.BLL.Models.Unit.Interfaces;
using Liaison.Data.Sql.Edmx;

namespace Liaison.BLL.Models
{
    public static class ResourceStrings
    {        
        public static string HQ = "Headquarters";
        public static string HQHQ = "Headquarters & Headquarters";
        public static string HQS = "Headquarters & Support";
        public static string HQSVC = "Headquarters & Service";
        public static string Weapons = "Weapons";
        public static string Chemical = "Chemical";
        public static string Det = "Det.";
        public static string Group = "Group";
        public static  string Seperator = ", ";

    }
    public static partial class ExtensionMethods
    {
        public static IEnumerable<IUnit> ToIUnits(this IEnumerable<Liaison.Data.Sql.Edmx.Unit> input)
        {
            return new List<IUnit>();
        }

        public static int? ToIntNull(this decimal? input)
        {
            if (input == null)
            {
                return null;
            }

            return (int) input.Value;
        }
        public static List<IEquipment> ToEquipmentList(this ICollection<Liaison.Data.Sql.Edmx.EquipmentOwner> eolist)
        {
            var returnable = new List<IEquipment>();
            foreach (var eo in eolist)
            {
                if (eo.Aircraft != null)
                {
                    returnable.Add(new BLLAircraft(eo));
                }
            }
            return returnable;
        }

        public static List<BLLBase> ToListBases(this IEnumerable<Base> bases)
        {
            return new List<BLLBase>();
        }

       
        public static string ToOrdinal(this int? input, bool useOrdinal)
        {
            if (input == null)
            {
                return "";
            }
            if (!useOrdinal)
            {
                return input.Value.ToString();
            }
            if (input <= 0) return input.ToString();

            switch (input % 100)
            {
                case 11:
                case 12:
                case 13:
                    return input + "th";
            }

            switch (input % 10)
            {
                case 1:
                    return input + "st";
                case 2:
                    return input + "nd";
                case 3:
                    return input + "rd";
                default:
                    return input + "th";
            }
        }


        public static string ToOrdinalAsWord(this int? input)
        {
            if (input == null)
            {
                return "";
            }

            if (input < 13)
            {
                if (input == 1) return "First";
                if (input == 2) return "Second";
                if (input == 3) return "Third";
                if (input == 4) return "Fourth";
                if (input == 5) return "Fifth";
                if (input == 6) return "Sixth";
                if (input == 7) return "Seventh";
                if (input == 8) return "Eighth";
                if (input == 9) return "Nineth";
                if (input == 10) return "Tenth";
                if (input == 11) return "Eleventh";
                if (input == 12) return "Twelveth";
            }
            else if (input < 20)
            {
                if (input == 13) return "Thirteenth";
                if (input == 14) return "Fourteenth";
                if (input == 15) return "Fifteenth";
                if (input == 16) return "Sixteenth";
                if (input == 17) return "Seventeenth";
                if (input == 18) return "Eighteenth";
                if (input == 19) return "Nineteenth";
            }
            else
            {
                if (input < 30)
                {
                    var x = input - 20;
                    if (x == 0)
                    {
                        return "Twentieth";
                    }
                    return "Twenty-" + x.ToOrdinalAsWord();
                }
                if (input < 40)
                {
                    var x = input - 30;
                    if (x == 0)
                    {
                        return "Thirtieth";
                    }
                    return "Thirty-" + x.ToOrdinalAsWord();
                }
                if (input < 50)
                {
                    var x = input - 40;
                    if (x == 0)
                    {
                        return "Fourtieth";
                    }
                    return "Fourty-" + x.ToOrdinalAsWord();
                }
                if (input < 60)
                {
                    var x = input - 50;
                    if (x == 0)
                    {
                        return "Fiftieth";
                    }
                    return "Fifty-" + x.ToOrdinalAsWord();
                }
                if (input < 70)
                {
                    var x = input - 60;
                    if (x == 0)
                    {
                        return "Sixtieth";
                    }
                    return "Sixty-" + x.ToOrdinalAsWord();
                }
                if (input < 80)
                {
                    var x = input - 70;
                    if (x == 0)
                    {
                        return "Seventieth";
                    }
                    return "Seventy-" + x.ToOrdinalAsWord();
                }
                if (input < 90)
                {
                    var x = input - 80;
                    if (x == 0)
                    {
                        return "Eightieth";
                    }
                    return "Eighty-" + x.ToOrdinalAsWord();
                }
                if (input < 100)
                {
                    var x = input - 90;
                    if (x == 0)
                    {
                        return "Ninetieth";
                    }
                    return "Ninety-" + x.ToOrdinalAsWord();
                }
            }
            



            throw new Exception("Number not done: "+input);
        }

        public static string ToRomanNumerals(this int? number)
        {
            if (number == null)
            {
                return "";
            }

            if ((number < 0) || (number > 3999))
                throw new ArgumentOutOfRangeException("insert value between 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRomanNumerals(number - 1000);
            if (number >= 900) return "CM" + ToRomanNumerals(number - 900);
            if (number >= 500) return "D" + ToRomanNumerals(number - 500);
            if (number >= 400) return "CD" + ToRomanNumerals(number - 400);
            if (number >= 100) return "C" + ToRomanNumerals(number - 100);
            if (number >= 90) return "XC" + ToRomanNumerals(number - 90);
            if (number >= 50) return "L" + ToRomanNumerals(number - 50);
            if (number >= 40) return "XL" + ToRomanNumerals(number - 40);
            if (number >= 10) return "X" + ToRomanNumerals(number - 10);
            if (number >= 9) return "IX" + ToRomanNumerals(number - 9);
            if (number >= 5) return "V" + ToRomanNumerals(number - 5);
            if (number >= 4) return "IV" + ToRomanNumerals(number - 4);
            if (number >= 1) return "I" + ToRomanNumerals(number - 1);

            throw new Exception("Number not done");
        }
    }
}