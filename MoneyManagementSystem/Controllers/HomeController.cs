using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Services;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using MoneyManagementSystem.Informations;
using MoneyManagementSystem.Models;

namespace MoneyManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Register()
        {

            return View();
        }

        public ActionResult Deneme()
        {

            return View();
        }

        public ActionResult signIn()
        {
            return View();
        }

        public ActionResult Wealths()
        {

            return View();
        }
        [WebMethod]
        public string InsertToDb(string uname, string password)
        {
            List<Payment> payments = new List<Payment>();
            Payment p = new Payment();
            p.electricity_bill = 0;
            p.gas_bill = 0;
            p.kitchen_charges = 0;
            p.water_bill = 0;
            payments.Add(p);

            List<Budget> budgets = new List<Budget>();
            Budget b = new Budget();
            b.bugdet = 10000;
            b.Time = DateTime.Today;
            budgets.Add(b);
            
            Customer new_custumer = new Customer
            {
                password = password, userName = uname,
                Payments = payments, Budgets =budgets,
                MoneyInvestment =  new MoneyInvestment() {dollar =  0f, euro = 0f, gold = 0f, turkish_lira = 10000},
                CompanyInvestments = new List<CompanyInvestment>() {}

            };
            MoneyManagementSystemDB context = new MoneyManagementSystemDB();
            context.Customers.Add(new_custumer);
            context.SaveChanges();
            // string connectionString = ConfigurationManager
            //     .ConnectionStrings["moneymanagement"].ToString();
            // string query = "INSERT INTO dbo.Customers (username, password) VALUES ('"+uname+"','"+password+"')";
            //
            // using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            // {
            //     SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            //     sqlConnection.Open();
            //     sqlCommand.ExecuteNonQuery();
            //     sqlConnection.Close();
            // }
            Wealths();
            return "işlem onaylandi";
        }

        [WebMethod]
        public int User(string username, string password)
        {
            Console.WriteLine("giriş");
            MoneyManagementSystemDB _context = new MoneyManagementSystemDB();
            List<Customer> customers = _context.Customers
                .Where(customer => customer.userName == username && customer.password == password).ToList();

            if (customers.Count == 1)
            {
                return customers[0].Id;
            }


           return -1;
        }
        
    }
}