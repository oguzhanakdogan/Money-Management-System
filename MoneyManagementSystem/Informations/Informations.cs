using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using MoneyManagementSystem.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MoneyManagementSystem.Informations
{
    public class CustomerSingleton
    {
        
        private static JObject Companies;
        private static JObject Currencies;

        private CustomerSingleton()
        {
        }

        public static JObject GetEuro()
        {

            string text = System.IO.File.ReadAllText(@"C:\Users\oguzhan.akdogan\RiderProjects\MoneyManagementSystem\MoneyManagementSystem\CurrencyInformations\eurohistory.txt");

            // Display the file contents to the console. Variable text is a string.
            System.Console.WriteLine("Contents of WriteText.txt = {0}", text);
            JObject obj = JObject.Parse(text);
            Console.WriteLine(obj.GetValue("historics"));
            JObject rates;
            
            
            
            
            // var client = new HttpClient();
            // var request = new HttpRequestMessage
            // {
            //     Method = HttpMethod.Get,
            //     RequestUri = new Uri("https://v2.api.forex/historics/USD-EUR.json?source=apiforex&key=f3e5371b-9147-40ee-8532-2913575c355d")
            // };
            // using (var response = await client.SendAsync(request))
            // {
            //     response.EnsureSuccessStatusCode();
            //     var body = await response.Content.ReadAsStringAsync();
            //      obj = JObject.Parse(body);
            //      rates = JObject.Parse(obj.GetValue("historics").ToString());
            //     Console.WriteLine("RATES) => " + rates);
            //     Console.WriteLine(" BEF => " + rates.ToString());
            //     Console.WriteLine("requestin sonucu alındı ...");
            // }
            //
            // Console.WriteLine("requestin sonucu bitti ..." + rates.ToString());
            return obj.GetValue("historics") as JObject;
        }

        public static async Task<JObject> GetCurrencies()
        {
            if (Currencies == null)
            {
                //string fromorto, string currency, int amount, int customerId
                // Customer c = _context.GetAllEntitiesDependingOn(_context.Customers.Where(cus => cus.Id == customerId).First()).First();
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://v2.api.forex/rates/latest.json?beautify=true&key=f3e5371b-9147-40ee-8532-2913575c355d")
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    JObject obj = JObject.Parse(body);
                    JObject rates = JObject.Parse(obj.GetValue("rates").ToString());
                    Currencies = rates; 
                    Console.WriteLine("RATES) => " + rates);
                    Console.WriteLine(rates.ToString() + " BEF => " + rates.GetValue("BEF"));
                }

                
            }
            return Currencies;
            
        }
        public static JObject GetCompanies()
        {
            if (Companies == null)
            {
                string url = "https://api.genelpara.com/embed/borsa.json";
                try
                {
                    string rt;

                    WebRequest request = WebRequest.Create(url);

                    WebResponse response = request.GetResponse();

                    Stream dataStream = response.GetResponseStream();

                    StreamReader reader = new StreamReader(dataStream);

                    rt = reader.ReadToEnd();

                    Companies = JObject.Parse(rt);
                    //   Console.WriteLine("aygz => " + o.GetValue("AYGAZ"));
                    //  string aygaz =  o.GetValue("AYGAZ").ToString();
                    reader.Close();
                    response.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("sıkıntı çıktı => " + ex.Message);

                    return null;
                }
            }
            return Companies;
        }



    }
}