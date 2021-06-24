using System;
using System.Collections.Generic;
using System.Text;

namespace SystemRD1.Domain.Validations.Documents
{
    public class Utils
    {
        public static string OnlyNumber(string value)
        {
            var number = "";
            foreach (var s in value)
            {
                if (char.IsDigit(s))
                {
                    number += s;
                }
            }
            return number.Trim();
        }
    }
}
