
 

 

 

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services;
using MoneyManagementSystem.Informations;
using MoneyManagementSystem.Models;
using MoneyManagementSystem.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MoneyManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private static MoneyManagementSystemDB _context;
        private static List<Brands> brandsList;

        public ActionResult User(int userId)
        {
            


            CustomerSingleton.GetCompanies();

            Console.WriteLine("usedId => " + userId);
            _context = new MoneyManagementSystemDB();

            Customer c = _context.GetAllEntitiesDependingOn( _context.Customers.Where(customer => customer.Id == userId).ToList().First()).First();
           // c.CompanyInvestments = _context.MoneyInvestments.Where(inv => inv.Id)
            if (c == null)
            {
                Console.WriteLine("hiç adam yok");
                return null;
            }



            // string connectionString = ConfigurationManager
            //     .ConnectionStrings["moneymanagement"].ToString();
            //Get company investments of users
            
            string CompanyInvestmentsQuery =
                " Select  [dbo].[CompanyInvestments].Id, BrandName, Number from [dbo].[Customers] INNER JOIN [dbo].[CompanyInvestments] ON [dbo].[Customers].[Id] = [dbo].[CompanyInvestments].[Customer_Id] where [dbo].[Customers].[Id]=" +
                userId;
            var rslt = _context.Database.SqlQuery<CompanyInvestment>(CompanyInvestmentsQuery);

          //  c.CompanyInvestments = rslt.ToList();



            //get MoneyInvestments of users
            string query =
                "SELECT [dbo].MoneyInvestments.Id, dollar, euro, gold, turkish_lira FROM [dbo].[MoneyInvestments] INNER JOIN [dbo].[Customers] on [dbo].Customers.MoneyInvestment_Id = [dbo].[MoneyInvestments].Id  where [dbo].[Customers].Id=" +
                userId;

            var result = _context
                .Database
                .SqlQuery<MoneyInvestment>(query);

          //  c.MoneyInvestment = result.First();


          
            
            // using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            //  {
            //      SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            //      sqlConnection.Open();
            //      sqlCommand.ExecuteNonQuery();
            //      sqlConnection.Close();
            //  }
            JObject o = CustomerSingleton.GetCompanies();
            if (brandsList == null)
            {
                brandsList = new List<Brands>();
                foreach (var VARIABLE in o)
                {
                    //Console.WriteLine("değermiz => " + VARIABLE.Key);
                    JObject val = VARIABLE.Value as JObject;
                    brandsList.Add(
                        new Brands()
                        {
                            Name = VARIABLE.Key,
                            PurchasePrice = (float) val?.GetValue("alis"),
                            SalePrice = (float) val?.GetValue("satis"),
                            Change = (float) val?.GetValue("degisim")


                        }
                    );
                }
            }

           // var currencies =CustomerSingleton.GetCurrencies();
            
            Currencies currenciesList =
                new Currencies()
                {
                    // dollar = 1f, euro = (float) currencies.Result.GetValue("ATS"),  gold = (float)currencies.Result.GetValue("BEF"),
                    // turkish_lira = (float) currencies.Result.GetValue("FRF")
                    dollar = 1f, euro = 0.85f, gold = 0.0005f, turkish_lira = 8.6f
                };

            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                Customer = c, Brands = brandsList, Currencies = currenciesList
            };


            ViewBag.userId = c.Id;
            return View(customerViewModel);
        }

        
        [WebMethod]
        public string SellCompanyAllotment(int customerId, string companyName, int amount)
        {
            Console.WriteLine("edsgfdh");

            //get customer given by customerId
            Customer c = _context.GetAllEntitiesDependingOn( _context.Customers.Where(customer => customer.Id == customerId).ToList().First()).First();
            
            //gets brand in the brandlist got from API
            Brands brand = brandsList.Find(b => b.Name == companyName);

            //get the true or false value according to allotment that user has
            bool hasCompanyInvestment = c.CompanyInvestments.Exists(company => company.BrandName == companyName);
            if (!hasCompanyInvestment)
            {
                return "You do not have any allotment from this company!";
            }

            CompanyInvestment CI = c.CompanyInvestments.First(investment => investment.BrandName == companyName);
            if (CI.Number < amount)
            {
                return "You do not have allotments that you want to sell!";
            }
            //decreases allotment amount of user as amount is.
            
            if (CI.Number == amount)
            {
                c.CompanyInvestments.Remove(CI);
                Console.WriteLine("şimdi silinecek");
                _context.Entry(CI).State = EntityState.Deleted;
            }
            else
            {
                CI.Number -= amount;
            }

            //increases turkish liras of user as allotment price is
            c.MoneyInvestment.turkish_lira += amount * brand.PurchasePrice;
            Console.WriteLine("turkish_lira => " + c.MoneyInvestment.turkish_lira);

            _context.Entry(c).State = EntityState.Modified;
            
            _context.SaveChanges();
            
            Console.WriteLine(c.MoneyInvestment.ToString());
            return "OK";
        }

        [WebMethod]
        public string GetCompanyAllotment(int customerId, string companyName, int amount)
        {
            Console.WriteLine("kullanıcı ID => " + customerId + " company name => " + companyName + " amount => " +
                              amount);

            Customer c = _context.GetAllEntitiesDependingOn( _context.Customers.Where(customer => customer.Id == customerId).ToList().First()).First();

            if (c.CompanyInvestments == null)
            {
                Console.WriteLine("company investment null");
            }
           
            foreach (var VARIABLE in c.CompanyInvestments)
            {
                Console.WriteLine("yatırım isimleri => " + VARIABLE.BrandName);
            }

            Console.WriteLine("customer name password => " + c.userName + " " + c.password);


            Brands brand = brandsList.Find(b => b.Name == companyName);
            if (brand != null)
            {
                Console.WriteLine("brand null değil");
            }


            bool hasCompanyInvestment = c.CompanyInvestments.Exists(company => company.BrandName == companyName);

            //c.CompanyInvestments.Find(companyInvestment).Number += amount);
            foreach (var VARIABLE in c.CompanyInvestments)
            {
                Console.WriteLine("yatırım isimleri => " + VARIABLE.BrandName);
            }

            // companyInvestment.Number += amount;
            if (hasCompanyInvestment == false)
            {
                Console.WriteLine("daha önce bu şirkete yatırım yapılmamış");
                c.CompanyInvestments.Add(new CompanyInvestment()
                {
                    BrandName = companyName, Number = amount
                });
            }
            else
            {
                c.CompanyInvestments.First(CI => CI.BrandName == companyName).Number += amount;

            }



            Console.WriteLine("türk lirası azalacak");
            c.MoneyInvestment.turkish_lira -= amount * brand.SalePrice;
            Console.WriteLine("türk lirası azaldı");

        



        _context.Entry(c).State = EntityState.Modified;

        Console.WriteLine("Şimdi save edilecek");
        _context.SaveChanges();
        return "OK";
    }



    [WebMethod]
    public string Convert(string fromCurrency, string toCurrency, int amount , string fromAccount ,
        string toAccount, int userId, int buyorSell)
    {
       
        //buyorsell =1 => buy
        
        Customer customer;
        if (_context.Customers.Any(customer1 => customer1.Id == userId))
        {
            customer = _context
                .GetAllEntitiesDependingOn(_context.Customers.First(customer1 => customer1.Id == userId)).First();
        }
        else
        {
            return "The user is not registered to our service!";
        }
        Currencies currencies =
            new Currencies()
            {
                // dollar = 1f, euro = (float) currencies.Result.GetValue("ATS"),  gold = (float)currencies.Result.GetValue("BEF"),
                // turkish_lira = (float) currencies.Result.GetValue("FRF")
                dollar = 1f, euro = 0.85f, gold = 0.0005f, turkish_lira = 8.6f
            };
        
        
        float dollarAmount;
        float toCurrencyAmount;
        switch (fromCurrency)
            {
                case "turkish lira":
                    dollarAmount = amount / currencies.turkish_lira;
                    break;
                case "euro":
                    dollarAmount = amount / currencies.euro;
                    break;
                case "gold":
                    dollarAmount = amount / currencies.gold;
                    break;
                case "dollar":
                    dollarAmount = amount;
                    break;
                default:
                    return "Please enter a valid account name!";
            }

            switch (toCurrency)
            {
                case "turkish lira":
                    toCurrencyAmount = dollarAmount * currencies.turkish_lira;
                    break;
                case "euro":
                    toCurrencyAmount = dollarAmount * currencies.euro;
                    break;
                case "gold":
                    toCurrencyAmount = dollarAmount * currencies.gold;
                    break;
                case "dollar":
                    toCurrencyAmount = dollarAmount;
                    break;
                default:
                    return "Please enter a valid account name!"; 
            }
            
            
            Console.WriteLine("from account => " + fromAccount + " to account > " + toAccount + 
                              "from amount =>  " + amount + " to amount => " + toCurrencyAmount
            );     
            
            
            float fromAmount;
            if (buyorSell == 0)
            {
                fromAmount = amount;
            }
            else
            {
                fromAmount = toCurrencyAmount;
                toCurrencyAmount = amount;
            }
        switch (fromAccount)
        {
            case "dollar":
                if (amount > customer.MoneyInvestment.dollar)
                {
                    return "The amount in your dollar account is not enough to withdraw from!";
                }
                customer.MoneyInvestment.dollar -= fromAmount;
                break;
            case "turkish lira":
                if (amount > customer.MoneyInvestment.turkish_lira)
                {
                    return "The amount in your turkish lira account is not enough to withdraw from!";
                }
                customer.MoneyInvestment.turkish_lira -= fromAmount;
                break;
            case "euro":
                if (amount > customer.MoneyInvestment.euro)
                {
                    return "The amount in your euro account is not enough to withdraw from!";
                }
                customer.MoneyInvestment.euro -= fromAmount;
                break;
            case "gold":
                if (amount > customer.MoneyInvestment.gold)
                {
                    return "The amount in your euro account is not enough to withdraw from!";
                }
                customer.MoneyInvestment.gold -= fromAmount;
                break;
            default:
                return "you should select a valid account name!";
                
        }
        
        switch (toAccount)
        {
            case "dollar":
                customer.MoneyInvestment.dollar += toCurrencyAmount;
                break;
            case "turkish lira":
                customer.MoneyInvestment.turkish_lira += toCurrencyAmount;
                break;
            case "euro":
                customer.MoneyInvestment.euro += toCurrencyAmount;
                break;
            case "gold":
                customer.MoneyInvestment.gold += toCurrencyAmount;
                break;
            default:
                return "you should select a valid account name!";
                
        }
            
            
        _context.Entry(customer).State = EntityState.Modified;
        _context.SaveChanges();
            
            
        return "OK";
    }

    public ActionResult DashBoard()
    {
        return View();
    }

    public ActionResult Statistics(int customerId)
    {
           JObject historic =  CustomerSingleton.GetEuro();
           
            
			List<DataPoint> dataPoints1= new List<DataPoint>();
 
            
            foreach (KeyValuePair<string, JToken> value in historic)
            {
                JObject ob1 = (JObject) value.Value;
                dataPoints1.Add(new DataPoint((double) ob1.GetValue("date") * 1000,double.Parse(ob1.GetValue("open").ToString()) ));
            }
           
			ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints1);

            ViewBag.userId = customerId;
            
        return View(customerId);
    }
    }

}
 

 