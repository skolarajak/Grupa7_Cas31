using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Cas31.Libraries
{
    class Functions
    {
        Random rnd;

        public Functions()
        {
            this.rnd = new Random();
        }

        public string RandomNumber(int min, int max)
        {
            return this.rnd.Next(min, max).ToString();
        }

        public string ExtractNumbers(string input)
        {
            //List<char> chars = new List<char>();
            //foreach (char c in input)
            //{
            //    if (char.IsDigit(c))
            //    {
            //        chars.Add(c);
            //    }
            //}
            //new string(chars.ToArray());

            // Red ispod menja zakomentarisani kod iznad
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }

        
    }
}
