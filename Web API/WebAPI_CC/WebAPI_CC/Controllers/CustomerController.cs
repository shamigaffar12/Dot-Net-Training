using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebAPI_CC.Models;
using System.Net.Http;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace WebAPI_CC.Controllers
{
    public class CustomerController : ApiController
    {
       
       
        private northwindEntities1 db = new northwindEntities1();

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("employees/by-country/{country}")]
        public IHttpActionResult GetEmployeesByCountry(string country)
        {
            var employees = db.GetCustomersByCountry(country).ToList();
            return Ok(employees);
        }
        

    }
}