using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Tasks_for_kottans
{
    class Program
    {
        static bool def;
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your credit card number:");
            string q = Console.ReadLine();
            GetCreditCardVendor(q);
            Console.WriteLine(GetCreditCardVendor(q));
            if (def == true)
            {
                if (IsCreditCardNumberValid(q))
                {
                    Console.WriteLine(GetCreditCardVendor(q));
                    Console.WriteLine("Your card number is valid");
                }
                else
                    Console.WriteLine("Your card number is not valid");
                Console.WriteLine("Next valid number:");
                GenerateNextCreditCardNumber(q);
            }
            else
                Console.WriteLine(GetCreditCardVendor(q));
            Console.ReadLine();
        }
        static string GetCreditCardVendor(string a)
        {
            string[] vendors = { "American Express", "Maestro", "MasterCard", "Visa", "JCB", "Unknown" };
            string amExpress = @"^3[4,7]\d\d ?[\d]{4} ?[\d]{4} ?[\d]{3}$",
             maestro = @"^((?(?=5)5[0,6-9]|6[0-9])\d\d ?[\d]{4} ?([\d]{4}| ?[\d]{4} ?[\d]{1,3}| ?[\d]{4} ?[\d]{4}| ?[\d]{4} ?[\d]{4} ?[\d]{1,3}))$",
             mastCard = @"^(?(?=5)5[1-5]\d\d ?[\d]{4} ?[\d]{4} ?[\d]{4}|2(22[1-9]|2[3-9]\d|[3-6]\d\d|71\d|720) ?[\d]{4} ?[\d]{4} ?[\d]{4})$",
             visa = @"^4\d\d\d ?[\d]{4} ?[\d]{4} ?(\d|[\d]{4}|[\d]{4} ?[\d]{3})$",
             jcb = @"^(35(?(?=2)2[8,9]|[3-8]\d)) ?[\d]{4} ?[\d]{4} ?[\d]{4}$",
             check = @"^[\d, ]{12,23}$";
            def = false;
            if (Regex.IsMatch(a, check))
            {
                def = true;
                if (Regex.IsMatch(a, amExpress))
                    return vendors[0];
                if (Regex.IsMatch(a, maestro))
                    return vendors[1];
                if (Regex.IsMatch(a, mastCard))
                    return vendors[2];
                if (Regex.IsMatch(a, visa))
                    return vendors[3];
                if (Regex.IsMatch(a, jcb))
                    return vendors[4];
                else
                    return vendors[5];
            }
            else
                return String.Format("You enter wrong credit card number");
        }
        static bool IsCreditCardNumberValid(string a)
        {
            a = a.Replace(" ", string.Empty);
            double[] array_for_numbers = new double[a.Length];
            int start;
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                array_for_numbers[i] = Double.Parse(a[i].ToString());
            }
            if (a.Length % 2 == 0)
                start = 0;
            else
                start = 1;
            for (int i = start; i < array_for_numbers.Length; i = i + 2)
            {
                double temp = Convert.ToDouble(array_for_numbers[i]) * 2;
                if (temp > 9)
                {
                    array_for_numbers[i] = temp % 10 + Math.Truncate(temp / 10);
                }
                else
                    array_for_numbers[i] = temp;
            }
            for (int i = 0; i < array_for_numbers.Length; i++)
            {
                sum += array_for_numbers[i];
            }
            if (sum % 10 == 0)
            {
                return true;
            }
            else
            {               
                return false;
            }
        }
        static string GenerateNextCreditCardNumber(string a)
        {
            
            a = a.Replace(" ", string.Empty);                      
            string next_number = a;
            bool check = false;
            while (!check)
            {
                long number = Int64.Parse(next_number);
                number++;
                next_number = number.ToString();
                check = IsCreditCardNumberValid(next_number);
            }
            Console.WriteLine(next_number);
            return next_number;
        }
    }
}
