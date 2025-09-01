using System.Linq;
using System.Web.Http;
using WebAPI_CC.Models;

namespace WebAPI_CC.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private northwindEntities1 db = new northwindEntities1();
        // GET
        [HttpGet]
        [Route("by-employee/{employeeId}")]
        public IHttpActionResult GetOrdersByEmployee(int empId)
        {
            var orders = db.Orders
                .Where(o => o.EmployeeID == empId)
                .Select(o => new
                {
                    o.OrderID,
                    o.OrderDate,
                    o.ShipCountry,
                    o.CustomerID
                })
                .ToList();

            return Ok(orders);
        }
    }
}
