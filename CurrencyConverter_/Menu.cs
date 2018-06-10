using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter_
{
    class Menu
    {
        public Menu()
        {
            do
            {
                Start();
                Console.WriteLine("Press any key to start again or press ESC to exit");
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    break;
            } while (true);
        }

        public void Start()
        {
            Console.WriteLine(@"
Hello to currency converter console application.
To start it use, just enter amount in usd wich yuo want to convert.
Example 24,33 or 5.12
");
            float amount = 0;
            do
            {
                string input = Console.ReadLine();

                if (Single.TryParse(input.Replace('.',','), out amount))
                {
                    break;
                }
                Console.WriteLine("Some's wrong with your input. Please try again.");

            } while (true);


            Console.WriteLine(@"You enter: {0}
Now you need enter currency to you want make convertation.
Allowed currency is UAH, EUR, GBP", amount);
            string currency;
            do
            {
                currency = Console.ReadLine().ToUpper();
                if (CurrencyConverter.IsCurrencyAllowed(currency))
                {
                    break;
                }
                Console.WriteLine("Some's wrong with your currency input. Please try again.");
            } while (true);

            Console.WriteLine("We are calculating.... Please wait.");
            Console.WriteLine("You operation complete you want to convert {0}USD to {1}, and it is {2}{3}",amount,currency,CurrencyConverter.Convert(amount, currency), currency);
        }
    }
}
