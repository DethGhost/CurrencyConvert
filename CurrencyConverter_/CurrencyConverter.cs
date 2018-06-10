using System;
using System.IO;
using System.Net;
using System.Json;

namespace CurrencyConverter_
{
    public class CurrencyConverter
    {
        private static string API_URL = "https://free.currencyconverterapi.com/api/v5/convert?q=USD_{{currency}}&compact=ultra";
        
        public static float Convert(float amount, string currency)
        {
            if(!CurrencyConverter.IsCurrencyAllowed(currency))
                throw new ArgumentException("Illegal currency");
            
            var json = JsonValue.Parse(GetUrlContent(API_URL.Replace("{{currency}}", currency.ToUpper())));
            var result = (unchecked((JsonObject) json))[("USD_" + currency.ToUpper())];
            return amount * result;
        }

        private static string GetUrlContent(string url)
        {
            var webRequest = WebRequest.Create(url);

            using (var response = webRequest.GetResponse())
            using(var content = response.GetResponseStream())
            using(var reader = new StreamReader(content)){
                return reader.ReadToEnd();
            }
        }

        public static bool IsCurrencyAllowed(string currency)
        {
            switch (currency)
            {
                 case "UAH":
                 case "GBP":
                 case "EUR":
                     return true;
                 default:
                     return false;
            }
        }
    }
}