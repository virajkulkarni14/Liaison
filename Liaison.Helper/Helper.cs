using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.Helper
{
    public class Helper
    {
        public static string GetIntWithUnderscores(string nmber, bool useOrdinal)
        {
            if (int.TryParse(nmber, out int result))
            {
                return GetIntWithUnderscores(int.Parse(nmber), useOrdinal);
            }
            else return null;
        }

        public static string GetIntWithUnderscores(int number, bool useOrdinal)
        {
            string append = useOrdinal ? Helper.AddOrdinal(number) : number.ToString();

            if (number < 10)
            {
                return "____" + append;
            }
            else if (number < 100)
            {
                return "___" + append;
            }
            else if (number < 1000)
            {
                return "__" + append;
            }
            else if (number < 10000)
            {
                return "_" + append;
            }
            else if (number < 100000)
            {
                return append.ToString();
            }
            throw new Exception("Number too big");
        }


        public static string AddOrdinal (string num)
        {
            return AddOrdinal(int.Parse(num));
        }
        public static string AddOrdinal(int num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }
        }

        public static string ConvertNumberToWord(int? number)
        {
            if (number.HasValue)
            {
                if (number > 100)
                {
                    throw new NotImplementedException();
                }
                else if (number > 20)
                {
                    var arr = number.Value.ToString().ToCharArray();
                    var tens = arr[0];
                    var units = arr[1];

                    switch (tens)
                    {
                        case '2': return ("Twenty-" + ConvertNumberToWord((int)units));
                        case '3': return ("Thirty-" + ConvertNumberToWord((int)units));
                        case '4': return ("Fourty-" + ConvertNumberToWord((int)units));
                        case '5': return ("Fifty-" + ConvertNumberToWord((int)units));
                        case '6': return ("Sixty-" + ConvertNumberToWord((int)units));
                        case '7': return ("Seventy-" + ConvertNumberToWord((int)units));
                        case '8': return ("Eighty-" + ConvertNumberToWord((int)units));
                        case '9': return ("Ninety-" + ConvertNumberToWord((int)units));
                    }
                }
                else
                {
                    switch (number)
                    {
                        case 1: return "First";
                        case 2: return "Second";
                        case 3: return "Third";
                        case 4: return "Fourth";
                        case 5: return "Fifth";
                        case 6: return "Sixth";
                        case 7: return "Seventh";
                        case 8: return "Eighth";
                        case 9: return "Ninth";
                        case 10: return "Tenth";
                        case 11: return "Eleventh";
                        case 12: return "Twelveth";
                        case 13: return "Thirteenth";
                        case 14: return "Fourteenth";
                        case 15: return "Fifteenth";
                        case 16: return "Sixteenth";
                        case 17: return "Seventeenth";
                        case 18: return "Eighteenth";
                        case 19: return "Nineteenth";
                        case 20: return "Twentyith";
                    }
                }
            }
            return string.Empty;
        }
    }
}
