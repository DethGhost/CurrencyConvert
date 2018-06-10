using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyConverter_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace CurrencyConverter_.Tests
{
    [TestClass()]
    public class CurrencyConverterTests
    {
        public float testEUR;
        public float testUAH;
        public float testGBP;
        public float amount;
        Random r = new Random();


        [TestInitialize]
        public void InitMethod()
        {
            amount = (float)(r.NextDouble() * 100);
            WebClient client = new WebClient();
            var eur = ("https://free.currencyconverterapi.com/api/v5/convert?q=USD_EUR&compact=ultra");
            var uah = ("https://free.currencyconverterapi.com/api/v5/convert?q=USD_UAH&compact=ultra");
            var gbp = ("https://free.currencyconverterapi.com/api/v5/convert?q=USD_GBP&compact=ultra");
            testEUR = unchecked(Single.Parse(client.DownloadString(eur).Split(':')[1].TrimEnd('}').Replace('.', ',')));
            testUAH = unchecked(Single.Parse(client.DownloadString(uah).Split(':')[1].TrimEnd('}').Replace('.', ',')));
            testGBP = unchecked(Single.Parse(client.DownloadString(gbp).Split(':')[1].TrimEnd('}').Replace('.', ',')));
        }

        [TestMethod()]
        public void ConverterTest()
        {

            Assert.AreEqual(testEUR * amount, CurrencyConverter.Convert(amount, "EUR"));
            Assert.AreEqual(testUAH * amount, CurrencyConverter.Convert(amount, "UAH"));
            Assert.AreEqual(testGBP * amount, CurrencyConverter.Convert(amount, "GBP"));
            Assert.AreEqual(testEUR * 0, CurrencyConverter.Convert(0, "EUR"));
            Assert.AreEqual(testUAH * 0, CurrencyConverter.Convert(0, "UAH"));
            Assert.AreEqual(testGBP * 0, CurrencyConverter.Convert(0, "GBP"));
            Assert.AreNotEqual(1, CurrencyConverter.Convert(0, "EUR"));
            Assert.AreNotEqual(2, CurrencyConverter.Convert(0, "UAH"));
            Assert.AreNotEqual(0.1f, CurrencyConverter.Convert(0, "GBP"));
            

        }

        [TestMethod()]
        public void IsCurrencyAllowedTest()
        {
            Assert.IsFalse(CurrencyConverter.IsCurrencyAllowed("RUR"));
            Assert.IsFalse(CurrencyConverter.IsCurrencyAllowed("CAD"));
            Assert.IsFalse(CurrencyConverter.IsCurrencyAllowed("ZLT"));
            Assert.IsFalse(CurrencyConverter.IsCurrencyAllowed("PHP"));
            Assert.IsFalse(CurrencyConverter.IsCurrencyAllowed("RUP"));
            Assert.IsTrue(CurrencyConverter.IsCurrencyAllowed("GBP"));
            Assert.IsTrue(CurrencyConverter.IsCurrencyAllowed("UAH"));
            Assert.IsTrue(CurrencyConverter.IsCurrencyAllowed("EUR"));
        }
    }
}