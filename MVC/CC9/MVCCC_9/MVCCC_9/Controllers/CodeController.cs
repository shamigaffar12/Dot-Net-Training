using MVCCC_9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCC_9.Controllers
{
    public class CodeController : Controller
    {
        // GET: Code
        private northwindEntities db = new northwindEntities();
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult GermanCustomers()
        {
            var germanCustomers = db.Customers
                                     .Where(c => c.Country == "Germany")
                                     .ToList();
            return View(germanCustomers);
        }


        public ActionResult CustomerByOrderID()
        {
            var order = db.Orders
                          .Where(o => o.OrderID == 10248)
                          .Select(o => o.Customer)
                          .FirstOrDefault();

            return View(order);
        }
    }
}
    

