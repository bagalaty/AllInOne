using System.Collections.Generic;
using System.Globalization;

namespace Services.Extensions
{
    public static class NumberExtensions
    {
        public static string NumEnToAr(this string num)
        {
            var digits = new List<char>();
            var numDigits = num.ToCharArray();
            for (var i = 0; i < numDigits.Length; i++)
            {
                if (!char.IsDigit(numDigits[i]))
                    digits.Add(numDigits[i]);
                else
                    digits.Add((char)(int.Parse(numDigits[i].ToString()) + 1632));
            }
            return string.Join("", digits);
        }

        public static string NumArToEn(this string num)
        {
            var digits = new List<char>();
            var numDigits = num.ToCharArray();
            for (var i = 0; i < numDigits.Length; i++)
                digits.Add(char.IsDigit(numDigits[i]) ? (numDigits[i] - 1632).ToString()[0] : numDigits[i]);
            return string.Join("", digits);
        }
    }
}